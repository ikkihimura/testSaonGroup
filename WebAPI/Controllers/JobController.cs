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

        /// <summary>
        /// Get All the Jobs by the path GET /api/job
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/job
        ///     
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<Job>> Getjobs()
        {

            //var listProduct = this._iproduct.GetAll();
            var listProduct = await this._iJob.GetAll();
            return listProduct;
        }
        /// <summary>
        /// Get one job by id with the path GET /api/job/{id}
        /// </summary>
        /// <param name="idJob"></param>   
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/job/1
        ///     
        ///
        /// </remarks>
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


        /// <summary>
        /// Update the Job by the path PUT /api/job/ and the body with a json with the job
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/job/
        ///     {
        ///            "idJob": 1,
        ///             "jobName": "Architect test",
        ///             "jobTitle": "Data Architect Coordinator",
        ///             "jobDescription": "Define The RoadMap of SaonGroup for DB and Analytics - Test",
        ///             "createdAt": "2020-06-03",
        ///             "expiresAt": "2020-08-02"
        ///      }
        ///
        /// </remarks>
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
        /// <summary>
        /// Create a Job by the path POST /api/job and the json with the job object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/job
        ///     {
        ///            "idJob": 10,
        ///             "jobName": "Architect test",
        ///             "jobTitle": "Data Architect Coordinator",
        ///             "jobDescription": "Define The RoadMap of SaonGroup for DB and Analytics - Test",
        ///             "createdAt": "2020-06-03",
        ///             "expiresAt": "2020-08-02"
        ///      }
        ///
        /// </remarks>
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


        /// <summary>
        /// Delete a Job by the path DELETE /api/job and the json with the job object
        /// </summary>
        /// <param name="idJob"></param>  
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/job/1
        ///     
        ///
        /// </remarks>
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
