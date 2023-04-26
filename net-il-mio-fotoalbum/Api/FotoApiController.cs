using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoApiController : ControllerBase
    {

        private readonly FotoContext _context;

        public FotoApiController(FotoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetFotos([FromQuery] string? title)
        {
            var fotos = _context.Fotos.
                Where(f => title == null || f.Title.ToLower().Contains(title.ToLower())).ToList();

            return Ok(fotos);
        }
    }
}
