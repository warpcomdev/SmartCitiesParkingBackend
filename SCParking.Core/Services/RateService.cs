using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SCParking.Core.Interfaces;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Messages;
using SCParking.Domain.Models;
using SCParking.Domain.Validation;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Core.Services
{
    public class RateService : IRateService
    {
        private readonly IHelpers _helpers;
        private readonly ILogger _logger;
        private readonly IRateRepository _rateRepository;
        private readonly IRateDetailsRepository _rateDetailsRepository;
        public RateService(ILogger<RateService> logger,
            IHelpers helpers,IRateRepository rateRepository,IRateDetailsRepository rateDetailsRepository)
        {
            _logger = logger;
            _helpers = helpers;
            _rateRepository = rateRepository;
            _rateDetailsRepository = rateDetailsRepository;
        }

        public async Task Delete(Guid id)
        {
            //Borramos los detalles del RateId
            await _rateDetailsRepository.Delete(id);
            await _rateRepository.Delete(id);
        }

        public async Task<RatePostDto> GetById(Guid id)
        {
            var rate = await _rateRepository.GetById(id);
            RatePostDto resp = new RatePostDto()
            {
               endDate  = rate.EndDate.ToString("dd/MM/yyyy"),
               id = rate.Id.ToString(),
               name = rate.Name,
               placeType = rate.PlaceType,
               price = rate.Price,
               startDate = rate.StartDate.ToString("dd/MM/yyyy")

            };
            resp.rateDetails = await _rateDetailsRepository.GetByRateId(id);
            return resp;


        }

        public async Task<List<RatePostDto>> GetRates()
        {
            var rates = await _rateRepository.GetRates();
            List<RatePostDto> lista = new List<RatePostDto>();
            foreach (var rate in rates)
            {
                RatePostDto resp = new RatePostDto()
                {
                    id = rate.Id.ToString(),
                    endDate = rate.EndDate.ToString("dd/MM/yyyy"),
                    name = rate.Name,
                    placeType = rate.PlaceType,
                    price = rate.Price,
                    startDate = rate.StartDate.ToString("dd/MM/yyyy")

                };
                resp.rateDetails = await _rateDetailsRepository.GetByRateId(rate.Id);
                lista.Add(resp);
            }

            return lista;
        }
        public async Task<RatePostDto> GetByPlaceType(string placeType)
        {
            
            var rate = await _rateRepository.GetRateByPlaceType(placeType);
            if (rate != null)
            {
                RatePostDto resp = new RatePostDto()
                {
                    id = rate.Id.ToString(),
                    endDate = rate.EndDate.ToString("dd/MM/yyyy"),
                    name = rate.Name,
                    placeType = rate.PlaceType,
                    price = rate.Price,
                    startDate = rate.StartDate.ToString("dd/MM/yyyy")

                };
                resp.rateDetails = await _rateDetailsRepository.GetByRateId(rate.Id);
                return resp;
            }

            return null;

        }

        public async Task<RatePostDto> Insert(RatePostDto model,Guid currentUserId)
        {
            var error = new ErrorDto();
            
            ValidateErrorDto validate = await ValidateInsertOrUpdateRate(model, "Add");
            error.errors = validate.errorField;
            if (validate.existErrors)
            {
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }

            var entity = new Rate()
            {
                Id = Guid.NewGuid(),
                Name = model.name,
                StartDate = DateTime.ParseExact(model.startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(model.endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                PlaceType = model.placeType,
                Price = model.price,
                CreatedAt = DateTime.Now,
                CreatedBy = currentUserId
            };
            model.id = entity.Id.ToString();
            try
            {
                var rate = await _rateRepository.Insert(entity);
                List<RateDetails> listDetails = new List<RateDetails>();
                foreach (var rateDetail in model.rateDetails)
                {

                    var rateDetailModel = new RateDetails()
                    {
                        Id = Guid.NewGuid(),
                        RateId = entity.Id,
                        CreatedAt = DateTime.Now,
                        CreatedBy = currentUserId,
                        Day = rateDetail.day,
                        Hour = rateDetail.hour,
                        Price = rateDetail.price

                    };
                    rateDetail.id = rateDetailModel.Id.ToString();
                    listDetails.Add(rateDetailModel);

                }

                await _rateDetailsRepository.InsertBulk(listDetails);


                return model;

            }
            catch (Exception ex)
            {

                throw;
            }

            

        }

        

        public async Task<RatePostDto> Update(RatePostDto model,Guid currentUserId)
        {
            var error = new ErrorDto();
            
            ValidateErrorDto validate = await ValidateInsertOrUpdateRate(model, "Update");
            error.errors = validate.errorField;
            if (validate.existErrors)
            {
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }
            
            var entity = new Rate()
            {
                Id = Guid.Parse(model.id),
                Name = model.name,
                StartDate = DateTime.ParseExact(model.startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(model.endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                PlaceType = model.placeType,
                Price = model.price,
                UpdatedAt = DateTime.Now,
                ModifiedBy = currentUserId
            };

            try
            {
                var rate = await _rateRepository.Update(entity,currentUserId);
            }
            catch (Exception ex)
            {

                throw;
            }

            List<RateDetails> listDetails = new List<RateDetails>();
            foreach (var rateDetail in model.rateDetails)
            {
                
                var rateDetailModel = new RateDetails()
                {
                    
                    RateId = entity.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = currentUserId,
                    Day = rateDetail.day,
                    Hour = rateDetail.hour,
                    Price = rateDetail.price

                };
                
                listDetails.Add(rateDetailModel);

            }

            await _rateDetailsRepository.UpdateBulk(listDetails,entity.Id,currentUserId);


            return model;

        }

       

        private async Task<ValidateErrorDto> ValidateInsertOrUpdateRate(RatePostDto model, string action)
        {
            var validate = new ValidateErrorDto { errorField = new ExpandoObject() };
            var arr = new List<ErrorDetailDto>();
            try
            {
                if (action == "Update")
                {
                    if (String.IsNullOrEmpty(model.id))//En la actualizacion no se envia el Id
                    {
                        var arrId = new List<ErrorDetailDto>
                        {
                            new ErrorDetailDto() { error = TokenError.Blank }
                        };
                        validate.errorField.id = arrId;
                        validate.existErrors = true;
                    }
                    else
                    {
                        var arrId = new List<ErrorDetailDto> { new ErrorDetailDto() { error = TokenError.Invalid } };
                        bool isValid = _helpers.IsGuid(model.id);
                        if (isValid)
                        {

                            var rateExist = await _rateRepository.GetById(Guid.Parse(model.id));
                            if (rateExist == null)
                            {
                                validate.errorField.id = arrId;
                                validate.existErrors = true;
                            }
                        }
                        else
                        {
                            validate.errorField.id = arrId;
                            validate.existErrors = true;
                        }
                    }
                }
                if (string.IsNullOrEmpty(model.name))//Name es requerido
                {
                    var arrName = new List<ErrorDetailDto>
                    {
                        new ErrorDetailDto() { error = TokenError.Blank }
                    };
                    validate.errorField.name = arrName;
                    validate.existErrors = true;
                }
                else if (model.name.Length > 150)//Name no puede ser mayor a 150
                {
                    var arrName = new List<ErrorDetailDto>
                    {
                        new ErrorDetailDto() { error = TokenError.TooLong, count = model.name.Length }
                    };
                    validate.errorField.name = arrName;
                    validate.existErrors = true;
                }
                else
                {
                    var id = action == "Add" ? null : model.id.ToString();
                    // Validar Si existe en base de datos con el mismo nombre
                    var rateExists = await _rateRepository.GetRateByName(model.name);
                    if (rateExists != null && action=="Add")//Existe ya el tarifario con el nombre
                    {
                        var arrName = new List<ErrorDetailDto>
                        {
                            new ErrorDetailDto() { error = TokenError.Taken }
                        };
                        validate.errorField.name = arrName;
                        validate.existErrors = true;
                    }
                }


                if (string.IsNullOrEmpty(model.startDate))//La fecha inicio es requerido
                {

                    var arrstartAt = new List<ErrorDetailDto>
                        {
                            new ErrorDetailDto() { error = TokenError.Blank }
                        };
                    validate.errorField.startAt = arrstartAt;
                    validate.existErrors = true;
                }
                else
                {
                    bool isValidDate = _helpers.IsDateTime(model.startDate);//es formato fecha válido dd/MM/yyyy
                    if(!isValidDate)
                    {
                        var arrstartAt = new List<ErrorDetailDto>
                                            {
                                                new ErrorDetailDto() { error = TokenError.Format }
                                            };
                        validate.errorField.startAt = arrstartAt;
                        validate.existErrors = true;

                    }
                }

                if (string.IsNullOrEmpty(model.endDate))//fecha fin requerido
                {

                    var arrendAt = new List<ErrorDetailDto>
                        {
                            new ErrorDetailDto() { error = TokenError.Blank }
                        };
                    validate.errorField.endAt = arrendAt;
                    validate.existErrors = true;
                }
                else
                {

                    bool isValidDate = _helpers.IsDateTime(model.endDate);
                    if (!isValidDate)
                    {
                        
                        var arrendAt = new List<ErrorDetailDto>
                                            {
                                                new ErrorDetailDto() { error = TokenError.Format }
                                            };
                        validate.errorField.endAt = arrendAt;
                        validate.existErrors = true;

                    }

                }

                if (!string.IsNullOrEmpty(model.startDate) && !string.IsNullOrEmpty(model.endDate))
                {
                    bool isValidDateStart = _helpers.IsDateTime(model.startDate);
                    bool isValidDateEnd = _helpers.IsDateTime(model.endDate);

                    if (isValidDateStart && isValidDateEnd)
                    {
                        if (DateTime.ParseExact(model.endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.ParseExact(model.startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        {
                            var arrendAt = new List<ErrorDetailDto>
                                            {
                                                new ErrorDetailDto() { error = TokenError.LessThan }
                                            };
                            validate.errorField.endAt = arrendAt;
                            validate.existErrors = true;
                        }
                    }

                }

                var rateExistByType = await GetByPlaceType(model.placeType);
                if (rateExistByType != null && action == "Add")
                {
                    string fechaHoy = DateTime.Now.ToString("dd/MM/yyyy");
                    if (DateTime.ParseExact(fechaHoy, "dd/MM/yyyy", CultureInfo.InvariantCulture) <
                        DateTime.ParseExact(rateExistByType.endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))//Exite aún un tarifario activo para el tipo de plaza
                    {
                        var arrName = new List<ErrorDetailDto>
                        {
                            new ErrorDetailDto() { error = TokenError.Taken }
                        };
                        validate.errorField.name = arrName;
                        validate.existErrors = true;
                    }
                }
                
                return validate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        
    }
}
