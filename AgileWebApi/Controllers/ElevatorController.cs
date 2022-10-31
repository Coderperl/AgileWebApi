using AgileWebApi.Data;
using Microsoft.AspNetCore.Mvc;

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
        //HTTP RESPONSES



    }
}
