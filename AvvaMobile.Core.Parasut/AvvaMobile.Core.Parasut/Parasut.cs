using System;
using AvvaMobile.Core.Parasut.Models;

namespace AvvaMobile.Core.Parasut
{
    public class Parasut
    {
        public Parasut()
        {
            ParasutBaseDomain = "https://api.parasut.com";
            PrasutBaseUrl = $"{ParasutBaseDomain}/v4/{CompanyID}";
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string CompanyID { get; set; }

        private string ParasutBaseDomain { get; set; }
        private string PrasutBaseUrl { get; set; }

        /// <summary>
        /// Login and gets an auth token from Parasut auth service.
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<GetTokenResponse>> GetTokenAsync()
        {
            var result = new ServiceResult<GetTokenResponse>();

            var nm = new NetworkManager(ParasutBaseDomain);
            nm.AddContentTypeJSONHeader();

            var getTokenRequest = new
            {
                grant_type = "password",
                username = Username,
                password = Password,
                client_id = ClientID,
                client_secret = ClientSecret
            };

            var httpResponse = await nm.PostAsync<GetTokenResponse>("/oauth/token", getTokenRequest);
            if (httpResponse.IsSuccess)
            {
                result.Data = httpResponse.Data;
            }
            else
            {
                result.SetError(httpResponse.Message);
            }

            return result;
        }
    }
}