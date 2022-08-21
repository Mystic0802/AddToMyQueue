namespace AddToMyQueue.Web.Models.Response
{
    public class AccessTokenReponse
    {
        // Stoopid jsonConvert did not want want to accept the typical naming conventions like every other model used...
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
}
