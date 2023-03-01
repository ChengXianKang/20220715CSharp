using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// MyHeroDAL 的摘要说明
/// </summary>
public class MyHeroDAL
{
    DBHelper db = new DBHelper();
    public MyHeroDAL()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    #region 查询所有的英雄信息
    public List<MyHeroEntity> List()
    {
        string sql = "select * from MyHero";
        db.PrepareSql(sql);
        DataTable dt = new DataTable();
        dt = db.ExecQuery();
        List<MyHeroEntity> list = new List<MyHeroEntity>();
        foreach (DataRow dr in dt.Rows)
        {
            MyHeroEntity entity = new MyHeroEntity();
            entity.HeroId = int.Parse(dr["HeroId"].ToString());
            entity.HeroName = dr["HeroName"].ToString();
            entity.HeroPic = dr["HeroPic"].ToString();
            entity.HeroSkill = dr["HeroSkill"].ToString();
            entity.HeroEquipment = dr["HeroEquipment"].ToString();
            list.Add(entity);
        }
        return list;
    }
    #endregion

    #region 查询一个英雄的详情
    public MyHeroEntity Detail(int heroId)
    {
        string sql = "select * from MyHero where HeroId = " + heroId;
        DataTable dt = new DataTable();
        db.PrepareSql(sql);
        dt = db.ExecQuery();
        if (dt.Rows.Count == 0)
            return null;
        else
        {
            MyHeroEntity entity = new MyHeroEntity();
            entity.HeroId = int.Parse(dt.Rows[0]["HeroId"].ToString());
            entity.HeroName = dt.Rows[0]["HeroName"].ToString();
            entity.HeroPic = dt.Rows[0]["HeroPic"].ToString();
            entity.HeroSkill = dt.Rows[0]["HeroSkill"].ToString();
            entity.HeroEquipment = dt.Rows[0]["HeroEquipment"].ToString();
            return entity;
        }
    }
    #endregion
}