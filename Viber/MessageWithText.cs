using StrollTracker.Data;

namespace StrollTracker.Viber
{
    public class MessageWithText
    {
        private string? avatarPath;
        public string AvatarPath
        {
            get { return avatarPath; }
            set { avatarPath = value; }
        }

        /// <summary>
        /// Create message that user will see when open bot for the first time.
        /// </summary>
        /// <param name="userId">for finding and sending message to one specific user.</param>
        /// <param name="message">text that user will see in the message.</param>
        public TextMessage CreateTextMessage(string userId, string message)
        {
            return new TextMessage()
            {
                receiver = userId,
                sender = new Sender()
                {
                    name = "StrollTracker",
                    avatar = AvatarPath
                },
                tracking_data = "tracking data",
                type = "text",
                text = message
            };
        }

        /// <summary>
        /// Create message that user will see when open bot for the first time.
        /// </summary>
        /// <param name="message">text that user will see in the message.</param>
        /// <param name="avatarPath">picture that user will see in the message.</param>
        public static WelcomeMessage WelcomeTextMessage(string message, string avatarPath)
        {
            return new WelcomeMessage()
            {
                sender = new SenderForWelcomeMessage()
                {
                    name = "StrollTracker",
                    avatar = avatarPath
                },
                tracking_data = "tracking data",
                type = "picture",
                text = message,
                media = avatarPath,
                thumbnail = avatarPath
            };
        }
    }
}
