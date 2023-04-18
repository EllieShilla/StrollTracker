namespace StrollTracker.Data
{
    public class Distance
    {
        /// <summary>
        /// Сalculates the distance between two points in kilometers.
        /// </summary>
        public static double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371d;
            var dLat = Deg2Rad(lat2 - lat1);
            var dLon = Deg2Rad(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2d) * Math.Sin(dLat / 2d) +
                Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                Math.Sin(dLon / 2d) * Math.Sin(dLon / 2d);
            var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
            var d = R * c;

            return d;
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180d);
        }
    }
}
