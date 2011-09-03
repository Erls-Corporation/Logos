namespace Logos.Domain.Core
{
    public class Credentials
    {
        readonly Username _username;
        readonly ApiToken _apiToken;

        public Credentials(Username username, ApiToken apiToken)
        {
            _username = username;
            _apiToken = apiToken;
        }

        public string Username
        {
            get
            {
                return _username.Value;
            }
        }

        public string ApiToken
        {
            get
            {
                return _apiToken.Value;
            }
        }
    }
}