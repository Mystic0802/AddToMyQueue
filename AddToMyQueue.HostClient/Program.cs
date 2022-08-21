using AddToMyQueue.HostClient.Models.Response;
using AddToMyQueue.HostClient.Services;

namespace AddToMyQueue.HostClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press enter to request auth...");
            Console.ReadKey();
            await RequestUserAuth();

            Console.WriteLine("Press enter to request access...");
            Console.ReadKey();
            await RequestAccessToken();

            await Task.Delay(-1);
        }

        /// <summary>
        /// Sends User authorization request to spotify.
        /// </summary>
        private static async Task RequestUserAuth()
        {
            var authService = new AuthService(new HttpClient(), "https://localhost:7268");
            var response = await authService.GetAsync<UserAuthUrlResponse>("api/SpotifyService/RequestUserAuthUrl");
            var url = response.payload?.Uri;
            if (url == null || url == string.Empty)
                return;

            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = url;
            System.Diagnostics.Process.Start(psi);
        }

        private static async Task RequestAccessToken()
        {

        }
    }
}