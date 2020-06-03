using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class JobController : Controller
    {
        JobAPI _jobAPI = new JobAPI();

        public async Task<IActionResult> Index()
        {
            List<JobDTO> dto = new List<JobDTO>();

            HttpClient client = _jobAPI.InitializeClient();

            HttpResponseMessage res = await client.GetAsync("api/job");

            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the job list    
                dto = JsonConvert.DeserializeObject<List<JobDTO>>(result);

            }
            //returning the employee list to view    
            return View(dto);
        }

        // GET: Product/Create  
        public IActionResult Create()
        {
            return View();
        }

        // POST: job/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("JobName,JobTitle,JobDescription,ExpiresAt")] JobDTO product)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _jobAPI.InitializeClient();
              
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/job", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // POST: job/Edit/1  
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<JobDTO> dto = new List<JobDTO>();
            HttpClient client = _jobAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/job");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<JobDTO>>(result);
            }

            var product = dto.SingleOrDefault(m => m.IdJob == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Job/Edit/1  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("IdJob,JobName,JobTitle,JobDescription,CreatedAt,ExpiresAt")] JobDTO product)
        {
            if (id != product.IdJob)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpClient client = _jobAPI.InitializeClient();

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                HttpResponseMessage res = client.PutAsync("api/job", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // POST: Job/Delete/1  
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<JobDTO> dto = new List<JobDTO>();
            HttpClient client = _jobAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/job");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<JobDTO>>(result);
            }

            var product = dto.SingleOrDefault(m => m.IdJob == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: job/Delete/5  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([Bind("IdJob")] long id)
        {
            HttpClient client = _jobAPI.InitializeClient();
            HttpResponseMessage res = client.DeleteAsync($"api/job/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
