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
        ContextDb DBcontext = new ContextDb();

        public async Task InsertScore(Score score)
        {
            await DBcontext.Scores.AddAsync(score);
            await DBcontext.SaveChangesAsync();
        }
        public async Task<List<Score>> GetOneUserScore(int userId)
        {
            List<Score> scores = await (from score in DBcontext.Scores where score.UserId == userId select score).ToListAsync();
            return scores;
        }

    }
}
