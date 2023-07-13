namespace SCParking.Domain.Views.DTOs
{
    public class LeadFilterDto: FilterDto
    {
       public string campaignId { get; set; }

       public string customerId { get; set; }
    }

}
