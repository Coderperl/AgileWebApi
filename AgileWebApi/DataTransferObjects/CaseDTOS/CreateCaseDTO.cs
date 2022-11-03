using AgileWebApi.Data;

namespace AgileWebApi.DataTransferObjects.CaseDTO
{
    public class CreateCaseDTO
    {
        public string Name { get; set; }
        public int ElevatorId { get; set; }
        public int TechnicianId { get; set; }
        public Comment Comment { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CaseCreated { get; set; }
        public DateTime CaseEnded { get; set; } 
    }
}
