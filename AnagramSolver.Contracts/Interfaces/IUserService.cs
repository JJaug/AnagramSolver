﻿namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserService
    {
        public void CreateUser(string firstName, string lastName, string email, string pass, string favouriteWords);
        public string ReadUser(long id);
        public void DeleteUser(long id);


    }
}
