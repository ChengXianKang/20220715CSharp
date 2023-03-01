using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBBSWebApi.DAL.Core;
using MyBBSWebApi.DAL.Factorys;
using MyBBSWebApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //Get/Post/Put/Delete
        //Get => 数据的获取
        //Post => 数据的插入
        //Put => 数据更新
        //Delete => 数据的删除
        [HttpGet]
        public IEnumerable<Post> Get()
        {
           var conetxt = DbContextFactory.GetDbContext();
           return conetxt.Posts.ToList();
        }
    }
}
