﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuciQuizLibrary.Models;

namespace ZuciQuizLibrary.Services.Interfaces
{
    public interface ITopicService
    {
        Task<List<Topic>> GetAllTopic();
        Task<Topic> GetByTopicId(int topicId);
        Task<Topic> GetTopicNameById(int topicId);
        Task InsertTopic(Topic topic);
        Task UpdateTopic(int topicId, Topic topic);
        Task DeleteTopic(int topicId);
    }
}
