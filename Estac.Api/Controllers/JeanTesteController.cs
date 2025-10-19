using Estac.Domain.Interface.Services.Cursos;
using Estac.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
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

    }
}
