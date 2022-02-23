using ExamDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDal.Repositories
{
    public class ExamRepository
    {
        ExamDbContext _ctx;
        public ExamRepository(ExamDbContext ctx)
        {
            _ctx = ctx;

        }
        public void Insert(ExamDefinition exam)
        {
            _ctx.ExamDefinitions.Add(exam);
            _ctx.SaveChanges();

        }

        public List<ExamDefinition> GetExams()
        {
            List<ExamDefinition> liste = _ctx.ExamDefinitions.ToList();
            return liste;
        }
    }
}
