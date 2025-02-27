﻿using Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Api
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;

        public ApiHelper()
        {
            InitializeClient();
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<UserAccess> Authenticate(string username, string password)
        {
            var data = new
            {
                username,
                password
            };

            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");           

            using (HttpResponseMessage responseMessage = await _apiClient.PostAsync("api/Authentication/login", stringContent))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    try
                    {
                        
                        var result = await responseMessage.Content.ReadAsAsync<UserAccess>();

                        return result;
                    }
                    catch(Exception e)
                    {
                        var gds = e.Message;
                        return null;
                    }
                    
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }

            }
           
        }
        public  void Authorized(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();           
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

        }
    }
}
