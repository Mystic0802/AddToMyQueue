using System.Text;

namespace AddToMyQueue.Web.Models
{
    public class SpotifyClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _scope;
        private readonly string _clientId;

        private string state;
        
        private string accessToken;
        private string refreshToken;
        

        public SpotifyClient(HttpClient httpClient, string scope, string clientId)
        {
            _httpClient = httpClient;
            _scope = scope;
            _clientId = clientId;
        }

        public async Task Authenticate()
        {

        }

        public async Task RefreshAccess()
        {

        }

        private string generateState()
        {
            string RandomString()
            {
                string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                int length = 20;

                const int byteSize = 0x100;
                var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
                if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

                // Guid.NewGuid and System.Random are not particularly random. By using a
                // cryptographically-secure random number generator, the caller is always
                // protected, regardless of use.
                using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                {
                    var result = new StringBuilder();
                    var buf = new byte[128];
                    while (result.Length < length)
                    {
                        rng.GetBytes(buf);
                        for (var i = 0; i < buf.Length && result.Length < length; ++i)
                        {
                            // Divide the byte into allowedCharSet-sized groups. If the
                            // random value falls into the last group and the last group is
                            // too small to choose from the entire allowedCharSet, ignore
                            // the value in order to avoid biasing the result.
                            var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                            if (outOfRangeStart <= buf[i]) continue;
                            result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                        }
                    }
                    return result.ToString();
                }
            }


            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());

            state = GuidString;
            return GuidString;
        }
    }
}
