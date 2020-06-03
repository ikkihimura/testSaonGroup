using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    public class JobDTO
    {
        public long IdJob { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
