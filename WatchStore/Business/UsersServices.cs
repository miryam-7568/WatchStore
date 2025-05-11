using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsersServices
        : IUsersServices
    {
        readonly IUsersData _usersData;

        public UsersServices(IUsersData usersData)
        {
            this._usersData = usersData;
        }
        public bool ValidatePasswordStrength(string password)
        {
            var zxcvbnResult = Zxcvbn.Core.EvaluatePassword(password);
            return zxcvbnResult.Score >= 3;  // סיסמה נחשבת לחזקה אם היא מקבלת דירוג של 3 או יותר
        }

        public async Task<User> GetUserById(int id)
        {
            return await _usersData.GetUserByIdFromDB(id);
        }
        public void Register(User user)
        {
            if (ValidatePasswordStrength(user.Password))
            {
                _usersData.Register(user);
            }
            else
            {
                throw new CustomApiException(400, "password not strong enough");
            }
        }
        public async Task<User> Login(LoginUser loginUser)
        {
            return await _usersData.Login(loginUser);
        }

        public async Task<User> UpdateUser(int id, User userToUpdate)
        {
            if (ValidatePasswordStrength(userToUpdate.Password))
            {
                return await _usersData.UpdateUser(id, userToUpdate);
            }
            else
            {
                throw new CustomApiException(400, "password not strong enough");
            }
        }

    }
}
