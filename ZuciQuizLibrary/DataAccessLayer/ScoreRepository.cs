using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuciQuizLibrary.DataAccessLayer.Interfaces;
using ZuciQuizLibrary.Models;

namespace ZuciQuizLibrary.DataAccessLayer
{

    public class ScoreRepository : IScoreRepository
    {
        ContextDb context = new ContextDb();

        public async Task InsertScore(Score score)
        {
            await context.Scores.AddAsync(score);
            await context.SaveChangesAsync();
        }
        public async Task<List<Score>> GetOneUserScore(int userId)
        {
            List<Score> scores = await (from score in context.Scores where score.UserId == userId select score).ToListAsync();
            return scores;
        }
    }
}
