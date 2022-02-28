using ExamDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDal.Repositories
{
    public class QuestionRepository
    {
        ExamDbContext _ctx;
        public QuestionRepository(ExamDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Question> ListQuestions()
        {
            return _ctx.Questions.Include(c=>c.ExamDef).ToList();
        }

        public int AddOrUpdateQuestion(Question question)
        {
            if (question.Id <= 0)
            {
                _ctx.Questions.Add(question);
            }
            else
            {
                var updated = GetQuestionById(question.Id);
                updated.QuestionText = question.QuestionText;
                updated.ExamId = question.ExamId;
                
            }
            _ctx.SaveChanges();

            return question.Id;
        }

        public Question GetQuestionById(int id)
        {
            return _ctx.Questions.First(c => c.Id == id);
        }

        public void Delete(int id)
        {
            var silinecek = GetQuestionById(id);
            _ctx.Questions.Remove(silinecek);
            _ctx.SaveChanges();
        }
    }
}
