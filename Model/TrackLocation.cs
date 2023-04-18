namespace StrollTracker.Model
{
    public class TrackLocation
    {
        public int Id { get; set; }
        public string IMEI { get; set; } = "";
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public DateTime DateEvent { get; set; }
        public DateTime date_track { get; set; }
        public int TypeSource { get; set; }
    }
}
