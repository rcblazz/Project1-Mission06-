using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class TaskResponse
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string Task { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public byte Quadrant { get; set; }

        // Build Foreign Key Relationship

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool Completed { get; set;  }
    }
}
