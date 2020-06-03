using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAO
{
    //Implementation to access to the data objects on the in memory database
    public class JobRepository : IJobRepository<Job,long>
    {
        //Setting the injection for access to the context of the in memory db  
        InMemDBContext _ctx;
        public JobRepository(InMemDBContext ctx)
        {
            this._ctx = ctx;

        }
        //Adding object to the dabase
        public async Task<long> Add(Job job)
        {
            this._ctx.Job.Add(job);
            long jobID = await this._ctx.SaveChangesAsync();
            return jobID;
        }
        //Delete object of the dabase
        public async Task<long> Delete(long id)
        {
            long jobID = 0;
            var job = await this._ctx.Job.FirstOrDefaultAsync(b => b.IdJob == id);
            if (job != null)
            {
                this._ctx.Job.Remove(job);
                jobID = await this._ctx.SaveChangesAsync();
            }
            return jobID;
        }
        //Get a single Job object by the id
        public async Task<Job> Get(long id)
        {
            return await this._ctx.Job.FirstOrDefaultAsync(b => b.IdJob == id);
        }

        //Get All job objects from the dabase
        public async Task<IEnumerable<Job>> GetAll()
        {

            return await this._ctx.Job.ToListAsync();
        }

        //Updating the object in the dabase
        public async Task<long> Update(long id, Job jobItem)
        {
            long jobID = 0;
            var job = this._ctx.Job.Find(id);
            if (job != null)
            {
                job.JobName = jobItem.JobName;
                job.JobTitle = jobItem.JobTitle;
                job.JobDescription = jobItem.JobDescription;
                job.CreatedAt = jobItem.CreatedAt;
                job.ExpiresAt = jobItem.ExpiresAt;
                

                jobID = await this._ctx.SaveChangesAsync();
            }
            return jobID;

        }
    }
}
