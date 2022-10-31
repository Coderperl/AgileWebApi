using AgileWebApi.Data;

namespace AgileWebApi.DataTransferObjects.CaseDTO
{
    public class CasesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Elevator Elevator { get; set; }
        public Technician Technician { get; set; }
        public enum CaseStatus
        {
            Started,
            NotStarted,
            Finished
        }
    }
}
