using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    public class JobDTO
    {
        public long IdJob { get; set; }
        [Required]
        [Display(Name = "Job")]
        [StringLength(50)]
        public string JobName { get; set; }
        [Display(Name = "Job Title")]
        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }
        [Display(Name = "Description")]
        [Required]
        [StringLength(100)]
        public string JobDescription { get; set; }
        
        
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Expire At")]
        [Required]
        public DateTime ExpiresAt { get; set; }
    }
}
