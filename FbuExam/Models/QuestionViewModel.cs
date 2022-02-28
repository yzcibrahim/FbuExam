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

        public int QuestionOrder { get; set; }
        public List<ExamDefinitionViewModel> AllExams { get; set; } = new List<ExamDefinitionViewModel>();

        public string AnswerA { get; set; }

        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }

        public string CorrectAnswer { get; set; }//ABCD


    }

}
