using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessaggiApiController : ControllerBase
    {
        private readonly FotoContext _context;

        public MessaggiApiController(FotoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetMessaggi([FromQuery] string? mex)
        {
            var messaggi = _context.CentroMessaggi
                .Where(m => mex == null || m.TestoMessaggio.ToLower().Contains(mex.ToLower()))
                .ToList();

            return Ok(messaggi);
        }


        //____________________Nuovo Messaggio___________________\\

        [HttpPost]
        public IActionResult NuovoMessaggio(Messaggio mex)
        {
            _context.CentroMessaggi.Add(mex);
            _context.SaveChanges();

            return Ok(mex);
        }

        //____________________Show Messaggio___________________\\

        [HttpGet("{id}")]
        public IActionResult GetMessaggio(int id)
        {

            var mex = _context.CentroMessaggi.FirstOrDefault(m => m.Id == id);

            if (mex is null)
            {
                return NotFound();
            }

            return Ok(mex);
        }

        //____________________Elimina Messaggio___________________\\

        [HttpDelete("{id}")]
        public IActionResult DeleteMessaggio(int id)
        {
            var mexSalvato = _context.CentroMessaggi.FirstOrDefault(m => m.Id == id);

            if (mexSalvato is null)
            {
                return NotFound();
            }

            _context.CentroMessaggi.Remove(mexSalvato);
            _context.SaveChanges();

            return Ok();
        }

    }
}
