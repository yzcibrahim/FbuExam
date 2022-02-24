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
            return RedirectToAction("ListExam");
        }

        public IActionResult Delete(int id)
        {
            _examRepository.Delete(id);
            return RedirectToAction("ListExam");
        }
    }
}
