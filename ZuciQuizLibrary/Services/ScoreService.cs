using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuciQuizLibrary.DataAccessLayer;
using ZuciQuizLibrary.DataAccessLayer.Interfaces;
using ZuciQuizLibrary.Models;
using ZuciQuizLibrary.Services.Interfaces;

namespace ZuciQuizLibrary.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        public ScoreService(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        public async Task InsertScore(Score score)
        {
            await _scoreRepository.InsertScore(score);
        }
        public async Task<List<Score>> GetOneUserScore(int userId)
        {
            try
            {
                var scores = await _scoreRepository.GetOneUserScore(userId);
                return scores;
            }
            catch
            {
                throw new Exception("User Not Found");
            }
        }

        public Task<Score> GetScore(int totalQuestions, int correctCount)
        {
            try
            {
                double score = (double)correctCount / totalQuestions * 100;   // score calculation
                Score result = new Score { Mark = score }; 
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception("calculation Error", ex);
            }
        }

    }
}
