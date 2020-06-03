using System;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using WebAPI.Controllers;
using System.Net.Http;
using WebAPI;
using System.Threading.Tasks;
using System.Net;
using WebAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace XUnitTest
{
    public class UnitTest1
    {
        private static HttpClient _client = new HttpClient();
        public UnitTest1()
        {
          
        }
        [Theory]
        [InlineData()]
        public async Task JobGetAllTest()
        {
            //arrange
            
            
            // Act
            var response = await _client.GetAsync("https://localhost:5001/api/job");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task JobGetTest(long idJob)
        {
            //arrange
           
            // Act
          
            var response = await _client.GetAsync($"https://localhost:5001/api/job/{idJob}");
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public async Task JobDeleteTest(long idJob)
        {
            //arrange
           
            // Act
           
            var response = await _client.DeleteAsync($"https://localhost:5001/api/job/{idJob}");
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData()]
        public async Task JobCreateTest()
        {
            //arrange
                      
            Job job = new Job
            {
                JobName = "tester",
                JobTitle = "Test Title",
                JobDescription = "This is a test",
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(job);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Act
            var response = await _client.PostAsync("https://localhost:5001/api/job", data);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
