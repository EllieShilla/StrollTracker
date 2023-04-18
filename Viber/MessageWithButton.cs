using StrollTracker.Data;

namespace StrollTracker.Viber
{
    public class MessageWithButton
    {
        private string userId;

        public MessageWithButton(string userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Create message with keyboard.
        /// <param name="buttonText">text that user will see in a button</param>
        /// <param name="message">message that will send with button.</param>
        /// </summary>
        public Keyboard CreateTop10StrollButton(string buttonText, string message)
        {
            return new Keyboard()
            {
                receiver = userId,
                type = "text",
                text = message,
                keyboard = new Keys()
                {
                    Type = "keyboard",
                    DefaultHeight = false,
                    Buttons = new Button[]
                    {
                        new Button()
                        {
                            Text = buttonText,
                            TextSize = "regular",
                            ActionBody=buttonText
                        }
                    }
                }
            };
        }
    }
}
