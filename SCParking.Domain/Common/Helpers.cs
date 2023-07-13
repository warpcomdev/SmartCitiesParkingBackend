using SCParking.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Globalization;
using System.Data;

namespace SCParking.Domain.Common
{
    public class Helpers: IHelpers
    {
        public static IWebHostEnvironment _environment;
        public Helpers(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public string UploadFile(IFormFile file, string nameFile, string folder)
        {
            string path = "\\uploads\\"+ folder+"\\" + nameFile + Path.GetExtension(file.FileName);
            try
            {
                string contentRootPath = _environment.ContentRootPath;
                if (!Directory.Exists(_environment.ContentRootPath + "\\uploads\\"+folder+"\\"))
                {
                    Directory.CreateDirectory(contentRootPath + "\\uploads\\"+folder+"\\");
                }
                using (FileStream filestream = System.IO.File.Create(contentRootPath + path))
                {
                    file.CopyTo(filestream);
                    filestream.Flush();
                    return path;
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }


        public  bool IsGuid(string value)
        {
            return Guid.TryParse(value, out _);
        }

        public bool IsDateTime(string value)
        {
            return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public string GeneralPathWeb(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    path = path.Replace("\\", "/");
                    path = Environment.GetEnvironmentVariable("LAIA_ApiHost") + path + "?" + Tools.TimeSpanTicks();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return path;
        }

        public IEnumerable<dynamic> Sort(IEnumerable<DynamicClass> data, Dictionary<string, string> order)
        {

            // Ordenamiento
            if (order != null)
            {
                _ = new List<dynamic>();
                string orderAscending = string.Empty;
                string orderDecending = string.Empty;
                //PropertyInfo[] lst = typeof(order.GetType()).GetProperties();
                //PropertyInfo[] lst = order.GetType().GetProperties();

                foreach (KeyValuePair<string, string> itemOrden in order)
                {
                    if (itemOrden.Value == "asc")
                    {
                        orderAscending = $"{orderAscending},{itemOrden.Key}";
                    }
                    if (itemOrden.Value == "desc")
                    {
                        orderDecending = $"{orderDecending},{itemOrden.Key}";
                    }
                }

                /*foreach (PropertyInfo oProperty in lst)
                {
                    string nameProperty = oProperty.Name;
                    var value = oProperty.GetValue(order)?.ToString();
                    //var ordenado = new List<Customer>();
                    if (value == "asc")
                    {
                        orderAscending = $"{orderAscending},{nameProperty}";
                    }
                    if (value == "desc")
                    {
                        orderDecending = $"{orderDecending},{nameProperty}";
                    }

                }*/

                //Adiciono datos originales por si entra un order de una propiedad que no existe
                //, se agrega try - catch para controlar propiedad que no exista y evitar error 500
                List<DynamicClass> orderList = data.ToList();

                if (!string.IsNullOrEmpty(orderAscending))
                {
                    orderAscending = orderAscending.Substring(1);
                    //data= (PagedList<T>)data.OrderBy($"{orderAscending} ascending");
                    try
                    {
                        orderList = data.OrderBy($"{orderAscending} ascending").ToList();
                        //var dddxx = data.OrderBy(x => x.Name).ToList();

                        //var ddd = data.OrderBy(x => x.sortCol + " " + x.sortDir).ToList();
                        //var dddd = ddd;
                    }
                    catch (Exception ex)
                    {
                        
                    }

                }
                if (!string.IsNullOrEmpty(orderDecending))
                {
                    orderDecending = orderDecending.Substring(1);
                    try
                    {
                        orderList = data.OrderBy($"{orderDecending} descending").ToList();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                //Temporal mientras se encuentra otra forma de evitar error de tipo de dato
                //data.Clear();
                //foreach (var dat in orderList)
                //{
                //    data.Add(customer);
                //}

            }

            return data;
        }


        public void ToCSV(DataTable dtDataTable, string strNameFile)
        {            
            var strFilePath =  _environment.ContentRootPath + "\\uploads\\files\\"+ strNameFile;                           
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
