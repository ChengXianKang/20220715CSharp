using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyBBSWebApi.BLL;
using MyBBSWebApi.Common;
using MyBBSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Controllers
{
    //[Route("[controller]/[action]")]
    [Route("[controller]")] //restful
    [ApiController]
    [EnableCors("any")]
    public class LoginController : ControllerBase
    {
        private readonly IUserBll _userBll;

        public LoginController(IUserBll userBll)
        {
            _userBll = userBll;
        }
        [HttpGet]
        public List<Users> GetAll()
        {
           
            List<Users> userList = _userBll.GetAll();
            return userList;
        }
        [HttpGet("{userNo}/{password}")]
        public Users GetLoginRes(string userNo, string password)
        {
            Users user = _userBll.CheakLogin(userNo, password);
            return user;
        }
        [HttpPost]
        public string Insert(Users user)
        {
            return _userBll.AddUser(user);
        }
        [HttpPut]
        public string Update(int id, string userNo, string userName, string password, int? userLevel,Guid token)
        {

            return _userBll.UpdateUser(id, userNo, userName, password, userLevel,token, null, null);
        }
        [HttpDelete]
        public string Remove(int id)
        {

            return _userBll.RemoveUser(id);
        }
        [HttpPost("test")]
        public void Test(TestApiControllerViewModel test)
        {
            //return _userBll.AddUser(user);
        }
    }
}
