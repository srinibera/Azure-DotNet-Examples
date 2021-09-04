using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp_Authorize_Groups.Models;

namespace WebApp_Authorize_Groups
{
    public class StudentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;
        
        public StudentService(IHttpClientFactory clientFactory,ITokenAcquisition tokenAcquisition,IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                var _httpClient = _clientFactory.CreateClient();

                var scope = _configuration["StudentAPI:Scopes"];
                var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { scope });

                _httpClient.BaseAddress = new Uri(_configuration["StudentAPI:baseAddress"]);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await _httpClient.GetAsync("Students");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var studentList=JsonConvert.DeserializeObject<List<Student>>(responseContent);                    
                    return studentList;
                }

                throw new ApplicationException($"Status code: {response.StatusCode}, Error: {response.ReasonPhrase}");
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }
        }

        public async Task<bool> PostStudentAsync(Student student)
        {
            try
            {
                var _httpClient = _clientFactory.CreateClient();

                var scope = _configuration["StudentAPI:Scopes"];
                var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { scope });

                _httpClient.BaseAddress = new Uri(_configuration["StudentAPI:baseAddress"]);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                
                var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");                
                var response = await _httpClient.PostAsync("Students", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;;
                }

                throw new ApplicationException($"Status code: {response.StatusCode}, Error: {response.ReasonPhrase}");
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }
        }
    }
}
