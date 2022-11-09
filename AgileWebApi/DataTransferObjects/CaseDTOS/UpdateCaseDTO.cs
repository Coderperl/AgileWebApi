using AgileWebApi.Data;

namespace AgileWebApi.DataTransferObjects.CaseDTO
{
    public class UpdateCaseDTO
    {
        public int TechnicianId { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime CaseEnded { get; set; }
    }
}
