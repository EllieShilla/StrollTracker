using StrollTracker.Viber;

namespace StrollTracker.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = "";
        public string Message { get; set; } = "";
        public EventType EventType { get; set; }
    }
}
