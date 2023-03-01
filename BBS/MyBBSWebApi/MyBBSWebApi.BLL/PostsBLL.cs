using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MyBBSWebApi.BLL.Interfaces;
using MyBBSWebApi.DAL;
using MyBBSWebApi.DAL.Core;
using MyBBSWebApi.DAL.Factorys;
using MyBBSWebApi.Model;
using MyBBSWebApi.Models.Models;

namespace MyBBSWebApi.BLL
{


    [System.ComponentModel.DataObject]
    public partial class PostsBLL : IPostsBLL
    {
        // PostsDAL mdd = new PostsDAL();
        MyBBSDbContext context = DbContextFactory.GetDbContext();
        /// <summary>
        /// 构造方法
        /// </summary>
        public PostsBLL()
        { }

        // /// <summary>
        // /// 添加记录
        // /// </summary>
        // public void InsertRecord(Posts md)
        // {
        //     // mdd.InsertRecord(md);
        // }

        // /// <summary>
        // /// 删除所有记录
        // /// </summary>
        // public void DeleteRecord()
        // {
        //     // mdd.DeleteRecord();
        // }

        // /// <summary>
        // /// 根据Id删除记录
        // /// </summary>
        // public void DeleteRecordById(int Id)
        // {
        //     // mdd.DeleteRecordById(Id);
        // }

        // /// <summary>
        // /// 根据Guid删除记录
        // /// </summary>
        // public void DeleteRecordByGuid(Guid Id)
        // {
        //     // mdd.DeleteRecordByGuid(Id);
        // }

        // /// <summary>
        // /// 修改记录
        // /// </summary>
        // public void UpdateRecord(Posts md)
        // {
        //     // mdd.UpdateRecord(md);
        // }

        // /// <summary>
        // /// 根据Id查询记录
        // /// </summary>
        // public DataTable GetById(int Id)
        // {
        //     // return mdd.GetById(Id);
        // }





        /// <summary>
        /// 查询所有记录
        /// </summary>
        public IEnumerable<Post> GetAll()
        {
            return context.Posts.ToList();
        }

        /// <summary>
        /// 查询所有记录并分页
        /// </summary>
        public IEnumerable<Post> GetAllOfPage(int pageIndex = 1, int pageSize = 1)
        {
            // int? StartPage = (pageIndex * pageSize) - (pageSize - 1);
            // int? EndPage = (pageIndex * pageSize);
            // return mdd.GetAllOfPage(StartPage, EndPage);
            int skipNum = (pageIndex - 1) * pageSize;
            var list = context.Posts.Skip(skipNum).Take(pageSize);
            return list.ToList();
        }




    }
}
/////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////
