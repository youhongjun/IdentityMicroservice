using System;
using Xunit;

using System.Threading.Tasks;
using System.Net.Http;
using IdentityModel.Client;

namespace IdentityMicroserviceTests
{
    public class UnitTest1
    {
        private async Task<TokenResponse> GetClientCredentialsToken()
        {
            // request token
            var client = new HttpClient();
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:6000/connect/token",

                ClientId = "myapi.service",
                ClientSecret = "myapisecret",
                Scope = "myapiservice"
            });

            return response;
        }

        private async Task<TokenResponse> GetPasswordToken()
        {
            // request token
            var client = new HttpClient();
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:6000/connect/token",

                ClientId = "agent.api.service",
                ClientSecret = "agentsecret",
                Scope = "agentservice clientservice productservice",

                UserName = "edison@gmail.com",
                Password = "edisonpassword"
            });

            return response;
        }

        [Fact]
        public async Task Client_API_Password_Test1()
        {
            // request token
            var response = await GetPasswordToken();

            Assert.False(response.IsError);
            Console.WriteLine(response.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(response.AccessToken);
            var apiResponse = await apiClient.GetAsync("http://localhost:6011/api/values");
            Assert.True(apiResponse.IsSuccessStatusCode);
            var content = await apiResponse.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        [Fact]
        public async Task Product_API_Password_Test1()
        {
            // request token
            var response = await GetPasswordToken();

            Assert.False(response.IsError);
            Console.WriteLine(response.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(response.AccessToken);
            var apiResponse = await apiClient.GetAsync("http://localhost:6012/api/values");
            Assert.True(apiResponse.IsSuccessStatusCode);
            var content = await apiResponse.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        [Fact]
        public async Task Agent_API_Password_Test1()
        {
            // request token
            var response = await GetPasswordToken();

            Assert.False(response.IsError);
            Console.WriteLine(response.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(response.AccessToken);
            var apiResponse = await apiClient.GetAsync("http://localhost:6013/api/values");
            Assert.True(apiResponse.IsSuccessStatusCode);
            var content = await apiResponse.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        [Fact]
        public async Task MyAPI_ClientCredentials_Test1()
        {
            // request token
            var response = await GetClientCredentialsToken();

            Assert.False(response.IsError);
            Console.WriteLine(response.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(response.AccessToken);
            var apiResponse = await apiClient.GetAsync("http://localhost:6010/api/values/3");
            Assert.True(apiResponse.IsSuccessStatusCode);
            var content = await apiResponse.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        [Fact]
        public async Task MyAPI_ClientCredentials_Test2()
        {
            // request token
            var response = await GetClientCredentialsToken();

            Assert.False(response.IsError);
            Console.WriteLine(response.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(response.AccessToken);
            var apiResponse = await apiClient.GetAsync("http://localhost:6010/api/values");
            Assert.True(apiResponse.IsSuccessStatusCode);
            var content = await apiResponse.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
