using System.Threading.Tasks;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Domain.Integrations
{
    public interface IV3ClickRepository
    {
        public Task<SendCodificationResponseDto> SendCodification(SendCodificationDto sendCodification);
        public Task<SendCallLeadResponseDto> SendCallLead(SendCallLeadDto sendCallLead);

    }
}
