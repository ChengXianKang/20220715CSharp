using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// 会员信息的数据操作和访问(增，删，修改，查（列表，详情）)
/// </summary>
public class MyUserDAL
{
    DBHelper db = new DBHelper();

    #region 增加
    public int Add(MyUserEntity entity)
    {
        string sql = @"insert into MyUser(UserAccount,UserPwd,Question,Answer,UserMail,
            UserPhone,UserSex,UserPro,UserHobby,UserPhoto,UserSelf)
            values(@UserAccount,@UserPwd,@Question,@Answer,@UserMail,
            @UserPhone,@UserSex,@UserPro,@UserHobby,@UserPhoto,@UserSelf)";
        db.PrepareSql(sql);
        db.SetParameter("UserAccount", entity.UserAccount);
        db.SetParameter("UserPwd", entity.UserPwd);
        db.SetParameter("Question", entity.Question);
        db.SetParameter("Answer", entity.Answer);
        db.SetParameter("UserMail", entity.UserMail);
        db.SetParameter("UserPhone", entity.UserPhone);
        db.SetParameter("UserSex", entity.UserSex);
        db.SetParameter("UserPro", entity.UserPro);
        db.SetParameter("UserHobby", entity.UserHobby);
        db.SetParameter("UserPhoto", entity.UserPhoto);
        db.SetParameter("UserSelf", entity.UserSelf);
        return db.ExecNonQuery();
    }
    #endregion

    #region 删除
    public int Delete(int id)
    {
        string sql = "delete from MyUser where UserId=@UserId";
        db.PrepareSql(sql);
        db.SetParameter("UserId", id);
        return db.ExecNonQuery();
    }
    #endregion

    #region 修改
    public int Update(MyUserEntity entity)
    {
        string sql = @"update MyUser set UserAccount=@UserAccount,UserPwd=@UserPwd,
            Question=@Question,Answer=@Answer,UserMail=@UserMail,UserPhone=@UserPhone,
            UserSex=@UserSex,UserPro=@UserPro,UserHobby=@UserHobby,UserPhoto=@UserPhoto,
            UserSelf=@UserSelf where UserId=@UserId";
        db.PrepareSql(sql);
        db.SetParameter("UserAccount", entity.UserAccount);
        db.SetParameter("UserPwd", entity.UserPwd);
        db.SetParameter("Question", entity.Question);
        db.SetParameter("Answer", entity.Answer);
        db.SetParameter("UserMail", entity.UserMail);
        db.SetParameter("UserPhone", entity.UserPhone);
        db.SetParameter("UserSex", entity.UserSex);
        db.SetParameter("UserPro", entity.UserPro);
        db.SetParameter("UserHobby", entity.UserHobby);
        db.SetParameter("UserPhoto", entity.UserPhoto);
        db.SetParameter("UserSelf", entity.UserSelf);
        db.SetParameter("UserId", entity.UserId);
        return db.ExecNonQuery();
    }
    #endregion

    #region 列表
    public DataTable Select()
    {
        string sql = "select * from MyUser";
        db.PrepareSql(sql);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        return dt;
    }
    public List<MyUserEntity> List()
    {
        string sql = "select * from MyUser";
        db.PrepareSql(sql);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        List<MyUserEntity> list = new List<MyUserEntity>();
        foreach (DataRow item in dt.Rows)
        {
            MyUserEntity entity = new MyUserEntity();
            entity.UserId = int.Parse(item["UserId"].ToString());
            entity.UserAccount = item["UserAccount"].ToString();
            entity.UserPwd = item["UserAccount"].ToString();
            entity.Question = item["UserAccount"].ToString();
            entity.Answer = item["UserAccount"].ToString();
            entity.UserMail = item["UserAccount"].ToString();
            entity.UserPhone = item["UserAccount"].ToString();
            entity.UserSex = item["UserAccount"].ToString();
            entity.UserPro = item["UserAccount"].ToString();
            entity.UserHobby = item["UserAccount"].ToString();
            entity.UserPhoto = item["UserAccount"].ToString();
            entity.UserSelf = item["UserAccount"].ToString();
            list.Add(entity);
        }
        return list;
    }
    #endregion

    #region 详情
    public MyUserEntity Detail(int id)
    {
        string sql = "select * from MyUser where UserId=@UserId";
        db.PrepareSql(sql);
        db.SetParameter("UserId",id);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        if (dt.Rows.Count == 0)
            return null;
        MyUserEntity entity = new MyUserEntity();
        entity.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
        entity.UserAccount = dt.Rows[0]["UserAccount"].ToString();
        entity.UserPwd = dt.Rows[0]["UserPwd"].ToString();
        entity.Question = dt.Rows[0]["Question"].ToString();
        entity.Answer = dt.Rows[0]["Answer"].ToString();
        entity.UserMail = dt.Rows[0]["UserMail"].ToString();
        entity.UserPhone = dt.Rows[0]["UserPhone"].ToString();
        entity.UserSex = dt.Rows[0]["UserSex"].ToString();
        entity.UserPro = dt.Rows[0]["UserPro"].ToString();
        entity.UserHobby = dt.Rows[0]["UserHobby"].ToString();
        entity.UserPhoto = dt.Rows[0]["UserPhoto"].ToString();
        entity.UserSelf = dt.Rows[0]["UserSelf"].ToString();
        return entity;
    }
    public MyUserEntity Detail(string acc)
    {
        string sql = "select * from MyUser where UserAccount=@UserAccount";
        db.PrepareSql(sql);
        db.SetParameter("UserAccount", acc);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        if (dt.Rows.Count == 0)
            return null;
        MyUserEntity entity = new MyUserEntity();
        entity.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
        entity.UserAccount = dt.Rows[0]["UserAccount"].ToString();
        entity.UserPwd = dt.Rows[0]["UserPwd"].ToString();
        entity.Question = dt.Rows[0]["Question"].ToString();
        entity.Answer = dt.Rows[0]["Answer"].ToString();
        entity.UserMail = dt.Rows[0]["UserMail"].ToString();
        entity.UserPhone = dt.Rows[0]["UserPhone"].ToString();
        entity.UserSex = dt.Rows[0]["UserSex"].ToString();
        entity.UserPro = dt.Rows[0]["UserPro"].ToString();
        entity.UserHobby = dt.Rows[0]["UserHobby"].ToString();
        entity.UserPhoto = dt.Rows[0]["UserPhoto"].ToString();
        entity.UserSelf = dt.Rows[0]["UserSelf"].ToString();
        return entity;
    }
    #endregion

    #region 检查用户名是否可用（可用返回true,不可用返回false）
    public bool IsAccCanUse(string strAcc)
    {
        string sql = "select count(*) from MyUser where UserAccount = @UserAccount";
        db.PrepareSql(sql);
        db.SetParameter("UserAccount", strAcc);
        int count = (int)db.ExecScalar();
        if (count == 0)
            return true;
        else
            return false;
    }
    #endregion

    #region 登录
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="acc">用户名</param>
    /// <param name="pwd">密码</param>
    /// <returns>会员对象或null</returns>
    public MyUserEntity Login(string acc, string pwd)
    {
        string sql = "select * from MyUser where UserAccount=@UserAccount and UserPwd=@UserPwd";
        db.PrepareSql(sql);
        db.SetParameter("UserAccount", acc);
        db.SetParameter("UserPwd",pwd);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        if (dt.Rows.Count == 1) //登录成功
        {
            MyUserEntity entity = new MyUserEntity();
            entity.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
            entity.UserAccount = dt.Rows[0]["UserAccount"].ToString();
            entity.UserPwd = dt.Rows[0]["UserPwd"].ToString();
            entity.Question = dt.Rows[0]["Question"].ToString();
            entity.Answer = dt.Rows[0]["Answer"].ToString();
            entity.UserMail = dt.Rows[0]["UserMail"].ToString();
            entity.UserPhone = dt.Rows[0]["UserPhone"].ToString();
            entity.UserSex = dt.Rows[0]["UserSex"].ToString();
            entity.UserPro = dt.Rows[0]["UserPro"].ToString();
            entity.UserHobby = dt.Rows[0]["UserHobby"].ToString();
            entity.UserPhoto = dt.Rows[0]["UserPhoto"].ToString();
            entity.UserSelf = dt.Rows[0]["UserSelf"].ToString();
            return entity;
        }
        else
            return null;
    }
    #endregion
}