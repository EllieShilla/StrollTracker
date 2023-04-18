using Microsoft.AspNetCore.Mvc;
using StrollTracker.Data;
using StrollTracker.DTO;
using StrollTracker.Model;

namespace StrollTracker.Interfaces
{
    public interface ITrackLocationRepository
    {
        List<Stroll> GetStrollList(string imei);
        List<StrollWithName> GetTop10Walking(string imei);
        AmountOfWalking GetAmountOfWalkingForAllTime(string imei);
        List<StrollByDay> GetAmountOfWalkingByDays(string imei);
        bool isIMEI_Exist(string imei);

    }
}
