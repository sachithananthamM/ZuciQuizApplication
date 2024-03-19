using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZuciQuizLibrary.DataAccessLayer.Interfaces;
using ZuciQuizLibrary.Models;

namespace QuizWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IScoreRepository _scoreRepository;
        public ScoreController(IUserAnswerRepository userAnswerRepository, IScoreRepository scoreRepository)
        {
            _userAnswerRepository = userAnswerRepository;
            _scoreRepository = scoreRepository;
        }
        [HttpPost]
        public async Task<ActionResult> InsertScore(Score score)
        {
            try
            {
                await _scoreRepository.InsertScore(score);
                return Created($"api/score/{score.Id}", score);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult> GetOneUserScore(int userId)
        {
            List<Score> scores = await _scoreRepository.GetOneUserScore(userId);
            return Ok(scores);
        }
    }
}
