using System;
using System.Collections.Generic;
using MyBBSWebApi.Common;
using MyBBSWebApi.DAL;
using MyBBSWebApi.Models;

namespace MyBBSWebApi.BLL
{
    public class UserBll : IUserBll
    {
        UserDal userDal = new UserDal();
        public List<Users> GetAll()
        {
            return userDal.GetAll().FindAll(m => !m.IsDelete);
        }
        public Users CheakLogin(string userNo, string password)
        {
            List<Users> userlist = userDal.GetUserByUserNoAndPassword(userNo, password.ToMd5());
            if (userlist.Count <= 0)
            {
                userlist = userDal.GetUserByUserNoAndAutoLoginTag(userNo, password);
                if(userlist == null) return default;
                userlist = userlist.FindAll(m=>m.AutoLoginLimitTime > DateTime.Now);
                if (userlist.Count <= 0)
                {
                    return default;
                }
                else
                {
                    return GetLoginResult(userlist, false);
                }
            }
            else
            {
                return GetLoginResult(userlist, true);
            }
        }

        private Users GetLoginResult(List<Users> userlist, bool isPasswordLogin)
        {
            Users user = userlist.Find(m => !m.IsDelete);
            user.Token = Guid.NewGuid();
            //如果是用户密码登录的，则赋予新的
            if (isPasswordLogin){
                user.AutoLoginTag = Guid.NewGuid();
                user.AutoLoginLimitTime = DateTime.Now.AddDays(7);
                }
            UpdateUser(user.Id, user.UserNo, user.UserName, user.Password, user.UserLevel, user.Token, user.AutoLoginTag, user.AutoLoginLimitTime);
            //if (user == null) return default;
            //else return user;
            return user;
        }

        public Users GetUserByToken(string token)
        {
            Users user = userDal.GetUserByToken(token);
            if (user == null)
            {
                throw new Exception("token错误");
            }
            return user;

        }

        public string AddUser(Users user)
        {
            UserDal userDal = new UserDal();
            user.IsDelete = false;
            user.Password = user.Password.ToMd5();
            int rows = userDal.AddUser(user);
            if (rows > 0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }
        public string UpdateUserOfUI(Users user)
        {

            UserDal userDal = new UserDal();
            int rows = userDal.UpdateUserOfUI(user);
            if (rows > 0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }
        public string UpdateUser(
            int id,
            string userNo,
            string userName,
            string password,
            int? userLevel,
            Guid? token,
            Guid? autoLoginTag,
            DateTime? autoLoginLimitTime)
        {

            UserDal userDal = new UserDal();
            int rows = userDal.UpdateUser(id, userNo, userName, password, userLevel, token, autoLoginTag, autoLoginLimitTime);
            if (rows > 0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }
        public string RemoveUser(int id)
        {

            UserDal userDal = new UserDal();
            int rows = userDal.RemoveUser(id);
            if (rows > 0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }
    }
}
