using SCParking.Domain.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SCParking.API.Helpers
{
    public class FilterRequest
    {

        public void GetParametersFilterArray(ref Dictionary<string,string> filter, string queryString)
        {
            var requestquerystring = System.Net.WebUtility.UrlDecode(queryString);
            var requestParametersIn = requestquerystring.Replace("?", "").Split("&");
          
            var requestParameters = new ArrayList();
            

            //Exclude Parameters without array
            foreach (var requestparam in requestParametersIn)
            {
                if(requestparam.Contains("[]"))
                {
                    requestParameters.Add(requestparam);
                }
            }            
            //Clone Dictionary to update values
            var cloneDictionary = (Dictionary<string, string>)Tools.CloneDictionary(filter);
            
            //Storage key and values from arrays
            Hashtable requestArray = new Hashtable(); 

            //ForEach for parameters with value == null for arrays
            foreach (var parameterArray in cloneDictionary.Where(x => x.Value == null))
            {
                var arrValues = new ArrayList();
                var paramKey = parameterArray.Key;

                bool exist = false;
                foreach (var requestparamValidation in requestParameters)
                {
                    if (requestparamValidation.ToString().Contains(paramKey))
                        exist = true;
                }

                if (exist) //Include only parameters with array
                {

                    foreach (var parameter in requestParameters)
                    {
                        if (parameter.ToString().Contains("[]") && parameter.ToString().Contains(paramKey))
                        {

                            var value = (parameter.ToString().Split("="))[1];
                            arrValues.Add(value);
                            requestArray[paramKey] = arrValues;
                        }
                    }

                    filter.Remove(paramKey);
                    var arrayFull = (ArrayList)requestArray[paramKey];
                    var strings = arrayFull.Cast<string>().ToArray();
                    var valuesParameters = string.Join(",", strings);
                    filter.Add(paramKey, valuesParameters);

                }  

              
            }
        }

        public void GetParametersIdArray(ref Dictionary<string, string> id, string queryString)
        {
            var requestquerystring = System.Net.WebUtility.UrlDecode(queryString);
            var requestParametersIn = requestquerystring.Replace("?", "").Split("&");

            var requestParameters = new ArrayList();


            //Exclude Parameters without array
            foreach (var requestparam in requestParametersIn)
            {
                if (requestparam.Contains("[]"))
                {
                    requestParameters.Add(requestparam);
                }
            }

            var dictionaryParameters = new Dictionary<string, string>();

            foreach (var parameter in requestParameters)
            {
                if (parameter.ToString().Contains("[]="))
                {

                    var valueParameter = (parameter.ToString().Split("[]="))[1];
                    var nameParameter = (parameter.ToString().Split("[]="))[0];
                    if(dictionaryParameters.ContainsKey(nameParameter))
                    {
                        var values = dictionaryParameters[nameParameter] + "," + valueParameter;
                        dictionaryParameters.Remove(nameParameter);
                        dictionaryParameters.Add(nameParameter, values);
                    }
                    else
                    {
                        dictionaryParameters.Add(nameParameter, valueParameter);
                    }                  
                 
                }
            }
            id = dictionaryParameters;
            
        
        }


    }
}
