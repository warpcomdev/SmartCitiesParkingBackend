using System;
using Newtonsoft.Json;
using SCParking.Domain.Common;

namespace SCParking.Domain.Entities

{
    public partial class InfoExtra
    {
        [JsonProperty("tipoTelefonia")]
        public string TipoTelefonia { get; set; }

        [JsonProperty("tipoLlamada")]
        public string TipoLlamada { get; set; }

        [JsonProperty("fechaLlamadaProgramada")]
        public DateTime FechaLlamadaProgramada { get; set; }

        [JsonProperty("segmento")]
        public string Segmento { get; set; }

        [JsonProperty("promocion")]
        public string Promoción { get; set; }

        [JsonProperty("tipoCampagna")]
        public string TipoCampagna { get; set; }

        [JsonProperty("infoCampagna")]
        public string InfoCampagna { get; set; }

        [JsonProperty("genero")]
        public string Genero { get; set; }

        [JsonProperty("codigoPostal")]
        public string CodigoPostal { get; set; }

        [JsonProperty("ipUsuario")]
        public string IpUsuario { get; set; }

        [JsonProperty("idAfiliado")]
        public string IdAfiliado { get; set; }

        [JsonProperty("urlCaptado")]
        public string UrlCaptado { get; set; }

        [JsonProperty("valorMarketing")]
        public string ValorMarketing { get; set; }

        [JsonProperty("categoriaAdn")]
        public string CategoriaAdn { get; set; }

        #region telefonos alternos
        [JsonProperty("telefono1")]
        public string Telefono1 { get; set; }

        [JsonProperty("telefono1Descripcion")]
        public string Telefono1Descripcion { get; set; }

        [JsonProperty("telefono2")]
        public string Telefono2 { get; set; }

        [JsonProperty("telefono2Descripcion")]
        public string Telefono2Descripcion { get; set; }

        [JsonProperty("telefono3")]
        public string Telefono3 { get; set; }

        [JsonProperty("telefono3Descripcion")]
        public string Telefono3Descripcion { get; set; }

        [JsonProperty("telefono4")]
        public string Telefono4 { get; set; }

        [JsonProperty("telefono4Descripcion")]
        public string Telefono4Descripcion { get; set; }

        [JsonProperty("telefono5")]
        public string Telefono5 { get; set; }

        [JsonProperty("telefono5Descripcion")]
        public string Telefono5Descripcion { get; set; }

        [JsonProperty("telefono6")]
        public string Telefono6 { get; set; }

        [JsonProperty("telefono6Descripcion")]
        public string Telefono6Descripcion { get; set; }

        [JsonProperty("telefono7")]
        public string Telefono7 { get; set; }

        [JsonProperty("telefono7Descripcion")]
        public string Telefono7Descripcion { get; set; }

        [JsonProperty("telefono8")]
        public string Telefono8 { get; set; }

        [JsonProperty("telefono8Descripcion")]
        public string Telefono8Descripcion { get; set; }

        [JsonProperty("telefono9")]
        public string Telefono9 { get; set; }

        [JsonProperty("telefono9Descripcion")]
        public string Telefono9Descripcion { get; set; }

        [JsonProperty("telefono10")]
        public string Telefono10 { get; set; }

        [JsonProperty("telefono10Descripcion")]
        public string Telefono10Descripcion { get; set; }
        #endregion

        [JsonProperty("ciudad")]
        public string Ciudad { get; set; }

        [JsonProperty("marca_dispositivo")]
        public string MarcaDispositivo { get; set; }
        [JsonProperty("modelo_dispositivo")]
        public string ModeloDispositivo { get; set; }
        [JsonProperty("referenciador")]
        public string Referenciador { get; set; }
        [JsonProperty("id_backlanding")]
        public string IdBacklanding { get; set; }
        [JsonProperty("conversion")]
        public string Conversion { get; set; }
    }

    public partial class InfoExtra
    {
        public static InfoExtra FromJson(string json)
        {
            InfoExtra resultado = new InfoExtra();
            try
            {
                resultado = JsonConvert.DeserializeObject<InfoExtra>(json, Tools.Converter.Settings);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }
    }

    public static class Serialize
    {
        public static string ToJson(this InfoExtra self) => JsonConvert.SerializeObject(self, Tools.Converter.Settings);
    }
   
}
