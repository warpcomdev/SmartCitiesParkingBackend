using System.Collections.Generic;
using SCParking.Domain.Models;

namespace SCParking.Domain.Interfaces
{
    public interface ISettingRepository
    {       
        ICollection<Setting> Get();        
        
    }
}
