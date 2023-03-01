using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 密码问题的实体
/// </summary>
public class MyQuestionEntity
{
    public MyQuestionEntity()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public int QuestionId { get; set; }
    public string QuestionTitle { get; set; }
}