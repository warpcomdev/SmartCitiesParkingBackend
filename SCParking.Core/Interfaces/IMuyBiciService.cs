using SCParking.Domain.Views.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCParking.Core.Interfaces
{
    public interface IMuyBiciService
    {
        Task<BiciEntityRequestDto> Get();
        Task SendEntities();
    }
}
