using AgileWebApi.Data;
using Microsoft.AspNetCore.Mvc;

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


    }
}
