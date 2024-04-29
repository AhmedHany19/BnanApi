using BnanApi.DTOS;
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



        [HttpGet]
        public async Task<IActionResult> Questions()
        {
            List<QuestionDTO> questionsDTO = new List<QuestionDTO>();
            var Questions = await _context.CrMasSysQuestionsAnswers.Where(x => x.CrMasSysQuestionsAnswerStatus == "A").ToListAsync();
            foreach (var question in Questions)
            {
                var MainTask = _context.CrMasSysMainTasks.FirstOrDefault(x => x.CrMasSysMainTasksCode == question.CrMasSysQuestionsAnswerMainTask && x.CrMasSysMainTasksSystem == question.CrMasSysQuestionsAnswerSystem && x.CrMasSysMainTasksStatus == "A");
                QuestionDTO questionDTO = new QuestionDTO();
                questionDTO.No = question.CrMasSysQuestionsAnswerNo;
                questionDTO.System = question.CrMasSysQuestionsAnswerSystem;
                questionDTO.ArMainTask = MainTask.CrMasSysMainTasksArName;
                questionDTO.EnMainTask = MainTask.CrMasSysMainTasksEnName;
                questionDTO.ArFullMainTask = MainTask.CrMasSysMainTasksConcatenateArName;
                questionDTO.EnFullMainTask = MainTask.CrMasSysMainTasksConcatenateEnName;
                questionDTO.ArQuestions = question.CrMasSysQuestionsAnswerArQuestions;
                questionDTO.ArAnswer = question.CrMasSysQuestionsAnswerArAnswer;
                questionDTO.EnQuestions = question.CrMasSysQuestionsAnswerEnQuestions;
                questionDTO.ArQuestions = question.CrMasSysQuestionsAnswerEnAnswer;
                questionsDTO.Add(questionDTO);
            }
            return Ok(questionsDTO);
        }
    }
}
