using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// GoodTypeDAL 的摘要说明
/// </summary>
public class GoodTypeDAL
{
    DBHelper db = new DBHelper();
    #region 列表
    public List<GoodTypeEntity> List()
    {
        string sql = "select * from GoodType";
        db.PrepareSql(sql);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        List<GoodTypeEntity> list = new List<GoodTypeEntity>();
        foreach (DataRow dr in dt.Rows)
        {
            GoodTypeEntity entity = new GoodTypeEntity();
            entity.TypeId = int.Parse(dr["TypeId"].ToString());
            entity.ParentId = int.Parse(dr["ParentId"].ToString());
            entity.TypeName = dr["TypeName"].ToString();
            list.Add(entity);
        }
        return list;
    }
    #endregion
}