using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SCParking.Domain.Common
{
    public static class Tools
    {

        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "123456789";
        const string SPECIALS = @"!@*#";
        public const string StringChars = "0123456789abcdef";

        public static string RandomToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public static string GenerateUniqueHexString(int length)
        {
            Random rand = new Random();
            var charList = StringChars.ToArray();
            string hexString = "";

            for (int i = 0; i < length; i++)
            {
                int randIndex = rand.Next(0, charList.Length);
                hexString += charList[randIndex];
            }

            return hexString;
        }



        public static string CustomToken(Guid complementId, Guid idKey)
        {
            byte[] _time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] _key = idKey.ToByteArray();
            byte[] _complement = complementId.ToByteArray();
            byte[] data = new byte[_time.Length + _key.Length + _complement.Length];

            System.Buffer.BlockCopy(_time, 0, data, 0, _time.Length);
            System.Buffer.BlockCopy(_key, 0, data, _time.Length, _key.Length);
            System.Buffer.BlockCopy(_complement, 0, data, _time.Length + _key.Length, _complement.Length);

            return BitConverter.ToString(data).Replace("-", "");
        }
        
        public static string GeneratePixelControl(string laiaToken, string src, string siteId, string scriptId)
        {
           string script = $"<script " +
                // $"src=\""+ src + "/laia.js?" + TimeSpanTicks()+ "\" " +
                $"src=\"" + src + "/laia.js?e8277a89bab189f76e90\" " +
                $"id=\"laia-script\" " +
                $"data-site-id=\""+ siteId + "\" " +
                $"data-ctm-id=\"" + scriptId + "\" " +
                $"data-laia-token=\""+ laiaToken + "\" " +
                $"async= \"\">" +
                $"</script>";
           return script;
         }
        public static string TimeSpanTicks()
        {
            return DateTime.UtcNow.Ticks.ToString();
        }

        public static string GetStatus(int? status)
        {
            switch (status)
            {
                case 1: return "Activo";
                case 2: return "Suspendido";
                case 3: return "Eliminado";
                default: return "Eliminado";
            }

        }

        public static string GetRoleName(string name)
        {
            switch (name)
            {
                case "admin": return "Administrador";
                case "super-admin": return "Super Administrador";
                case "operator": return "Operador";
                case "agent": return "Agente";
                default: return "";
            }

        }


        public static string GetUserType(int? type)
        {
            switch (type)
            {
                case 1: return "Aplicacion";
                case 2: return "API";
                default: return "Aplicacion";
            }

        }

        //public static bool CheckIfPropertyExistsInErrorDto(dynamic objec , string property)
        //{
        //    var result = false;
        //    try
        //    {
        //        //if(objec.)

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    return result;
        //}

        public static string GetMD5Hash(string input)
        {
            string resultado = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            resultado = sb.ToString();
            return resultado;
        }

        public static string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial,
        int passwordSize)
        {
            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            System.Random _random = new Random();
            int counter;

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;

            if (useUppercase) charSet += UPPER_CAES;

            if (useNumbers) charSet += NUMBERS;

            if (useSpecial) charSet += SPECIALS;

            for (counter = 0; counter < passwordSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            return String.Join(null, _password);
        }

        public static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        public static void CopyDictionary(IDictionary srcDict, ref IDictionary destDict)
        {
            if (srcDict == null)
                throw new System.ArgumentNullException("Source cannot be null.");

            if (destDict == null)
                throw new System.ArgumentNullException("Destination cannot be null.");

            if (srcDict.Count == 0)
                return;

            destDict.Clear();

            foreach (object key in srcDict.Keys)
            {
                destDict.Add(key, srcDict[key]);
            }

        }

        public static IDictionary CloneDictionary(IDictionary srcDict)
        {
            Type srcDicType = srcDict.GetType();

            IDictionary cloned = (IDictionary)Activator.CreateInstance(srcDicType);

            CopyDictionary(srcDict, ref cloned);

            return cloned;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        public static string DataTableToJson(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public static string GetDataValueJsonString(string valueExtract, string jsonString)
        {

            var data = valueExtract.Split('.');
            var jo = JObject.Parse(jsonString);
            int i = 1;
            JValue va = (JValue)"";
            foreach (var d in data)
            {

                if (i == data.Length)
                {
                    va = jo[d] as JValue;

                }
                else
                {
                    jo = (JObject)jo[d];
                }
                i = i + 1;
            }
            return va.ToString();
        }

    }
}
