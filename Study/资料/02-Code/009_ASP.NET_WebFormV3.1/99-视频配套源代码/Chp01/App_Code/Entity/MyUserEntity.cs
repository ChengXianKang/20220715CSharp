using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 会员信息的实体
/// </summary>
public class MyUserEntity
{
    public MyUserEntity()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public int UserId { get; set; }
    public string UserAccount { get; set; }
    public string UserPwd { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string UserMail { get; set; }
    public string UserPhone { get; set; }
    public string UserSex { get; set; }
    public string UserPro { get; set; }
    public string UserHobby { get; set; }
    public string UserPhoto { get; set; }
    public string UserSelf { get; set; }
}