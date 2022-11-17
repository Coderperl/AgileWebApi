using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.CaseDTO;
using AgileWebApi.DataTransferObjects.ElevatorDTO;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AgileWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ElevatorController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                MaxFloor = a.MaxFloor,
                MinFloor = a.MinFloor,
                ElevatorType = a.ElevatorType,
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
                MaxFloor = elevator.MaxFloor,
                MinFloor = elevator.MinFloor,
                ElevatorType = elevator.ElevatorType,
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
            elevator.ShutDown = elevatorDto.ShutDown;
            elevator.Door = elevatorDto.Door;
            elevator.Floor = elevatorDto.Floor;
            elevator.LastInspection = elevatorDto.LastInspection;
            elevator.NextInspection = elevatorDto.NextInspection;
            elevator.Name = elevatorDto.Name;
            elevator.MaxFloor = elevatorDto.MaxFloor;
            elevator.MinFloor = elevatorDto.MinFloor;
            elevator.ElevatorType = elevatorDto.ElevatorType;
            elevator.ElevatorStatus = elevatorDto.ElevatorStatus;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        [Route("/api/Create")]
        public async Task<IActionResult> GetConnectionStringAsync()
        {
            var deviceId = "elevatorDevice";
            Device device;
            var connectionstring = "";
            bool exist = false;

            using var registryManager = RegistryManager.CreateFromConnectionString(_configuration.GetConnectionString("IoTHub"));
            var result = registryManager.CreateQuery($"SELECT * FROM devices");          

            if (result.HasMoreResults)
            {
                foreach (var twin in await result.GetNextAsTwinAsync())
                {                    
                    deviceId = twin.DeviceId;
                    device = await registryManager.GetDeviceAsync(deviceId);
                    connectionstring = $"{_configuration.GetConnectionString("IoTHub").Split(";")[0]};DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}";
                    exist = true;
                    return new OkObjectResult(connectionstring);
                }
                if (exist == false)
                {
                    device = await registryManager.AddDeviceAsync(new Device(deviceId));
                    connectionstring = $"{_configuration.GetConnectionString("IoTHub").Split(";")[0]};DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}";
                    return new OkObjectResult(connectionstring);
                }
            }
            return BadRequest("Empty");
        }
    }
}
