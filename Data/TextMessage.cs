namespace StrollTracker.Data
{
    public class TextMessage
    {
        public string? receiver { get; set; }
        public Sender sender { get; set; }
        public string? tracking_data { get; set; }
        public string? type { get; set; }
        public string? text { get; set; }
    }
}
