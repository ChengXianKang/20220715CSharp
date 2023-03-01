using System;
using System.Collections.Generic;
using System.Data;
using MyBBSWebApi.Model;
using MyBBSWebApi.Models.Models;

namespace MyBBSWebApi.BLL.Interfaces
{
     public interface IPostsBLL
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllOfPage(int pageIndex = 1, int pageSize = 1);
    }
}