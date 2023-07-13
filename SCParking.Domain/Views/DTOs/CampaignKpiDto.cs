using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class CampaignKpiDto
    {
        public string hora { get; set; }
        public decimal mediaLeads { get; set; }
        public int lwLeads { get; set; }
        public int leads { get; set; }
        public decimal porcLeads { get; set; }
        public decimal mediaVentas { get; set; }
        public int lwVentas { get; set; }
        public int ventas { get; set; }
        public decimal porcVentas { get; set; }
        public decimal mediaPalitos { get; set; }
        public int lwPalitos { get; set; }
        public int palitos { get; set; }
        public decimal porcPalitos { get; set; }
        public decimal porcVentasMedia { get; set; }
        public decimal porcVentasLw { get; set; }
        public decimal porcVentasHoy { get; set; }

        public decimal porcPalitosMedia { get; set; }
        public decimal porcPalitosLw { get; set; }
        public decimal porcPalitosHoy { get; set; }
    }

    public class CampaignKpiResponseDto
    {
        public string campaignId { get; set; }
        public string campaign { get; set; }
        public string fecha { get; set; }
        public string customerId { get; set; }
        public string customer { get; set; }
        public List<CampaignKpiDto> resultados { get; set; }

        public CampaignKpiResponseDto()
        {
            resultados = new List<CampaignKpiDto>();
        }
    }
}
