using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FbuExam.Models
{
    public class ExamDefinitionViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();

        public QuestionViewModel QuestionToAdd { get; set; } = new QuestionViewModel();

    }
}
