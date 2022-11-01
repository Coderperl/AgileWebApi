using AgileWebApi.Data;
using AgileWebApi.DataTransferObjects.TechnicialDTO;
using Microsoft.AspNetCore.Mvc;

namespace AgileWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnicianController(ApplicationDbContext context)
        {
            _context = context;
        }
        //HTTP RESPONSES
        [HttpGet  ]
        [Route("{id}")]
        public IActionResult GetOneTechnician(int id)
        {
            var tech = _context.Technicians.FirstOrDefault(technician => technician.Id == id);
            if (tech == null)
                return NotFound();
            var result = new TechnicianDTO()
            {
                Id = tech.Id,
                Name = tech.Name,
                Role = tech.Role,
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllTechnicians()
        {
            return Ok(_context.Technicians.Select(technician => new TechniciansDTO()
            {
                Name = technician.Name,
                Id = technician.Id,
                Role = technician.Role
            }).ToList());
        }

    }
}
