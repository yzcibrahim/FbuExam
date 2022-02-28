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
    public class ExamController : Controller
    {
        ExamRepository _examRepository;
        QuestionRepository _questionRepository;
        ChoiceRepository _choiceRepository;
        public ExamController(ExamRepository examRepository, QuestionRepository questionRepository, ChoiceRepository choiceRepository)
        {
            _examRepository = examRepository;
            _questionRepository = questionRepository;
            _choiceRepository = choiceRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListExams()
        {
            List<ExamDefinitionViewModel> model = new List<ExamDefinitionViewModel>();
            //model.Add(new ExamDefinitionViewModel() { Name = "test" });
            //model.Add(new ExamDefinitionViewModel() { Name = "deneme" });
            List<ExamDefinition> liste = _examRepository.GetExams();

            foreach (var exam in liste)
            {
            //    model.Add(new ExamDefinitionViewModel() { Id = exam.Id, Name = exam.Name });
                ExamDefinitionViewModel vm = new ExamDefinitionViewModel();
                vm.Id = exam.Id;
                vm.Name = exam.Name;
               
                vm.Questions = new List<QuestionViewModel>();

                foreach(var q in exam.Questions)
                {
                    QuestionViewModel qw = new QuestionViewModel();
                    qw.Id = q.Id;
                    qw.ExamId = q.ExamId;
                    qw.QuestionText = q.QuestionText;
                    qw.QuestionOrder = q.QuestionOrder;
                    qw.ExamName = exam.Name;
                    vm.Questions.Add(qw);
                }

                model.Add(vm);
            }


            return View(model);
        }

        public IActionResult AddExam(int id)
        {
            ExamDefinitionViewModel model = new ExamDefinitionViewModel();
           if(id>0)
            {
                ExamDefinition dataModel = _examRepository.GetExamById(id);
                model.Id = dataModel.Id;
                model.Name = dataModel.Name;

                model.Questions = new List<QuestionViewModel>();
                foreach(var qModel in dataModel.Questions)
                {
                    QuestionViewModel qvm = new QuestionViewModel();
                    qvm.Id = qModel.Id;
                    qvm.QuestionText = qModel.QuestionText;
                    qvm.ExamId = qModel.ExamId;
                    qvm.ExamName = dataModel.Name;
                    qvm.QuestionOrder = qModel.QuestionOrder;
                    model.Questions.Add(qvm);

                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddExam(ExamDefinitionViewModel model)
        {
            ExamDefinition dataModel = new ExamDefinition();
            dataModel.Name = model.Name;
            dataModel.Id = model.Id;
          int savedId =  _examRepository.InsertOrUpdate(dataModel);
            // return RedirectToAction("ListExams");
            return RedirectToAction("AddExam", new { id = savedId });
        }

        [HttpPost]
        public IActionResult AddQuestion(ExamDefinitionViewModel model)
        {
            //Question q = new Question();
            //q.Id = model.Id;
            //q.QuestionText = model.QuestionText;
            //q.ExamId = model.ExamId;

            var questionToAdd = model.QuestionToAdd;
            Question q = new Question()
            {
                Id = questionToAdd.Id,
                ExamId = questionToAdd.ExamId,
                QuestionText = questionToAdd.QuestionText,
                QuestionOrder = questionToAdd.QuestionOrder
            };

           int savedQuestionId = _questionRepository.AddOrUpdateQuestion(q);


            Choice choice1 = new Choice();
            choice1.QuestionId = savedQuestionId;
            choice1.ChoiceNum = 0;
            choice1.ChText = questionToAdd.AnswerA;
            choice1.IsCorrect = questionToAdd.CorrectAnswer == "A";

            _choiceRepository.AddOrUpdateChoice(choice1);

            choice1 = new Choice();
            choice1.QuestionId = savedQuestionId;
            choice1.ChoiceNum = 1;
            choice1.ChText = questionToAdd.AnswerB;
            choice1.IsCorrect = questionToAdd.CorrectAnswer == "B";

            _choiceRepository.AddOrUpdateChoice(choice1);

            choice1 = new Choice();
            choice1.QuestionId = savedQuestionId;
            choice1.ChoiceNum = 2;
            choice1.ChText = questionToAdd.AnswerC;
            choice1.IsCorrect = questionToAdd.CorrectAnswer == "C";

            _choiceRepository.AddOrUpdateChoice(choice1);

            choice1 = new Choice();
            choice1.QuestionId = savedQuestionId;
            choice1.ChoiceNum = 3;
            choice1.ChText = questionToAdd.AnswerD;
            choice1.IsCorrect = questionToAdd.CorrectAnswer == "D";

            _choiceRepository.AddOrUpdateChoice(choice1);

            // List<Choice> Choices = new List<Choice>();


            return RedirectToAction("AddExam", new { id = questionToAdd.ExamId });
        }

        public IActionResult Delete(int id)
        {
            _examRepository.Delete(id);
            return RedirectToAction("ListExams");
        }


        public IActionResult TakeExam(int id)
        {
            ExamDefinition ExamToSolve = _examRepository.GetExamById(id);
            Exam model = new Exam();
            model.ExamDefId = id;
            model.examDef = ExamToSolve;
            return View(model);
        }
        public IActionResult StartExam(Exam model)
        {
            Exam savedExam = _examRepository.AddExamInstance(model);
            ExamDefinition def = _examRepository.GetExamById(savedExam.ExamDefId);
            var questions = def.Questions.OrderBy(c => c.QuestionOrder);

            ViewBag.ExamInstanceId = savedExam.Id;

            return View("AnswerForQuestion", questions.First());
        }

        [HttpPost]
        public IActionResult AnswerForQuestion(int ExamInstanceId, int QuestionId, string answerText)
        {
            int answerChoiceId = 0;
            if (answerText == "A")
                answerChoiceId = 0;
            else if(answerText == "B")
                answerChoiceId = 1;
            else if (answerText == "C")
                answerChoiceId = 2;
            else if (answerText == "D")
                answerChoiceId = 3;
            ViewBag.ExamInstanceId = ExamInstanceId;
            var examInstance = _examRepository.GetExamInstance(ExamInstanceId);

            ExamAnswer answer = new ExamAnswer();
            answer.ExamId = ExamInstanceId;
            answer.QuestionId = QuestionId;
            answer.SelectedChoiceId = answerChoiceId;

            List<Question> questionList = examInstance.examDef.Questions.OrderBy(c=>c.QuestionOrder).ToList();

            var answeredQuestion = questionList.First(c => c.Id == QuestionId);
            questionList = questionList.Where(c => c.QuestionOrder > answeredQuestion.QuestionOrder).ToList();
           
            
            _examRepository.AddExamAnswer(answer);
            return View("AnswerForQuestion", questionList.First());
           

        }
    }
}
