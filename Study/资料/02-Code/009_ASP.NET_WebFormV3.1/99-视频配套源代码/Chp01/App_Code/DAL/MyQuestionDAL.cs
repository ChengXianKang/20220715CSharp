using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// 密码问题的数据操作和访问
/// </summary>
public class MyQuestionDAL
{
    DBHelper db = new DBHelper();

    #region 列表
    public List<MyQuestionEntity> List()
    {
        string sql = "select * from MyQuestion";
        db.PrepareSql(sql);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        List<MyQuestionEntity> list = new List<MyQuestionEntity>();
        foreach (DataRow item in dt.Rows)
        {
            MyQuestionEntity entity = new MyQuestionEntity();
            entity.QuestionId = int.Parse(item["QuestionId"].ToString());
            entity.QuestionTitle = item["QuestionTitle"].ToString();
            list.Add(entity);
        }
        return list;
    }
    #endregion
}