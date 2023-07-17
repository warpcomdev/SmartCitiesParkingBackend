using SCParking.Core.Interfaces;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SCParking.Domain.Common;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Core.Services
{
    public class MuyBiciService : IMuyBiciService
    {
        private readonly ICollection<Setting> _settings;
        private readonly ISmartCityRepository _smartCityRepository;
        private readonly ISettingRepository _settingRepository;
        private readonly IHelpers _helpers;
        private readonly ILogger _logger;
        public MuyBiciService(ILogger<MuyBiciService> logger,ISmartCityRepository smartCityRepository,IHelpers helpers,ISettingRepository settingRepository)
        {
            _logger = logger;
            _settingRepository = settingRepository;
            _settings = _settingRepository.Get();
            _helpers = helpers;
            _smartCityRepository = smartCityRepository;
        }
        public async Task<BiciEntityRequestDto> Get()
        {
            var bicisEntities = await _smartCityRepository.GetMuyBicisEntity();
            BiciEntityRequestDto biciEntityRequestDto = new BiciEntityRequestDto()
            {
                actionType = "append",
                entities = bicisEntities.ToList()
            };

            return biciEntityRequestDto;
        }

        public async Task SendEntities()
        {
            var entities = await Get();
            var usrParking = _settings.Where(x => x.SettingCode == Constants.Setting_USRSCSERVICEPARKING).FirstOrDefault().Value;
            var passwordParking = _settings.Where(x => x.SettingCode == Constants.Setting_PASSSCSERVICEPARKING).FirstOrDefault().Value;
            var token = await _smartCityRepository.Authenticate("murcia", "/bicicletas", usrParking, passwordParking);
            await _smartCityRepository.SendBatchEntities(token, entities);
        }
    }
}
