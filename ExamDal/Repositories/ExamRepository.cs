using ExamDal.Entities;
using Microsoft.EntityFrameworkCore;
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
        public int InsertOrUpdate(ExamDefinition exam)
        {
            if (exam.Id <= 0)
            {
                _ctx.ExamDefinitions.Add(exam);
            }
            else {
                var updateEdilecek = _ctx.ExamDefinitions.Find(exam.Id);
                updateEdilecek.Name = exam.Name;
            }
            _ctx.SaveChanges();

            return exam.Id;
        }

        public List<ExamDefinition> GetExams()
        {
            List<ExamDefinition> liste = _ctx.ExamDefinitions.Include(c=>c.Questions).ToList();
            return liste;
        }

        public ExamDefinition GetExamById(int id)
        {
            ExamDefinition result = _ctx.ExamDefinitions.Include(c=>c.Questions).First(c => c.Id == id);
            return result;
        }

        public void Delete(int id)
        {
            var silinecek = GetExamById(id);
            _ctx.ExamDefinitions.Remove(silinecek);
            _ctx.SaveChanges();
        }
    }
}
