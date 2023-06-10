using AppEverTec.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppEverTec.Services
{
    public class ApiService : IApiService
    {
        public Task<Response<T>> Delete<T>(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<T>> Get<T>(string url)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                var answer2 = new StringContent(answer, Encoding.UTF8, "application/json");
                if (!response.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        IsSucces = false,
                        Message = answer2.ReadAsStringAsync().Result.ToString()
                    };
                }
                var list = JsonConvert.DeserializeObject<T>(answer);
                return new Response<T>
                {
                    IsSucces = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    IsSucces = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<T>> GetList<T>(string url)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                var answer2 = new StringContent(answer, Encoding.UTF8, "application/json");
                if (!response.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        IsSucces = false,
                        Message = answer2.ReadAsStringAsync().Result.ToString()
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response<T>
                {
                    IsSucces = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    IsSucces = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<T>> Post<T>(string url, object model)
        {
            try
            {
                var body = JsonConvert.SerializeObject(model);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                //client.BaseAddress = new Uri(url);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response<T>>(result);
                    return new Response<T>
                    {
                        IsSucces = false,
                        Message = error.Message,
                    };
                }
                var objresponse = JsonConvert.DeserializeObject<T>(result);
                return new Response<T>
                {
                    IsSucces = true,
                    Result = objresponse,
                };
            }

            catch (Exception ex)
            {
                return new Response<T>
                {
                    IsSucces = false,
                    Message = ex.Message,
                };
            }
        }

        public Task<Response<T>> Put<T>(string url, object model)
        {
            throw new NotImplementedException();
        }
    }
}
