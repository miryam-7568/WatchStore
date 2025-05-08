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
        IUsersData usersData;

        public UsersServices(IUsersData usersData)
        {
            this.usersData = usersData;
        }
        public bool ValidatePasswordStrength(string password)
        {
            var zxcvbnResult = Zxcvbn.Core.EvaluatePassword(password);
            return zxcvbnResult.Score >= 3;  // סיסמה נחשבת לחזקה אם היא מקבלת דירוג של 3 או יותר
        }

        public User GetUserById(int id)
        {
            User user = usersData.GetUserByIdFromDB(id);
            return user;
        }
        public void Register(User user)
        {
            if (ValidatePasswordStrength(user.Password))
            {
                usersData.Register(user);
            }
            else
            {
                throw new CustomApiException(400, "password not strong enough");
            }
        }
        public User Login(LoginUser loginUser)
        {
            return usersData.Login(loginUser);
        }

        public User UpdateUser(int id, User userToUpdate)
        {
            if (ValidatePasswordStrength(userToUpdate.Password))
            {
                return usersData.UpdateUser(id, userToUpdate);
            }
            else
            {
                throw new CustomApiException(400, "password not strong enough");
            }
        }

    }
}
