namespace StrollTracker.DTO
{
    public class StrollByDay
    {
        public DateOnly Date { get; set; }
        public double Distance { get; set; }
        public TimeSpan TimeOfWalking { get; set; }
    }
}
