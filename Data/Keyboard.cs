namespace StrollTracker.Data
{
    public class Keyboard
    {
        public string? receiver { get; set; }
        public string? type { get; set; }
        public string? text { get; set; }
        public Keys? keyboard { get; set; }
        public bool DefaultHeight { get; set; }
        public string? BgColor { get; set; }
    }

    public class Keys
    {
        public string? Type { get; set; }
        public bool DefaultHeight { get; set; }
        public Button[]? Buttons { get; set; }

    }

    public class Button
    {
        public string? Text { get; set; }
        public string? TextSize { get; set; }
        public string? ActionBody { get; set; }
    }
}
