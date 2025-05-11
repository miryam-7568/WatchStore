using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class UsersData
        : IUsersData
    {
        string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Repository", "users.txt");
        ShopDB327742698Context _shopDB;

        public UsersData(ShopDB327742698Context shopDB)
        {
            this._shopDB = shopDB;
        }

        public async Task<User> GetUserByIdFromDB(int id)
        {
            return await _shopDB.Users.FirstOrDefaultAsync(u => u.UserId == id);

        }
        public async void Register(User user)
        {
            try
            {
                if (await _shopDB.Users.AnyAsync(u => u.UserName == user.UserName))
                    throw new CustomApiException(409, "Username is already taken");
                await _shopDB.Users.AddAsync(user);
                await _shopDB.SaveChangesAsync();
            }
            catch (CustomApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, "Error writing user to file: " + ex.Message);
            }
        }

        public async Task<User>  Login(LoginUser loginUser)
        {
              return  await _shopDB.Users.FirstOrDefaultAsync(u => u.Password == loginUser.Password && u.UserName == loginUser.UserName);
        }

        public async Task<User> UpdateUser(int id, User userToUpdate)
        {
            try
            {
                var user = _shopDB.Users.FirstOrDefault(u => u.UserId == id);
                if (user == null)
                {
                    throw new CustomApiException(404, "User not found");
                }
                user.UserName = userToUpdate.UserName;
                user.Password = userToUpdate.Password;
                user.FirstName = userToUpdate.FirstName;
                user.LastName = userToUpdate.LastName;
                await _shopDB.SaveChangesAsync();
                return user;
            }
            catch (CustomApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, ex.Message);
            }
        }
    }
}
