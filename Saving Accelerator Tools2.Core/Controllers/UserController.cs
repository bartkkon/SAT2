using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers
{
    public class UserController
    {
        public static Users_DB LoadUser()
        {
            var LoginUser = new Users_DB();
            var context = new DataBaseConnetionContext();
            LoginUser = context.User
                .Where(u => u.Login == Environment.UserName.ToLower())
                .Include(u => u.User_Devisions)
                    .ThenInclude(b => b.Devisions)
                .Include(u => u.User_Pages)
                    .ThenInclude(b => b.Page)
                .Include(u => u.User_Plant)
                    .ThenInclude(b => b.Plant)
                .Include(u => u.User_Role)
                    .ThenInclude(b => b.Role)
                .FirstOrDefault();

            return LoginUser;
        }

        public static ICollection<Users_DB> LoadAllUsers()
        {
            ICollection<Users_DB> LoadUsers;
            var context = new DataBaseConnetionContext();
            LoadUsers = context.User
                //.Include(u => u.Devision)
                //.Include(u => u.Pages)
                //.Include(u => u.Plant)
                //.Include(u => u.Role)
                .ToList();

            return LoadUsers;
        }

        public static Users_DB LoadSpecificUser(string SelectedLogin)
        {
            var LoginUser = new Users_DB();
            var context = new DataBaseConnetionContext();
            LoginUser = context.User
                .Where(u => u.Login == SelectedLogin)
                .Include(u => u.User_Devisions)
                    .ThenInclude(b =>b.Devisions)
                .Include(u => u.User_Pages)
                    .ThenInclude(b => b.Page)
                .Include(u => u.User_Plant)
                    .ThenInclude(b => b.Plant)
                .Include(u => u.User_Role)
                    .ThenInclude(b => b.Role)
                .FirstOrDefault();

            return LoginUser;
        }

        public static void AddUsers(Users_DB NewUser)
        {
            var context = new DataBaseConnetionContext();
            context.Add(NewUser);
            context.SaveChanges();
        }

        public static void UpdateUser(Users_DB UpdatedUser)
        {
            var context = new DataBaseConnetionContext();
            context.Update(UpdatedUser);
            context.SaveChanges();
        }

        public static void DeletedUser(Users_DB RemoveUser)
        {
            var context = new DataBaseConnetionContext();
            context.Remove(RemoveUser);
            context.SaveChanges();
        }
    }
}
