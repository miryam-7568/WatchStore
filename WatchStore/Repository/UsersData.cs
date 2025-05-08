using Entities;
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
        public User GetUserByIdFromDB(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.UserId == id)
                        return user;
                }
            }
            return null;
        }
        public void Register(User user)
        {
            try
            {
                int numberOfUsers = System.IO.File.Exists(filePath) ? System.IO.File.ReadLines(filePath).Count() : 0;
                user.UserId = numberOfUsers + 1;
                if (System.IO.File.Exists(filePath))
                {
                    var existingUsers = System.IO.File.ReadLines(filePath).Select(line => JsonSerializer.Deserialize<User>(line)).ToList();
                    if (existingUsers.Any(u => u.UserName == user.UserName))
                        throw new CustomApiException(409,"Username is already taken");
                }
                string userJson = JsonSerializer.Serialize(user);
                System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);
            }
            catch (CustomApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomApiException(500,"Error writing user to file: " + ex.Message);
            }
        }

        public User Login(LoginUser loginUser)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    throw new CustomApiException(500,"DB not found.");
                }

                using (StreamReader reader = System.IO.File.OpenText(filePath))
                {
                    string currentUserInFile;
                    while ((currentUserInFile = reader.ReadLine()) != null)
                    {
                        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                        if (user?.UserName == loginUser.UserName && user.Password == loginUser.Password)
                        {
                            return user;
                        }
                    }
                }
                throw new CustomApiException(401, "Invalid username or password.");
            }
            catch (Exception ex)
            {
                if (ex is CustomApiException)
                {
                    throw ex;
                }
                throw new CustomApiException(500, "Error reading users from file: " + ex.Message);
            }
        }

        public User UpdateUser(int id, User userToUpdate)
        {
            string textToReplace = string.Empty;
            try
            {
                using (StreamReader reader = System.IO.File.OpenText(filePath))
                {
                    string currentUserInFile;
                    while ((currentUserInFile = reader.ReadLine()) != null)
                    {
                        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                        if (user?.UserId == id)
                            textToReplace = currentUserInFile;
                    }
                }

                if (textToReplace != string.Empty)
                {
                    string text = System.IO.File.ReadAllText(filePath);
                    text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                    System.IO.File.WriteAllText(filePath, text);
                    return userToUpdate;
                }
                else
                {
                    throw new CustomApiException(404, "no user found");
                }
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
