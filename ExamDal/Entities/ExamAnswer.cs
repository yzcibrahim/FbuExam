using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDal.Entities
{
    public class ExamAnswer
    {
        public int Id { get; set; }
        public int? ExamId { get; set; }
        public int? QuestionId { get; set; }
        public int? SelectedChoiceId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
        [ForeignKey("SelectedChoiceId")]
        public virtual Choice SelectedChoice { get; set; }
    }
}
