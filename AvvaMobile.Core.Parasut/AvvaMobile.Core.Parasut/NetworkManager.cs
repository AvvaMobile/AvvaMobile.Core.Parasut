using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AvvaMobile.Core
{
    public class HttpResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class HttpResponse<T> : HttpResponse
    {
        public T Data { get; set; }
    }

    public class NetworkManager
    {
        readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public NetworkManager()
        {

        }

        public NetworkManager(string baseAddress)
        {
            SetBaseAddress(baseAddress);
        }

        /// <summary>
        /// Updates the base address of http client.
        /// </summary>
        /// <param name="baseAddress"></param>
        public void SetBaseAddress(string baseAddress)
        {
            client.BaseAddress = new Uri(baseAddress);
        }

        /// <summary>
        /// Clears all existing header from the client.
        /// </summary>
        public void ClearHeaders()
        {
            client.DefaultRequestHeaders.Clear();
        }

        /// <summary>
        /// Adds new header value to the client request.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddHeader(string name, string value)
        {
            client.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// Adds Bearer token to header.
        /// </summary>
        /// <param name="token"></param>
        public void AddBearerToken(string token)
        {
            AddHeader("Authorization", $"Bearer {token}");
        }

        /// <summary>
        /// Adds "ContentType:application/json" header to current request.
        /// </summary>
        public void AddContentTypeJSONHeader()
        {
            AddHeader("ContentType", "application/json");
        }

        /// <summary>
        /// Sends a GET request and returns data as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> GetAsync<T>(string uri)
        {
            return await GetAsync<T>(uri, null);
        }

        /// <summary>
        /// Sends a GET request with url parameters and returns data as T type.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> GetAsync<T>(string uri, Dictionary<string, string> parameters)
        {
            var response = new HttpResponse<T>();

            try
            {
                var sb = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var param in parameters)
                    {
                        sb.Append(param.Key);
                        sb.Append("=");
                        sb.Append(param.Value);
                        sb.Append("&");
                    }
                }
                if (uri.IndexOf("?") > -1)
                {
                    uri = $"{client.BaseAddress}{uri}&{sb}";
                }
                else
                {
                    uri = $"{client.BaseAddress}{uri}?{sb}";
                }

                var resp = await client.GetAsync(uri);
                response.IsSuccess = resp.IsSuccessStatusCode;
                response.StatusCode = resp.StatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.GetAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a GET request with url parameters and returns data as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<HttpResponse<string>> GetAsync(string uri, Dictionary<string, string> parameters)
        {
            var response = new HttpResponse<string>();

            try
            {
                var sb = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var param in parameters)
                    {
                        sb.Append(param.Key);
                        sb.Append("=");
                        sb.Append(param.Value);
                        sb.Append("&");
                    }
                }
                if (uri.IndexOf("?") > -1)
                {
                    uri = $"{client.BaseAddress}{uri}&{sb}";
                }
                else
                {
                    uri = $"{client.BaseAddress}{uri}?{sb}";
                }

                var resp = await client.GetAsync(uri);
                response.IsSuccess = resp.IsSuccessStatusCode;
                response.StatusCode = resp.StatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = responseString;
                }
                else
                {
                    response.Message = responseString;
                }
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.GetAsync Error: " + ex.Message;
            }

            return response;
        }

        public async Task<HttpResponse<string>> GetXMLStringAsync(string uri, Dictionary<string, string> parameters)
        {
            var response = new HttpResponse<string>();

            try
            {
                var sb = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var param in parameters)
                    {
                        sb.Append(param.Key);
                        sb.Append("=");
                        sb.Append(param.Value);
                        sb.Append("&");
                    }
                }
                if (uri.IndexOf("?") > -1)
                {
                    uri = $"{client.BaseAddress}{uri}&{sb}";
                }
                else
                {
                    uri = $"{client.BaseAddress}{uri}?{sb}";
                }

                var resp = await client.GetAsync(uri);
                response.IsSuccess = resp.IsSuccessStatusCode;
                response.StatusCode = resp.StatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = responseString;
                }
                else
                {
                    response.Message = responseString;
                }
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.GetAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a POST request with data object. Also returns http response as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> PostAsync<T>(string uri, dynamic data)
        {
            var response = new HttpResponse<T>();

            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = await client.PostAsync($"{client.BaseAddress}{uri}", content);
                response.IsSuccess = resp.IsSuccessStatusCode;
                response.StatusCode = resp.StatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.PostAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a PUT request with data object. Also returns http response as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> PutAsync<T>(string uri, dynamic data)
        {
            var response = new HttpResponse<T>();

            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = await client.PutAsync($"{client.BaseAddress}{uri}", content);
                response.IsSuccess = resp.IsSuccessStatusCode;
                response.StatusCode = resp.StatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.PutAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a POST request with form data. Also returns http response as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> PostAsFormDataAsync<T>(string uri, MultipartFormDataContent content)
        {
            var response = new HttpResponse<T>();
            try
            {
                var resp = await client.PostAsync($"{client.BaseAddress}{uri}", content);
                response.IsSuccess = resp.IsSuccessStatusCode;
                response.StatusCode = resp.StatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.PostAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a DELETE request with data object. Also returns http response as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> DeleteAsync<T>(string uri)
        {
            var response = new HttpResponse<T>();

            try
            {
                var resp = await client.DeleteAsync(uri);
                response.IsSuccess = resp.IsSuccessStatusCode;
                response.StatusCode = resp.StatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.DeleteAsync Error: " + ex.Message;
            }

            return response;
        }
    }
}