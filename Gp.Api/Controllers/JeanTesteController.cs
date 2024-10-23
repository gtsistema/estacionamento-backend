using Gp.Domain.Interface.Services.Cursos;
using Gp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JeanTesteController : ControllerBase
    {
        private readonly ILogger<JeanTesteController> _logger;
        private readonly ILivroServices _services;

        public JeanTesteController(ILogger<JeanTesteController> logger, ILivroServices _services)
        {
            _logger = logger;
            this._services = _services;
        }

        [HttpGet]
        public async Task<IEnumerable<Livro>> Get()
        {
            return  await _services.GetallTeste();
        }
    }
}
