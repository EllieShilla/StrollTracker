namespace StrollTracker.Data
{
    public class Sender
    {
        public string? language { get; set; }
        public string? country { get; set; }
        public int api_versuion { get; set; }

        public int id { get; set; }
        public string? name { get; set; }
        public string? avatar { get; set; }
    }

    public class SenderForWelcomeMessage
    {
        public string? name { get; set; }
        public string? avatar { get; set; }
    }
}
