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
        public ExamController(ExamRepository examRepository)
        {
            _examRepository = examRepository;
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
            _examRepository.InsertOrUpdate(dataModel);
            return RedirectToAction("ListExams");
        }

        public IActionResult Delete(int id)
        {
            _examRepository.Delete(id);
            return RedirectToAction("ListExams");
        }
    }
}
