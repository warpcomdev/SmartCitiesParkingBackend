using Newtonsoft.Json;

namespace SCParking.Domain.SmartCitiesModels
{
    public class MuyBici
    {
        [JsonProperty("id_aparcamiento")]
        
        public long IdAparcamiento { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("num_puestos")]
        
        public long NumPuestos { get; set; }

        [JsonProperty("nombrecorto")]
        public string Nombrecorto { get; set; }

        [JsonProperty("id_poblacion")]
        
        public long IdPoblacion { get; set; }

        [JsonProperty("xocupados")]
        
        public long Xocupados { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("eshabilitada")]
        
        public long Eshabilitada { get; set; }

        [JsonProperty("xactivo")]
        
        public long Xactivo { get; set; }

        [JsonProperty("libres")]
        public long Libres { get; set; }

        [JsonProperty("ocupados")]
        
        public long Ocupados { get; set; }
    }
}
