using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;
using System.Linq.Dynamic;

namespace SCParking.Domain.Interfaces
{
    public interface IHelpers
    {
        string UploadFile(IFormFile file, string nameFile, string folder);
        string GeneralPathWeb(string path);

        bool IsGuid(string value);
        bool IsDateTime(string value);

        IEnumerable<dynamic> Sort(IEnumerable<DynamicClass> data, Dictionary<string, string> order);

        void ToCSV(DataTable dtDataTable, string strNameFile);
    }
}
