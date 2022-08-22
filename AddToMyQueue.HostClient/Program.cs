using Newtonsoft.Json;

namespace AddToMyQueue.HostClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press enter to request auth...");
            Console.ReadKey();
            await RequestUserAuth();

            await Task.Delay(-1);
        }

        /// <summary>
        /// Sends User authorization request to spotify.
        /// </summary>
        private static async Task RequestUserAuth()
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7268") };
            var endpointUrl = "api/SpotifyService/RequestUserAuthUrl";

            var response = await httpClient.GetAsync(endpointUrl);
            var obj = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new { Uri = "" });

            var uri = obj?.Uri;
            if (uri == null || uri == string.Empty)
                return;

            // Open browser for authentication...
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }
    }
}