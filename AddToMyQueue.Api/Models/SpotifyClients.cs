using Microsoft.Extensions.Logging;

namespace AddToMyQueue.Api.Models
{
    public interface ISpotifyClients
    {
        void AddClient(string userId, SpotifyClient client);
        SpotifyClient? GetClient(string userId);
        bool Contains(string userId);
        void RemoveClient(string userId);
    }

    public class SpotifyClients : ISpotifyClients
    {
        private readonly ILogger _logger;

        private readonly Dictionary<string, SpotifyClient> _clients;

        public SpotifyClients(/*ILogger logger*/)
        {
            _clients = new();
            //_logger = logger;
        }

        /// <summary>
        /// Adds a client. If a client with the same userId exists, it is not added.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="client"></param>
        public void AddClient(string userId, SpotifyClient client)
        {
            if (_clients.ContainsKey(userId))
                return;
            else
                _clients.Add(userId, client);
        }

        /// <summary>
        /// Trys to get the spotifyClient with the associated <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="SpotifyClient"/> if found; otherwise <see langword="Null"/></returns>
        public SpotifyClient? GetClient(string userId)
        {
            _clients.TryGetValue(userId, out SpotifyClient? client);
            return client;
        }

        public bool Contains(string userId)
        {
            return _clients.ContainsKey(userId);
        }

        public void RemoveClient(string userId)
        {
            _clients.Remove(userId);
            //if (!_clients.Remove(userId))
                //_logger.LogWarning("Failed to remove {userId} from clients dictionary", userId);
        }
    }
}
