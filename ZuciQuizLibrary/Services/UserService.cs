﻿using ZuciQuizLibrary.DataAccessLayer.Interfaces;
using ZuciQuizLibrary.Models;
using ZuciQuizLibrary.Services.Interfaces;

namespace ZuciQuizLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUser(userId);
        }

        public async Task<List<User>> GetAllUser()
        {
            var Users = await _userRepository.GetAllUser();
            return Users;
        }

        public async Task<User> GetUserbyUserId(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserbyUserId(userId);
                return user;
            }
            catch (Exception)
            {
                throw new Exception("User Not Found");
            }
        }

        public async Task<User> GetUserbyUserName(string userName)
        {

            var user = await _userRepository.GetUserbyUserName(userName);

            if (user == null)
            {
                throw new Exception("User Not found");
            }
            else
            {
                return user;
            }
        }

        public async Task InsertUser(User User)
        {
            await _userRepository.InsertUser(User);
        }

        public async Task UpdateUser(int userId, User User)
        {
            await _userRepository.UpdateUser(userId, User);
        }
    }
}
