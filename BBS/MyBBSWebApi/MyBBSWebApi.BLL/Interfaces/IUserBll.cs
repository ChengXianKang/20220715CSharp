using MyBBSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBBSWebApi.BLL
{
    public interface IUserBll
    {
        string AddUser(Users user);
        Users CheakLogin(string userNo, string password);
        Users GetUserByToken(string token);
        List<Users> GetAll();
        string RemoveUser(int id);
        string UpdateUserOfUI(Users user);
        string UpdateUser(int id, string userNo, string userName, string password, int? userLevel, Guid? token, Guid? autoLoginTag, DateTime? autoLoginLimitTime);
    }
}
