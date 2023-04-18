using Microsoft.AspNetCore.Components.Web;

namespace StrollTracker.Data
{
    public class WelcomeMessage
    {
        public SenderForWelcomeMessage? sender { get; set; }
        public string? tracking_data { get; set; }
        public string? type { get; set; }
        public string? text { get; set; }
        public string? media { get; set; }
        public string? thumbnail { get; set; }
    }

}
