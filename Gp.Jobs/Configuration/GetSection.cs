using Microsoft.Extensions.Configuration;
using System;

namespace Gp.Jobs.Configuration
{
    public class GetSection
    {
        private readonly IConfiguration _configuration;

        public GetSection(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string Get(string name, string property)
        {
            return _configuration.GetSection($"{name}:{property}")?.Value;
        }

        public string GetProtheus(string property)
        {
            return _configuration.GetSection($"Protheus:{property}")?.Value;
        }
    }
}
