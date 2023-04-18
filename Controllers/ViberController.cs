using Microsoft.AspNetCore.Mvc;
using StrollTracker.Viber;
using StrollTracker.Interfaces;
using StrollTracker.Services;
using StrollTracker.Data;
using Microsoft.AspNetCore.Http;

namespace StrollTracker.Controllers
{
    public class ViberController : BaseController
    {
        private readonly ITrackLocationRepository _trackingLocationRepository;
        private readonly IConfiguration _configuration;
        private ConfigData _config;

        public ViberController(ITrackLocationRepository trackLocationRepository, IConfiguration configuration)
        {
            _trackingLocationRepository = trackLocationRepository;
            _configuration = configuration;
            _config = new ConfigData()
            {
                AvatarPath = _configuration.GetValue<string>("Viber:AvatarPath"),
                UrlForSetMessages = _configuration.GetValue<string>("Viber:UrlForSetMessages"),
                MessagePath = _configuration.GetValue<string>("Viber:MessagePath"),
                WebhookPath = _configuration.GetValue<string>("Viber:WebhookPath"),
                ViberToken = _configuration.GetValue<string>("Viber:ViberToken")
            };
        }

        /// <summary>
        /// Accepting messages from Viber.
        /// If WelcomeMessage object is not zero, it means that a message is a conversation_started type. 
        /// </summary>
        [HttpPost()]
        public ActionResult Post(object data)
        {
            WorkWithData workWithData = new WorkWithData(data, _trackingLocationRepository, _config);
            workWithData.WorkWithUserData();
            WelcomeMessage obj = workWithData.SendDataToMessageHandler();
            if (obj != null)
                return Ok(obj);

            return Ok();
        }

        /// <summary>
        /// Sending a webhook for starting work with Viber.
        /// </summary>
        [HttpPost("startWork")]
        public ActionResult<string> StartWork()
        {
            ViberWebhookServices viberWebhookServices = new ViberWebhookServices()
            {
                UrlForSetMessage = _config.UrlForSetMessages,
                ViberToken = _config.ViberToken,
                WebhookPath = _config.WebhookPath,
            };

            return Ok(viberWebhookServices.Send());
        }
    }
}
