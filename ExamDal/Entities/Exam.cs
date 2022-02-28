using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDal.Entities
{
    public class Exam
    {
        public int Id { get; set; }

        public int ExamDefId { get; set; }

        public string UserName { get; set; }

        public List<ExamAnswer> Answers { get; set; }

        [ForeignKey("ExamDefId")]
        public ExamDefinition examDef { get; set; }


    }
}
