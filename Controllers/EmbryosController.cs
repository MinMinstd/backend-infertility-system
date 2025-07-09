using infertility_system.Data;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbryosController : ControllerBase
    {
        private readonly AppDbContext context;

        public EmbryosController(AppDbContext context)
        {
            this.context = context;
        }


    }
}
