using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEntity;
using System.Data;

namespace MyDAL
{
    public class ProfessionInfoDAL
    {
        #region 新增
        public int Add(ProfessionInfoEntity entity)
        {
            string sql = "insert into ProfessionInfo(professionName) values(@professionName)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProfessionName",entity.ProfessionName);
            return DBHelper.ExecNonQuery();
        }
        #endregion

        #region 删除
        public int Delete(int id)
        {
            string sql = "delete from ProfessionInfo where ProfessionID=@ProfessionID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProfessionID", id);
            return DBHelper.ExecNonQuery();
        }
        #endregion

        #region 修改
        public int Update(ProfessionInfoEntity entity)
        {
            string sql = "update ProfessionInfo set professionName=@professionName where ProfessionID=@ProfessionID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("professionName", entity.ProfessionName);
            DBHelper.SetParameter("ProfessionID", entity.ProfessionID);
            return DBHelper.ExecNonQuery();
        }
        #endregion

        #region 列表
        public List<ProfessionInfoEntity> List()
        {
            string sql = "select * from ProfessionInfo";
            DataTable dt = new DataTable();
            DBHelper.PrepareSql(sql);
            dt = DBHelper.ExecQuery();
            List<ProfessionInfoEntity> list = new List<ProfessionInfoEntity>();
            foreach (DataRow item in dt.Rows)
            {
                ProfessionInfoEntity entity = new ProfessionInfoEntity();
                entity.ProfessionID = int.Parse(item["ProfessionID"].ToString());
                entity.ProfessionName = item["ProfessionName"].ToString();
                list.Add(entity);
            }
            return list;
        }
        #endregion

        #region 详情
        public ProfessionInfoEntity Detail(int id)
        {
            string sql = "select * from ProfessionInfo where ProfessionID=@ProfessionID";
            DataTable dt = new DataTable();
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProfessionID", id);
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 0)
                return null;
            ProfessionInfoEntity entity = new ProfessionInfoEntity();
            entity.ProfessionID = int.Parse(dt.Rows[0]["ProfessionID"].ToString());
            entity.ProfessionName = dt.Rows[0]["ProfessionName"].ToString();
            return entity;
        }
        #endregion
    }
}
