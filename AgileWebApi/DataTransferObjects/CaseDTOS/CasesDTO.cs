using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.TechnicialDTO;

namespace AgileWebApi.DataTransferObjects.CaseDTO
{
    public class CasesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ElevatorDTO.ElevatorDTO? Elevator { get; set; }
        public TechnicianDTO? Technician { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
    }
}
