using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAO;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {

        //Agregar las interfaces para acceder al DAO
        private readonly IJobRepository<Job, long> _iJob;
        
        public JobController(IJobRepository<Job,long> jobRepository)
        {
            this._iJob = jobRepository;
        }


        //Get All the Jobs by the path GET /api/job
        [HttpGet]
        public async Task<IEnumerable<Job>> Getjobs()
        {

            //var listProduct = this._iproduct.GetAll();
            var listProduct = await this._iJob.GetAll();
            return listProduct;
        }
        
        //Get one job by id with the path GET /api/job/{id}
        [HttpGet]
        [Route("{idJob}")]
        public async Task<IActionResult> GetJob(int idJob)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobItem = await this._iJob.Get(idJob);

            if (jobItem == null)
            {
                return NotFound();
            }

            return Ok(jobItem);

        }

        //Update the Job by the path PUT /api/job/ and the body with a json with the job
        [HttpPut]
        public async Task<IActionResult> PutJobItem([FromBody] Job job)
        {
            /*if (idProduct != product.IdProduct)
            {
                return BadRequest();
            }
            */
            var jobItem = await this._iJob.Get(job.IdJob);

            if (jobItem == null)
            {
                return NotFound();
            }


            try
            {

                await this._iJob.Update(job.IdJob, job);
            }
            catch (DbUpdateConcurrencyException)
            {
                return UnprocessableEntity();
            }

            return NoContent();
        }

        //Create a Job by the path POST /api/job and the json with the job object
        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            job.CreatedAt = DateTime.Now;
            var idAdded = await this._iJob.Add(job);

            return CreatedAtAction(nameof(GetJob), new { idJob = job.IdJob }, job);
        }

        //Delete a Job by the path DELETE /api/job and the json with the job object
        [HttpDelete]
        [Route("{idJob}")]
        public async Task<IActionResult> DeleteJob(int idJob)
        {
            var jobItem = await _iJob.Get(idJob);

            if (jobItem == null)
            {
                return NotFound();
            }


            await _iJob.Delete(idJob);
            return NoContent();

        }
    }
}
