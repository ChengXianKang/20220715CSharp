using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEntity;
using System.Data;

namespace MyDAL
{
    public class StudentInfoDAL
    {
        #region 新增
        public int Add(StudentInfoEntiy entity)
        {
            string sql = "insert into StudentInfo(StuID,StuName,StuAge,StuSex,StuHobby,ProfessionID) values(@StuID,@StuName,@StuAge,@StuSex,@StuHobby,@ProfessionID)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("StuID", entity.StuID);
            DBHelper.SetParameter("StuName", entity.StuName);
            DBHelper.SetParameter("StuAge", entity.StuAge);
            DBHelper.SetParameter("StuSex", entity.StuSex);
            DBHelper.SetParameter("StuHobby", entity.StuHobby);
            DBHelper.SetParameter("ProfessionID", entity.ProfessionID);
            return DBHelper.ExecNonQuery();
        }
        #endregion

        #region 删除
        public int Delete(string id)
        {
            string sql = "delete from StudentInfo where StuID=@StuID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("StuID", id);
            return DBHelper.ExecNonQuery();
        }
        #endregion

        #region 修改
        public int Update(StudentInfoEntiy entity)
        {
            string sql = "update StudentInfo set StuName=@StuName,StuAge=@StuAge,StuSex=@StuSex,StuHobby=@StuHobby,ProfessionID=@ProfessionID where StuID=@StuID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("StuName", entity.StuName);
            DBHelper.SetParameter("StuAge", entity.StuAge);
            DBHelper.SetParameter("StuSex", entity.StuSex);
            DBHelper.SetParameter("StuHobby", entity.StuHobby);
            DBHelper.SetParameter("ProfessionID", entity.ProfessionID);
            DBHelper.SetParameter("StuID", entity.StuID);
            return DBHelper.ExecNonQuery();
        }
        #endregion

        #region 列表
        public List<StudentInfoEntiy> List()
        {
            string sql = "select * from StudentInfo";
            DataTable dt = new DataTable();
            DBHelper.PrepareSql(sql);
            dt = DBHelper.ExecQuery();
            List<StudentInfoEntiy> list = new List<StudentInfoEntiy>();
            foreach (DataRow item in dt.Rows)
            {
                StudentInfoEntiy entity = new StudentInfoEntiy();
                entity.StuID = item["StuID"].ToString();
                entity.StuName = item["StuName"].ToString();
                entity.StuAge = int.Parse(item["StuAge"].ToString());
                entity.StuSex = item["StuSex"].ToString();
                entity.StuHobby = item["StuHobby"].ToString();
                entity.ProfessionID = int.Parse(item["ProfessionID"].ToString());
                list.Add(entity);
            }
            return list;
        }
        #endregion

        #region 详情
        public StudentInfoEntiy Detail(string id)
        {
            string sql = "select * from StudentInfo where StuID=@StuID";
            DataTable dt = new DataTable();
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("StuID", id);
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 0)
                return null;
            StudentInfoEntiy entity = new StudentInfoEntiy();
            entity.StuID = dt.Rows[0]["StuID"].ToString();
            entity.StuName = dt.Rows[0]["StuName"].ToString();
            entity.StuAge = int.Parse(dt.Rows[0]["StuAge"].ToString());
            entity.StuSex = dt.Rows[0]["StuSex"].ToString();
            entity.StuHobby = dt.Rows[0]["StuHobby"].ToString();
            entity.ProfessionID = int.Parse(dt.Rows[0]["ProfessionID"].ToString());
            return entity;
        }
        #endregion

        //-----------------------------------------------------------------------------
        #region 条件查询
        public List<StudentInfoEntiy> Search(StudentInfoEntiy searchObj)
        {
            string sql = "select * from StudentInfo inner join ProfessionInfo on StudentInfo.ProfessionID = ProfessionInfo.ProfessionID where 1 = 1";
            if (searchObj.ProfessionID != 0)
                sql += " and StudentInfo.ProfessionID = " + searchObj.ProfessionID;
            if (!searchObj.StuName.Equals(""))
                sql += " and StuName like '%" + searchObj.StuName + "%'";
            DataTable dt = new DataTable();
            DBHelper.PrepareSql(sql);
            dt = DBHelper.ExecQuery();
            List<StudentInfoEntiy> list = new List<StudentInfoEntiy>();
            foreach (DataRow item in dt.Rows)
            {
                StudentInfoEntiy entity = new StudentInfoEntiy();
                entity.StuID = item["StuID"].ToString();
                entity.StuName = item["StuName"].ToString();
                entity.StuAge = int.Parse(item["StuAge"].ToString());
                entity.StuSex = item["StuSex"].ToString();
                entity.StuHobby = item["StuHobby"].ToString();
                entity.ProfessionID = int.Parse(item["ProfessionID"].ToString());
                entity.ProfessionName = item["ProfessionName"].ToString();
                list.Add(entity);
            }
            return list;
        }
        #endregion
    }
}
