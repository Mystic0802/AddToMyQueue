using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AddToMyQueue.Api.Models
{
    public class SpotifyApiData
    {
        private readonly IConfiguration _configuration;

        public SpotifyApiData(IConfiguration configuration)
        {
            _configuration = configuration;
            
            ApiBaseUrl = _configuration["Spotify:BaseApiUrl"];
            AuthBaseUrl = _configuration["Spotify:Auth:BaseUrl"];
            ClientId = _configuration["Spotify:ClientId"];
            ClientSecret = _configuration["Spotify:ClientSecret"];
            Scopes = _configuration["Spotify:Auth:Scope"];
            RedirectUrl = _configuration["Spotify:Auth:RedirectUrl"];
            // Make default true
            if (!bool.TryParse(_configuration["Spotify:Auth:ShowDialog"], out bool result))
                result = true;
            ShowDialog = result;
            var plainTextBytes = Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret);
            AuthHeader = Convert.ToBase64String(plainTextBytes);
        }

        public string ApiBaseUrl { get; }
        public string AuthBaseUrl { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string Scopes { get; }
        public string State { get => GenerateState(); }
        public string RedirectUrl { get; }
        public bool ShowDialog { get; }
        public string AuthHeader { get; }

        private string GenerateState()
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 20;

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            using var rng = RandomNumberGenerator.Create();
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
}
