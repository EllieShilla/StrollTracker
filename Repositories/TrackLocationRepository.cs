using StrollTracker.Data;
using StrollTracker.DTO;
using StrollTracker.Interfaces;
using StrollTracker.Model;

namespace StrollTracker.Repositories
{
    public class TrackLocationRepository : ITrackLocationRepository
    {
        private readonly DataContext _context;
        public TrackLocationRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all item by imei and OrderBy date_track.
        /// </summary>
        private List<TrackLocation> GetTracksByIMEIOrderByDateTrack(string imei)
        {
            return _context.TrackLocation.Where(i => i.IMEI.Equals(imei)).OrderBy(i => i.date_track).ToList();
        }

        /// <summary>
        /// Each item in the List of TrackLocation is compared by date_track. 
        /// If date_track firstTrack is more than currentTrack for 30 minutes or more, than created new Stroll object.
        /// The distance walked and the time walked during the day are also counted and saved in StrollByDay object.
        /// </summary>
        public List<Stroll> GetStrollList(string imei)
        {
            List<TrackLocation> trackbyIMEI = GetTracksByIMEIOrderByDateTrack(imei);

            int countOfStrolls = 0;
            TrackLocation firstTrack = trackbyIMEI[0];
            TimeSpan minutes = TimeSpan.FromMinutes(30.0);
            List<Stroll> strolls = new List<Stroll>();
            double distance = 0;
            TimeSpan strollTime = TimeSpan.FromMinutes(0.0);

            foreach (TrackLocation track in trackbyIMEI)
            {
                TimeSpan timeDeduction = track.date_track.Subtract(firstTrack.date_track);
                int timeCompare = timeDeduction.CompareTo(minutes);

                if (timeCompare >= 0)
                {
                    countOfStrolls++;
                    strolls.Add(new Stroll() { Title = "Stroll_" + countOfStrolls, Distance = distance, StrollTime = strollTime });
                    resetData(out distance, out strollTime, out timeDeduction);
                    firstTrack = track;
                }

                distance += Distance.GetDistanceFromLatLonInKm(Convert.ToDouble(firstTrack.latitude),
                                                               Convert.ToDouble(firstTrack.longitude),
                                                               Convert.ToDouble(track.latitude),
                                                               Convert.ToDouble(track.longitude));

                strollTime += timeDeduction;
                firstTrack = track;
            }
            return strolls;
        }

        /// <summary>
        /// Counting of data on walks for all time on a particular imei.
        /// </summary>
        public AmountOfWalking GetAmountOfWalkingForAllTime(string imei)
        {
            List<Stroll> strolls = new List<Stroll>();
            strolls.AddRange(GetStrollList(imei));

            AmountOfWalking amount = new AmountOfWalking()
            {
                CountOfWalking = strolls.Count,
                AmountOfDistance = strolls.Sum(s => s.Distance),
                AmountOfTime = TimeSpan.FromMinutes(strolls.Sum(i => i.StrollTime.TotalMinutes))
            };

            return amount;
        }

        /// <summary>
        /// Cleaning data.
        /// </summary>
        private void resetData(out double distance, out TimeSpan strollTime, out TimeSpan timeDeduction)
        {
            distance = 0;
            strollTime = TimeSpan.FromMinutes(0.0);
            timeDeduction = TimeSpan.FromMinutes(0.0);
        }

        /// <summary>
        /// Each item in the List of TrackLocation is compared by date. 
        /// If the day of the firstTrack and the currentTrack one are different created a new StrollByDay object.
        /// The distance walked and the time walked during the day are also counted and saved in StrollByDay object.
        /// </summary>
        public List<StrollByDay> GetAmountOfWalkingByDays(string imei)
        {
            List<TrackLocation> trackbyIMEI = GetTracksByIMEIOrderByDateTrack(imei);

            TrackLocation firstTrack = trackbyIMEI[0];
            List<StrollByDay> strollByDays = new List<StrollByDay>();
            double distance = 0;
            TimeSpan strollTime = TimeSpan.FromMinutes(0.0);

            foreach (TrackLocation currentTrack in trackbyIMEI)
            {
                TimeSpan timeDeduction = currentTrack.date_track.Subtract(firstTrack.date_track);

                if (firstTrack.date_track.Date != currentTrack.date_track.Date)
                {
                    var date = firstTrack.date_track.Date;
                    strollByDays.Add(new StrollByDay()
                    {
                        Date = DateOnly.FromDateTime(firstTrack.date_track.Date),
                        TimeOfWalking = strollTime,
                        Distance = distance,
                    });
                    resetData(out distance, out strollTime, out timeDeduction);
                    firstTrack = currentTrack;

                }

                distance += Distance.GetDistanceFromLatLonInKm(Convert.ToDouble(firstTrack.latitude),
                                               Convert.ToDouble(firstTrack.longitude),
                                               Convert.ToDouble(currentTrack.latitude),
                                               Convert.ToDouble(currentTrack.longitude));

                strollTime += timeDeduction;
                firstTrack = currentTrack;
            }


            return strollByDays;
        }

        /// <summary>
        /// List of Strolls OrderByDescending by Distance and take first 10 result.
        /// </summary>
        public List<StrollWithName> GetTop10Walking(string imei)
        {
            List<Stroll> strolls = new List<Stroll>();
            strolls.AddRange(GetStrollList(imei));
            List<Stroll> topStrolls = strolls.OrderByDescending(i => i.Distance).Take(10).ToList();

            List<StrollWithName> strollWithsName = new List<StrollWithName>();

            foreach (Stroll track in topStrolls)
            {
                strollWithsName.Add(new StrollWithName()
                {
                    Name = track.Title,
                    Distance = track.Distance,
                    TimeOfWalking = track.StrollTime
                });
            }

            return strollWithsName;
        }

        /// <summary>
        /// Checking the existence of the specified imei.
        /// </summary>
        public bool isIMEI_Exist(string imei)
        {
            if (_context.TrackLocation.FirstOrDefault(i => i.IMEI.Equals(imei)) != null)
                return true;

            return false;
        }
    }
}

