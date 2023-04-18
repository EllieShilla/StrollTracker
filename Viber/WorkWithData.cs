using StrollTracker.Data;
using StrollTracker.DTO;
using StrollTracker.Interfaces;

namespace StrollTracker.Viber
{
    public class WorkWithData
    {
        private Object userObj;
        private UserDataHandler dataHandler;
        private readonly ITrackLocationRepository _trackingLocationRepository;
        private ConfigData _configData;
        string welcomeMessage = "Добрий день!";

        public WorkWithData(object userObj, ITrackLocationRepository trackLocationRepository, ConfigData configData)
        {
            this.userObj = userObj;
            _trackingLocationRepository = trackLocationRepository;
            this._configData = configData;
        }

        /// <summary>
        /// Deserialize data.
        /// </summary>
        public void WorkWithUserData()
        {
            dataHandler = new UserDataHandler()
            {
                UserObj = Convert.ToString(userObj)
            };
            dataHandler.DeserializeUserData();
        }

        /// <summary>
        /// If type of messege is a conversation_started then we create WelcomeMessage. 
        /// In other case we create MessageHandler for create another type of messages.
        /// </summary>
        public WelcomeMessage SendDataToMessageHandler()
        {
            UserDTO userDTO = dataHandler.HandlerUserObj();
            MessageHandler messageHandler = new MessageHandler(_trackingLocationRepository, userDTO.Id)
            {
                ConfigData = _configData
            };
            if (userDTO.EventType.Equals(EventType.conversation_started))
            {
                return MessageWithText.WelcomeTextMessage(welcomeMessage, _configData.AvatarPath);
            }

            messageHandler.getUserMessage(userDTO.EventType, userDTO.Message);

            return null;
        }
    }
}
