using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StrollTracker.DTO;

namespace StrollTracker.Viber
{
    public class UserDataHandler
    {
        private string userObj;
        Dictionary<string, JToken> tokens;

        public string UserObj
        {
            get { return userObj; }
            set { userObj = value; }
        }

        public UserDataHandler()
        {
            tokens = new Dictionary<string, JToken>();
        }

        /// <summary>
        /// Deserialize data that we geted from Viber.
        /// </summary>
        public void DeserializeUserData()
        {
            JObject deserealiseObj = JsonConvert.DeserializeObject<JObject>(UserObj);

            foreach (var token in deserealiseObj)
                tokens.Add(token.Key, token.Value);
        }

        /// <summary>
        /// Parsing deserialized data. 
        /// Created UserDTO object with userId for sending data to specific user, 
        /// messege and EventType for knowing what type of message we getting
        /// </summary>
        public UserDTO HandlerUserObj()
        {
            string userMessage = "";
            string userId = "";
            string eventType = "";

            if (tokens.ContainsKey("message"))
                userMessage = tokens["message"].First.First.Value<String>();
            if (tokens.ContainsKey("sender"))
                userId = tokens["sender"].First.First.Value<String>();
            if (tokens.ContainsKey("user"))
                userId = tokens["user"].First.First.Value<String>();
            if (tokens.ContainsKey("event"))
                eventType = tokens["event"].Value<String>();

            bool isParse = Enum.TryParse(eventType, out EventType type);

            return new UserDTO()
            {
                Id = userId,
                Message = userMessage,
                EventType = type
            };
        }
    }
}
