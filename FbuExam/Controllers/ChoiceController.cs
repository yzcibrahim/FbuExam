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
    public class ChoiceController : Controller
    {
        ChoiceRepository _choiceRepository;

        public ChoiceController(ChoiceRepository choiceRepository)
        {
            _choiceRepository = choiceRepository;
        }
        public IActionResult List()
        {
            List<Choice> chList = _choiceRepository.ListChoices();

            var model = new List<ChoiceViewModel>();

            foreach(var item in chList)
            {
                ChoiceViewModel viewModel = new ChoiceViewModel();
                viewModel.Id = item.Id;
                viewModel.ChoiceNum = item.ChoiceNum;
                viewModel.ChText = item.ChText;
                viewModel.IsCorrect = item.IsCorrect == true ? "Doğru" : "Yanlış";
                viewModel.QuestionId = item.QuestionId;
                
                model.Add(viewModel);
            }

            return View(model);
        }

        public IActionResult AddChoice(int id)
        {
            Choice dataModel = new Choice();

            if (id > 0)
            {
                dataModel = _choiceRepository.GetChoiceById(id);
            }

            return View(dataModel);
        }

        [HttpPost]
        public IActionResult AddChoice(Choice model)
        {
            _choiceRepository.AddOrUpdateChoice(model);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            _choiceRepository.Delete(id);
            return RedirectToAction("List");
        }
    }
}
