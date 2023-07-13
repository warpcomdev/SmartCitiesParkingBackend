using Newtonsoft.Json;

namespace SCParking.Domain.Tracking
{
    public class Site
    {
        [JsonProperty("idsite")]
        public string IdSite { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("main_url")]
        public string MainUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
       
        //[JsonProperty("ts_created")]
        //public string ts_created { get; set; }
        //[JsonProperty("ecommerce")]
        //public string ecommerce { get; set; }
        //[JsonProperty("sitesearch")]
        //public string sitesearch { get; set; }
        //[JsonProperty("sitesearch_keyword_parameters")]
        //public string sitesearch_keyword_parameters { get; set; }
        //[JsonProperty("sitesearch_category_parameters")]
        //public string sitesearch_category_parameters { get; set; }
        //[JsonProperty("timezone")]
        //public string timezone { get; set; }
        //[JsonProperty("currency")]
        //public string currency { get; set; }
        //[JsonProperty("exclude_unknown_urls")]
        //public string exclude_unknown_urls { get; set; }
        //[JsonProperty("excluded_ips")]
        //public string excluded_ips { get; set; }
        //[JsonProperty("excluded_parameters")]
        //public string excluded_parameters { get; set; }
        //[JsonProperty("excluded_user_agents")]
        //public string excluded_user_agents { get; set; }
        //[JsonProperty("group")]
        //public string group { get; set; }    
        //[JsonProperty("keep_url_fragment")]
        //public string keep_url_fragment { get; set; }
        //[JsonProperty("creator_login")]
        //public string creator_login { get; set; }
        //[JsonProperty("timezone_name")]
        //public string timezone_name { get; set; }
        //[JsonProperty("currency_name")]
        //public string currency_name { get; set; }

    }
}
