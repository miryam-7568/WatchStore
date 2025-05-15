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
        ShopDB327742698Context _ShopDB327742698Context;

        public UsersData(ShopDB327742698Context shopDB327742698Context)
        {
            _ShopDB327742698Context = shopDB327742698Context;
        }
        public async Task<User> GetUserByIdFromDB(int id)
        {
            return await _ShopDB327742698Context.Users.FirstOrDefaultAsync(user => user.UserId == id);

        }


        public async Task Register(User user)
        {
            try
            {
                if (await _ShopDB327742698Context.Users.AnyAsync(u => u.UserName == user.UserName))
                    throw new CustomApiException(409, "Username is already taken");
                await _ShopDB327742698Context.Users.AddAsync(user);
                await _ShopDB327742698Context.SaveChangesAsync();
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

        public async Task<User> Login(LoginUser loginUser)
        {
            return await _ShopDB327742698Context.Users.FirstOrDefaultAsync(user => user.UserName == loginUser.UserName && user.Password == loginUser.Password);

        }

        public async Task<User> UpdateUser(int id, User userToUpdate)
        {
            try
            {
                _ShopDB327742698Context.Update(userToUpdate);
                await _ShopDB327742698Context.SaveChangesAsync();

                return userToUpdate;
            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, "Error updating user: " + ex.Message);
            }

        }
    }
}
