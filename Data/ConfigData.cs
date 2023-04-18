using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace StrollTracker.Data
{
    public class ConfigData
    {
        public string? MessagePath { get; set; }
        public string? AvatarPath { get; set; }
        public string? ViberToken { get; set; }
        public string? WebhookPath { get; set; }
        public string? UrlForSetMessages { get; set; }
    }
}
