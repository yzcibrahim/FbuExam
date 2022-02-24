using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FbuExam.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
       
        [Required]
        [MinLength(5)]
        public string QuestionText { get; set; }
        public int ExamId { get; set; }

        public string ExamName { get; set; }
    }
}
