using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.TechnicialDTO;
using AgileWebApi.DataTransferObjects.ElevatorDTO;


namespace AgileWebApi.DataTransferObjects.CaseDTO
{
    public class CaseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ElevatorDTO.ElevatorDTO? Elevator { get; set; }
        public TechnicianDTO? Technician { get; set; }
        public List<Comment>? Comments { get; set; }
        public string Status { get; set; }
    }
}
