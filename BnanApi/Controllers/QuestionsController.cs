﻿using BnanApi.DTOS;
using BnanApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BnanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private readonly BnanKSAContext _context;

        public QuestionsController(BnanKSAContext context)
        {
            _context = context;
        }



        //[HttpGet]
        //public async Task<IActionResult> Questions()
        //{
        //    List<QuestionDTO> questionsDTO = new List<QuestionDTO>();
        //    var Questions = await _context.CrMasSysQuestionsAnswers.Where(x => x.CrMasSysQuestionsAnswerStatus == "A").ToListAsync();
        //    foreach (var question in Questions)
        //    {
        //        var MainTask = _context.CrMasSysMainTasks.FirstOrDefault(x => x.CrMasSysMainTasksCode == question.CrMasSysQuestionsAnswerMainTask && x.CrMasSysMainTasksSystem == question.CrMasSysQuestionsAnswerSystem && x.CrMasSysMainTasksStatus == "A");
        //        QuestionDTO questionDTO = new QuestionDTO();
        //        questionDTO.No = question.CrMasSysQuestionsAnswerNo;
        //        questionDTO.MainTaskCode = question.CrMasSysQuestionsAnswerMainTask;
        //        questionDTO.System = question.CrMasSysQuestionsAnswerSystem;
        //        questionDTO.ArMainTask = MainTask.CrMasSysMainTasksArName;
        //        questionDTO.EnMainTask = MainTask.CrMasSysMainTasksEnName;
        //        questionDTO.ArFullMainTask = MainTask.CrMasSysMainTasksConcatenateArName;
        //        questionDTO.EnFullMainTask = MainTask.CrMasSysMainTasksConcatenateEnName;
        //        questionDTO.ArQuestions = question.CrMasSysQuestionsAnswerArQuestions;
        //        questionDTO.ArAnswer = question.CrMasSysQuestionsAnswerArAnswer;
        //        questionDTO.EnQuestions = question.CrMasSysQuestionsAnswerEnQuestions;
        //        questionDTO.ArQuestions = question.CrMasSysQuestionsAnswerEnAnswer;
        //        questionsDTO.Add(questionDTO);
        //    }
        //    var groupBy = questionsDTO.GroupBy(x => x.MainTaskCode).ToList();
        //    return Ok(groupBy);
        //}
        [HttpGet]
        public async Task<IActionResult> Questions()
        {
            // Fetching all questions with status "A" from the database
            var questions = await _context.CrMasSysQuestionsAnswers
                .Where(x => x.CrMasSysQuestionsAnswerStatus == "A")
                .ToListAsync();

            // Grouping questions by code in-memory
            var groupedQuestions = questions
                .GroupBy(x => x.CrMasSysQuestionsAnswerMainTask)
                .ToList(); // Perform grouping in-memory

            // Creating a list to hold the result
            List<object> resultList = new List<object>();

            // Iterating over each group
            foreach (var group in groupedQuestions)
            {
                // Creating a list to hold questions and answers for this code
                List<object> subitem = new List<object>();
                var MainTask = _context.CrMasSysMainTasks.FirstOrDefault(x => x.CrMasSysMainTasksCode == group.FirstOrDefault().CrMasSysQuestionsAnswerMainTask && x.CrMasSysMainTasksSystem == group.FirstOrDefault().CrMasSysQuestionsAnswerSystem && x.CrMasSysMainTasksStatus == "A");
                foreach (var question in group)
                {
                    // Creating an anonymous object with question and answer
                    var questionAndAnswer = new
                    {
                        ArQuestion = question.CrMasSysQuestionsAnswerArQuestions, // Assuming Arabic question is preferred
                        ArAnswer = question.CrMasSysQuestionsAnswerArAnswer, // Assuming Arabic answer is preferred
                        EnQuestion = question.CrMasSysQuestionsAnswerEnQuestions, // Assuming Arabic question is preferred
                        EnAnswer = question.CrMasSysQuestionsAnswerEnAnswer, // Assuming Arabic answer is preferred
                    };

                    // Adding question and answer to the subitem list
                    subitem.Add(questionAndAnswer);
                }

                // Adding code and subitem to the result list
                resultList.Add(new { code = group.Key ,arName= MainTask.CrMasSysMainTasksArName,enName=MainTask.CrMasSysMainTasksEnName,
                    arFullName = MainTask.CrMasSysMainTasksConcatenateArName,
                    enFullName = MainTask.CrMasSysMainTasksConcatenateEnName
                    , questionsAndAnswers = subitem });
            }

            return Ok(resultList);
        }
    }
}
