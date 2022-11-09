using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.CaseDTO;
using AgileWebApi.DataTransferObjects.ElevatorDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgileWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElevatorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var elevators = _context.Elevators.ToList().Select(a => new ElevatorDTO
            {
                Id = a.Id,
                Name = a.Name,
                Address = a.Address,
                MaximumWeight = a.MaximumWeight,
                LastInspection = a.LastInspection,
                NextInspection = a.NextInspection,
                Reboot = a.Reboot,
                ShutDown = a.ShutDown,
                Door = a.Door,
                Floor = a.Floor,
                ElevatorStatus = a.ElevatorStatus
            });

            return Ok(elevators);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            var elevator = _context.Elevators.FirstOrDefault(a => a.Id == id); 
            if(elevator == null) return NotFound();
            
            var elevatorDTo = new ElevatorDTO()
            {
                Id = elevator.Id,
                Name = elevator.Name,
                Address = elevator.Address,
                MaximumWeight = elevator.MaximumWeight,
                LastInspection = elevator.LastInspection,
                NextInspection = elevator.NextInspection,
                Reboot = elevator.Reboot,
                ShutDown = elevator.ShutDown,
                Door = elevator.Door,
                Floor = elevator.Floor,
                ElevatorStatus = elevator.ElevatorStatus
            };

            return Ok(elevator);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateElevator(UpdateElevatorDTO elevatorDto, int Id)
        {
            var elevator = _context.Elevators.FirstOrDefault(a => a.Id == Id);
            if(elevator ==null) return BadRequest();

            elevator.Address = elevatorDto.Address;
            elevator.MaximumWeight = elevatorDto.MaximumWeight;
            elevator.Reboot = elevatorDto.Reboot;
            elevator.ShutDown = elevator.ShutDown;
            elevator.Door = elevatorDto.Door;
            elevator.Floor = elevatorDto.Floor;
            elevator.LastInspection = elevatorDto.LastInspection;
            elevator.NextInspection = elevatorDto.NextInspection;
            elevator.Name = elevatorDto.Name;
            elevator.ElevatorStatus = elevatorDto.ElevatorStatus;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
