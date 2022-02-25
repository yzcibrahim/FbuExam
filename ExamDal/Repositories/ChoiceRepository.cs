using ExamDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDal.Repositories
{
    public class ChoiceRepository
    {
        ExamDbContext _ctx;

        public ChoiceRepository(ExamDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Choice> ListChoices()
        {
            return _ctx.Choices.Include(c => c.Question).ToList();
        }

        public void AddOrUpdateChoice(Choice choice)
        {
            if (choice.Id <= 0)
            {
                _ctx.Choices.Add(choice);
            }
            else
            {
                _ctx.Attach(choice);
                _ctx.Entry(choice).State = EntityState.Modified;
               
            }
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var willBeDeleted = _ctx.Choices.Find(id);
            _ctx.Choices.Remove(willBeDeleted);
            _ctx.SaveChanges();

        }

        public Choice GetChoiceById(int id)
        {
            return _ctx.Choices.Find(id);
        }


    }
}
