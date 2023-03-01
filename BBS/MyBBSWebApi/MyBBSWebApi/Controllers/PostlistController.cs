using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyBBSWebApi.BLL;
using MyBBSWebApi.BLL.Interfaces;
using MyBBSWebApi.Model;
using MyBBSWebApi.Models;
using MyBBSWebApi.Models.Models;

namespace MyBBSWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostlistController : ControllerBase
    {
        private readonly IPostsBLL _postsBLL;
        public PostlistController(IUserBll userBll, IPostsBLL postsBLL)
        {
            this._postsBLL = postsBLL;
            UserBll = userBll;
        }

        public IUserBll UserBll { get; }

        [HttpGet("{token}")]
        public List<Post> GetPosts(string token)
        {
            // Users user = UserBll.GetUserByToken(token);
            //获取Posts
            List<Post> list = _postsBLL.GetAllOfPage().ToList();
            return list;
        }
    }
}