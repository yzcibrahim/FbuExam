using ExamDal.Entities;
using ExamDal.Repositories;
using FbuExam.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FbuExam.Controllers
{
    public class QuestionController : Controller
    {
        QuestionRepository _questionRepository;
        ExamRepository _examRepository;
        public QuestionController(QuestionRepository questionRepository, ExamRepository examRepository)
        {
            _questionRepository = questionRepository;
            _examRepository = examRepository;
        }

        public IActionResult ListQuestions()
        {
            List<Question> liste = _questionRepository.ListQuestions();

            var model = new List<QuestionViewModel>();

            foreach(var item in liste)
            {
                QuestionViewModel question = new QuestionViewModel();
                question.Id = item.Id;
                question.QuestionText = item.QuestionText;
                question.ExamId = item.ExamId;

                //1,yol examname almak
                //  ExamDefinition exam = _examRepository.GetExamById(question.ExamId);
                //  question.ExamName = exam.Name;

                //2yol
                question.ExamName = item.ExamDef.Name;
                model.Add(question);
            }

            return View(model);

        }

        public IActionResult AddQuestion(int id)
        {
            QuestionViewModel model = new QuestionViewModel();
            if(id>0)
            {
                Question question = _questionRepository.GetQuestionById(id);
                model.Id = question.Id;
                model.ExamId = question.ExamId;
                model.QuestionText = question.QuestionText;
                model.QuestionOrder = question.QuestionOrder;
            }

            List<ExamDefinition> examList=_examRepository.GetExams();

            foreach(var exam in examList)
            {
                ExamDefinitionViewModel exVM = new ExamDefinitionViewModel();
                exVM.Id = exam.Id;
                exVM.Name = exam.Name;
                model.AllExams.Add(exVM);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddQuestion(QuestionViewModel model)
        {
            //Question q = new Question();
            //q.Id = model.Id;
            //q.QuestionText = model.QuestionText;
            //q.ExamId = model.ExamId;
            Question q = new Question()
            { 
                Id = model.Id,
                ExamId = model.ExamId,
                QuestionText = model.QuestionText,
                QuestionOrder=model.QuestionOrder
            };
            _questionRepository.AddOrUpdateQuestion(q);

            return RedirectToAction("ListQuestions");
        }

        public IActionResult Delete(int id)
        {
            _questionRepository.Delete(id);
            return RedirectToAction("ListQuestions");
        }

    }
}
