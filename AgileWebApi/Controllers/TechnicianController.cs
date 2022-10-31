using AgileWebApi.Data;
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

    }
}
