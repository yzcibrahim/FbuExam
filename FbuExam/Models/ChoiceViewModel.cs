using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FbuExam.Models
{
    public class ChoiceViewModel
    {
        public int Id { get; set; }
        public int ChoiceNum { get; set; }
        public string ChText { get; set; }
        public string IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public QuestionViewModel Question { get; set; }
    }
}
