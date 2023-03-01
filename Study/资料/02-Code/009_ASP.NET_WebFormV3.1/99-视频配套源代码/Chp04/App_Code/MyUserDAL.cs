using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// MyUserDAL 的摘要说明
/// </summary>
public class MyUserDAL
{
    DBHelper db = new DBHelper();
    public MyUserDAL()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    #region 用户登录
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="strAcc">用户名</param>
    /// <param name="strPwd">密码</param>
    /// <returns>布尔值,登录成功返回用户ID,登录失败返回0</returns>
    public MyUserEntity Login(string strAcc, string strPwd)
    {
        string sql = "select * from MyUser where UserAccount=@UserAccount and UserPwd=@UserPwd";
        db.PrepareSql(sql);
        db.SetParameter("UserAccount", strAcc);
        db.SetParameter("UserPwd", strPwd);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        if (dt.Rows.Count == 1)
        {
            MyUserEntity entity = new MyUserEntity();
            entity.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
            entity.UserAccount = dt.Rows[0]["UserAccount"].ToString();
            entity.UserPwd = dt.Rows[0]["UserPwd"].ToString();
            entity.UserMail = dt.Rows[0]["UserMail"].ToString();
            entity.UserPhone = dt.Rows[0]["UserPhone"].ToString();
            entity.UserSex = dt.Rows[0]["UserSex"].ToString();
            return entity;
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region 根据用户ID查询用户信息
    public MyUserEntity Detail(int userId)
    {
        string sql = "select * from MyUser where UserId = @UserId";
        DataTable dt = new DataTable();
        db.PrepareSql(sql);
        db.SetParameter("UserId", userId);
        dt = db.ExecQuery();
        if (dt.Rows.Count == 0)
            return null;
        MyUserEntity entity = new MyUserEntity();
        entity.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
        entity.UserAccount = dt.Rows[0]["UserAccount"].ToString();
        entity.UserPwd = dt.Rows[0]["UserPwd"].ToString();
        entity.UserMail = dt.Rows[0]["UserMail"].ToString();
        entity.UserPhone = dt.Rows[0]["UserPhone"].ToString();
        entity.UserSex = dt.Rows[0]["UserSex"].ToString();
        return entity;
    }
    #endregion
}