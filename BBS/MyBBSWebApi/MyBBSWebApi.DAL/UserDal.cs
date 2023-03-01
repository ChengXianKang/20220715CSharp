using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MyBBSWebApi.DAL.Core;
using MyBBSWebApi.Models;

namespace MyBBSWebApi.DAL
{
    public class UserDal
    {
        public List<Users> GetAll()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM Users");
            List<Users> userList = ToModelList(res);
            return userList;
        }
        public List<Users> GetUserByUserNoAndPassword(string userNo, string password)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE UserNo=@UserNo AND Password=@Password",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@Password", password)
                );

            List<Users> userList = ToModelList(res);
            return userList;
        }
        public List<Users> GetUserByUserNoAndAutoLoginTag(string userNo, string autoLoginTag)
        {
            try
            {
                DataTable res = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE UserNo=@UserNo AND AutoLoginTag=@AutoLoginTag",
              new SqlParameter("@UserNo", userNo),
              new SqlParameter("@AutoLoginTag", autoLoginTag)
              );

                List<Users> userList = ToModelList(res);
                return userList;
            }
            catch (System.Exception)
            {
                 return default;
            }

        }
        public Users GetUserById(int id)
        {
            DataRow row = null;
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE Id=@Id",
                new SqlParameter("@Id", id));
            if (res.Rows.Count > 0)
            {
                row = res.Rows[0];
            }
            Users user = ToModel(row);
            return user;
        }
        public Users GetUserByToken(string token)
        {
            DataRow row = null;
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE Token=@Token",
                new SqlParameter("@Token", token));
            if (res.Rows.Count > 0)
            {
                row = res.Rows[0];
            }
            Users user = ToModel(row);
            return user;
        }
        public int AddUser(Users user)
        {
            return SqlHelper.ExecuteNonQuery(
                  "INSERT INTO Users(UserNo,UserName,UserLevel,Password,IsDelete) VALUES(@UserNo,@UserName,@UserLevel,@Password,@IsDelete)",
                  new SqlParameter("@UserNo", user.UserNo),
                  new SqlParameter("@UserName", user.UserName),
                  new SqlParameter("@UserLevel", user.UserLevel),
                  new SqlParameter("@Password", user.Password),
                  new SqlParameter("@IsDelete", user.IsDelete));
        }

        public int UpdateUserOfUI(Users user)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM Users Where Id = @Id", new SqlParameter("@Id", user.Id));
            int rowCount = 0;
            if (res.Rows.Count > 0)
            {
                DataRow row = res.Rows[0];
                rowCount = SqlHelper.ExecuteNonQuery(
             "UPDATE Users Set UserNo = @UserNo,UserName = @UserName,UserLevel=@UserLevel, Password = @Password, Token = @Token, AutoLoginTag = @AutoLoginTag , AutoLoginLimitTime = @AutoLoginLimitTime WHERE Id = @Id",
             new SqlParameter("@UserNo", user.UserNo),
             new SqlParameter("@UserName", user.UserName),
             new SqlParameter("@UserLevel", user.UserLevel),
             new SqlParameter("@Password", user.Password),
             new SqlParameter("@Token", user.Token),
             new SqlParameter("@AutoLoginTag", user.AutoLoginTag),
             new SqlParameter("@AutoLoginLimitTime", user.AutoLoginLimitTime),
             new SqlParameter("@Id", user.Id)
             );
            }
            return rowCount;
        }
        public int UpdateUser(int id, string userNo, string userName, string password, int? userLevel, Guid? token, Guid? autoLoginTag, DateTime? autoLoginLimitTime)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM Users Where Id = @Id", new SqlParameter("@Id", id));
            int rowCount = 0;
            if (res.Rows.Count > 0)
            {
                DataRow row = res.Rows[0];
                Users user = new Users();
                user.Id = (int)row["Id"];
                user.UserNo = userNo ?? row["UserNo"].ToString();
                user.UserName = userName ?? row["UserName"].ToString();
                user.UserLevel = userLevel ?? (int)row["UserLevel"];
                user.Password = password ?? row["Password"].ToString();
                user.Token = token ?? new Guid();
                user.AutoLoginTag = autoLoginTag ?? new Guid();
                user.AutoLoginLimitTime = autoLoginLimitTime;
                rowCount = SqlHelper.ExecuteNonQuery(
             "UPDATE Users Set UserNo = @UserNo,UserName = @UserName,UserLevel=@UserLevel, Password = @Password, Token = @Token, AutoLoginTag = @AutoLoginTag , AutoLoginLimitTime = @AutoLoginLimitTime WHERE Id = @Id",
             new SqlParameter("@UserNo", user.UserNo),
             new SqlParameter("@UserName", user.UserName),
             new SqlParameter("@UserLevel", user.UserLevel),
             new SqlParameter("@Password", user.Password),
             new SqlParameter("@Token", user.Token),
             new SqlParameter("@AutoLoginTag", user.AutoLoginTag),
             new SqlParameter("@AutoLoginLimitTime", user.AutoLoginLimitTime),
             new SqlParameter("@Id", user.Id)
             );
            }
            return rowCount;
        }

        public int RemoveUser(int id)
        {

            return SqlHelper.ExecuteNonQuery(
                  "DELETE FROM Users WHERE Id = @Id",
                  new SqlParameter("@Id", id));
        }
        private List<Users> ToModelList(DataTable table)
        {
            List<Users> userList = new List<Users>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                Users user = ToModel(row);
                userList.Add(user);
            }
            return userList;
        }
        private Users ToModel(DataRow row)
        {
            Users user = new Users();
            user.Id = (int)SqlHelper.FromDbValue(row["Id"]);
            user.UserNo = SqlHelper.FromDbValue(row["UserNo"]).ToString();
            user.UserName = SqlHelper.FromDbValue(row["UserName"]).ToString();
            user.UserLevel = (int)SqlHelper.FromDbValue(row["UserLevel"]);
            user.Password = SqlHelper.FromDbValue(row["Password"]).ToString();
            user.IsDelete = (bool)SqlHelper.FromDbValue(row["IsDelete"]);
            user.Token = (Guid?)SqlHelper.FromDbValue(row["Token"]);
            user.AutoLoginTag = (Guid?)SqlHelper.FromDbValue(row["AutoLoginTag"]);
            user.AutoLoginLimitTime = (DateTime?)SqlHelper.FromDbValue(row["AutoLoginLimitTime"]);
            return user;
        }
    }
}
