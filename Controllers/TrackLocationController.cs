using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrollTracker.Data;
using StrollTracker.DTO;
using StrollTracker.Interfaces;
using StrollTracker.Model;
using System.Net;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace StrollTracker.Controllers
{
    public class TrackLocationController : BaseController
    {
        private readonly ITrackLocationRepository _trackingLocationRepository;
        public TrackLocationController(ITrackLocationRepository trackLocationRepository)
        {
            _trackingLocationRepository = trackLocationRepository;
        }

        [HttpGet("tracksByIMEI")]
        public ActionResult<List<Stroll>> Tracks(string imei)
        {
            return _trackingLocationRepository.GetStrollList(imei);
        }

        [HttpGet("getAmountOfWalking")]
        public ActionResult<AmountOfWalking> GetAmountOfWalking(string imei)
        {
            return _trackingLocationRepository.GetAmountOfWalkingForAllTime(imei);
        }

        [HttpGet("walkingByDays")]
        public ActionResult<List<StrollByDay>> GetAmountOfWalkingByDays(string imei)
        {
            return _trackingLocationRepository.GetAmountOfWalkingByDays(imei);
        }


    }
}
