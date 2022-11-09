using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.CaseDTO;
using AgileWebApi.DataTransferObjects.ElevatorDTO;
using AgileWebApi.DataTransferObjects.TechnicialDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace AgileWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaseController(ApplicationDbContext context)
        {
            _context = context;
        }
        //HTTP RESPONSES
        [HttpGet]
        public IActionResult GetAllCases()
        {
            return Ok(_context.Cases.Select(c => new CasesDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Status = c.Status,
                CaseCreated = c.CaseCreated,
                CaseEnded = c.CaseEnded,
                Elevator = new ElevatorDTO()
                {
                    Id = c.Elevator.Id,
                    Name = c.Elevator.Name,
                    Address = c.Elevator.Address,
                    Door = c.Elevator.Door,
                    Reboot = c.Elevator.Reboot,
                    ShutDown = c.Elevator.ShutDown,
                    MaximumWeight = c.Elevator.MaximumWeight,
                    LastInspection = c.Elevator.LastInspection,
                    NextInspection = c.Elevator.NextInspection
                },
                Technician = new TechnicianDTO()
                {
                    Id = c.Technician.Id,
                    Name = c.Technician.Name,
                    Role = c.Technician.Role
                },
                CreatedBy = c.CreatedBy,
                

            }).ToList());
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOneCase(int Id)
        {
            var c = _context.Cases
                .Include(e => e.Elevator)
                .Include(t => t.Technician)
                .Include(c => c.Comments)
                .FirstOrDefault(c => c.Id == Id);
            if (c == null) return NotFound();
            var caseDTO = new CaseDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Comments = c.Comments,
                Status = c.Status,
                CaseCreated = c.CaseCreated,
                CaseEnded = c.CaseEnded,
                Elevator = new ElevatorDTO()
                {
                    Id = c.Elevator.Id,
                    Name = c.Elevator.Name,
                    Address = c.Elevator.Address,
                    Door = c.Elevator.Door,
                    Reboot = c.Elevator.Reboot,
                    ShutDown = c.Elevator.ShutDown,
                    MaximumWeight = c.Elevator.MaximumWeight,
                    LastInspection = c.Elevator.LastInspection,
                    NextInspection = c.Elevator.NextInspection
                },
                Technician = new TechnicianDTO()
                {
                    Id = c.Technician.Id,
                    Name = c.Technician.Name,
                    Role = c.Technician.Role
                }, 
                CreatedBy = c.CreatedBy,
            };
            return Ok(caseDTO);
        }

        [HttpPost]
        public IActionResult CreateCase(CreateCaseDTO createCaseDTO)
        {
            var elevator = _context.Elevators.Find(createCaseDTO.ElevatorId);
            if (elevator == null) return NotFound("ElevatorId was not found.");
            var technician = _context.Technicians.Find(createCaseDTO.TechnicianId);
            if (technician == null) return NotFound("TechnicianId was not found.");
            var Case = new Case()
            {
                Name = createCaseDTO.Name,
                Elevator = elevator,
                Technician = technician,
                Status = createCaseDTO.Status,
                CreatedBy = createCaseDTO.CreatedBy,
                CaseCreated = createCaseDTO.CaseCreated,
                CaseEnded = createCaseDTO.CaseEnded,
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Issue = createCaseDTO.Comment.Issue
                    }
                }
                
            };
            _context.Cases.Add(Case);
            _context.SaveChanges();
            var CaseDTO = new CaseDTO()
            {
                Id = Case.Id,
                Name = Case.Name,
                Status = Case.Status,
                CaseCreated = Case.CaseCreated,
                CaseEnded = Case.CaseEnded,
                Elevator = new ElevatorDTO()
                {
                    Id = elevator.Id,
                    Name = elevator.Name,
                    Address = elevator.Address,
                    Door = elevator.Door,
                    Reboot = elevator.Reboot,
                    ShutDown = elevator.ShutDown,
                    MaximumWeight = elevator.MaximumWeight,
                    LastInspection = elevator.LastInspection,
                    NextInspection = elevator.NextInspection
                },
                Technician = new TechnicianDTO()
                {
                    Id = technician.Id,
                    Name = technician.Name,
                    Role = technician.Role
                },
                Comments = Case.Comments, 
                CreatedBy = Case.CreatedBy
            };
            return CreatedAtAction(nameof(GetOneCase), new {Id = Case.Id}, CaseDTO);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateCase(UpdateCaseDTO caseDto, int Id)
        {
            var Case = _context.Cases
                .Include(e => e.Elevator)
                .Include(t => t.Technician)
                .Include(c => c.Comments)
                .FirstOrDefault(c => c.Id == Id);
            var tech = _context.Technicians.FirstOrDefault(x => x.Id == caseDto.TechnicianId);
            if (Case == null)
            {
                return BadRequest();
            }
            
            Case.Technician = tech;


            Case.Comments.Add(new Comment() { Issue = caseDto.Comment });
            Case.Status = caseDto.Status;
            Case.CaseEnded = caseDto.CaseEnded;
            _context.SaveChanges();
            return NoContent();
        }

    }
}
