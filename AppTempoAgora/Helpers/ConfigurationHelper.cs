using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTempoAgora.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration? _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetApiKey()
        {
            // Fix for CS1061: Use GetSection and Value property instead of GetValue extension method
            var apiKeySection = _configuration?.GetSection("ApiSettings:ApiKey");
            return apiKeySection?.Value ?? string.Empty; // Fix for CS8602: Null-safe dereferencing
        }

        public static string GetBaseUrl()
        {
            // Fix for CS1061: Use GetSection and Value property instead of GetValue extension method
            var apiKeySection = _configuration?.GetSection("ApiSettings:BaseUrl");
            return apiKeySection?.Value ?? string.Empty; // Fix for CS8602: Null-safe dereferencing
        }
    }
}
