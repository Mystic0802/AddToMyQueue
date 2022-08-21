using AddToMyQueue.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AddToMyQueue.Api.Controllers.SpotifyService
{
    [Route("api/SpotifyService/[controller]")]
    [ApiController]
    public class RequestUserAuthUrlController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _authBaseUrl;
        private readonly string _clientId;
        private readonly string _redirectUrl;
        private readonly string _state;
        private readonly string _scope;
        private readonly bool _showDialog;

        public RequestUserAuthUrlController(IConfiguration configuration)
        {
            _configuration = configuration;

            _authBaseUrl = _configuration["Spotify:Auth:BaseUrl"];
            _clientId = _configuration["Spotify:ClientId"];
            _redirectUrl = _configuration["Spotify:Auth:RedirectUrl"];
            _state = _configuration["Spotify:Auth:State"];
            _scope = _configuration["Spotify:Auth:Scope"].Replace(" ", "%20");
            _showDialog = bool.TryParse(_configuration["Spotify:Auth:ShowDialog"], out bool ShowDialog) && ShowDialog;
        }

        // GET: api/SpotifyService/<RequestUserAuth>
        [HttpGet]
        public string Get()
        {
            var uri = _authBaseUrl + $"/authorize?client_id={_clientId}&response_type=code&redirect_uri={_redirectUrl}";
            uri += _state != "" ? "&state=" + _state : "";
            uri += _scope != "" ? "&scope=" + _scope : "";
            uri += _showDialog ? "&show_dialog=" + _showDialog : "";
            var reponse = new UriResponse()
            { Uri = uri };
            return JsonConvert.SerializeObject(reponse);

        }
    }
}
