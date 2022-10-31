using AgileWebApi.Data;

namespace AgileWebApi.DataTransferObjects.CaseDTO
{
    public class UpdateCaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ElevatorId { get; set; }
        public int TechnicianId { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public enum CaseStatus
        {
            Started,
            NotStarted,
            Finished
        }
    }
}
