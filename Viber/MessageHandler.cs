using StrollTracker.Data;
using StrollTracker.DTO;
using StrollTracker.Interfaces;

namespace StrollTracker.Viber
{
    public class MessageHandler
    {
        private readonly ITrackLocationRepository _trackingLocationRepository;
        private string userId;
        private static string? imei;
        private ConfigData? configData;
        public ConfigData ConfigData
        {
            get { return configData; }
            set { configData = value; }
        }

        public MessageHandler(ITrackLocationRepository trackLocationRepository, string userId)
        {
            _trackingLocationRepository = trackLocationRepository;
            this.userId = userId;
        }

        /// <summary>
        /// Processing data by event type.
        /// </summary>
        public void getUserMessage(EventType type, string messageFromUser = "")
        {
            switch (type)
            {
                case EventType.subscribed:
                    {
                        MessageSender messageSender = new MessageSender(ConfigData.MessagePath, ConfigData.ViberToken)
                        {
                            AvatarPath = ConfigData.AvatarPath,
                            UserId = userId
                        };
                        messageSender.SendMessage("Щоб отримати інформацію про прогулянки введіть: imei #номер\nНаприклад: imei 12345");
                    }
                    break;
                case EventType.message:
                    {
                        MessageSender messageSender = new MessageSender(ConfigData.MessagePath, ConfigData.ViberToken)
                        {
                            AvatarPath = ConfigData.AvatarPath,
                            UserId = userId
                        };
                        if (messageFromUser.ToLower().Contains("imei"))
                        {
                            imei = messageFromUser.Replace("imei ", "");

                            if (_trackingLocationRepository.isIMEI_Exist(imei) == false)
                            {
                                messageSender.SendMessage("Введеного  IMEI не існує");
                                break;
                            }
                            else
                            {
                                AmountOfWalking data = _trackingLocationRepository.GetAmountOfWalkingForAllTime(imei);
                                string setInfo = StringMessage.GetAmountStrollString(data.CountOfWalking.ToString(),
                                                                       data.AmountOfDistance.ToString("#.##"),
                                                                       data.AmountOfTime.TotalMinutes.ToString("#.##"));

                                messageSender.SendButton("Топ 10 прогулянок", setInfo);
                                break;
                            }
                        }
                        if (messageFromUser.Equals("Топ 10 прогулянок"))
                        {
                            if (_trackingLocationRepository.isIMEI_Exist(imei))
                            {
                                List<StrollWithName> strollWithNames = _trackingLocationRepository.GetTop10Walking(imei);

                                string setInfo = $"Топ 10 прогулянок:\n";

                                foreach (var stroll in strollWithNames)
                                {
                                    setInfo += StringMessage.GetTopStrollString(stroll.Name,
                                                                  stroll.Distance.ToString("#.##"),
                                                                  stroll.TimeOfWalking.TotalMinutes.ToString("#.##"));
                                }
                                messageSender.SendMessage(setInfo);
                            }
                        }
                    }
                    break;
                default: break;
            }
        }
    }
}
