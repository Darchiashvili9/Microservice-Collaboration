using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Polly;
using Polly.Extensions.Http;
using System;

namespace ApiGatewayMock
{
    public class LoyaltyProgramClient
    {

        private static readonly IAsyncPolicy<HttpResponseMessage> ExponentialRetryPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                                                                                                                      .OrTransientHttpStatusCode()
                                                                                                                      .WaitAndRetryAsync(3, attempt =>
                                                                                                                      {
                                                                                                                          return TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt));
                                                                                                                      });


        private readonly HttpClient httpClient;

        public LoyaltyProgramClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterUser(string name)
        {
            var user = new { name, Settings = new { } };

            return await this.httpClient.PostAsync("/users/", CreateBody(user));
        }

        private static StringContent CreateBody(object user)
        {
            return new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
        }

        public async Task<HttpResponseMessage> QueryUser(string arg)
        {
            return await this.httpClient.GetAsync($"/users/{int.Parse(arg)}");
        }

        public async Task<HttpResponseMessage> UpdateUser(LoyaltyProgramUser user)
        {
            return await this.httpClient.PutAsync($"/users/{user.Id}", CreateBody(user));
        }
    }
}