using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEntity;
using MyDAL;
namespace MyBLL
{
    public class StudentInfoBLL
    {
        StudentInfoDAL dal = new StudentInfoDAL();
        #region 新增
        public int Add(StudentInfoEntiy entity)
        {
            return dal.Add(entity);
        }
        #endregion

        #region 删除
        public int Delete(string id)
        {
            return dal.Delete(id);
        }
        #endregion

        #region 修改
        public int Update(StudentInfoEntiy entity)
        {
            return dal.Update(entity);
        }
        #endregion

        #region 列表
        public List<StudentInfoEntiy> List()
        {
            return dal.List();
        }
        #endregion

        #region 详情
        public StudentInfoEntiy Detail(string id)
        {
            return dal.Detail(id);
        }
        #endregion

        //----------------------------------------------------------------------------------
        #region 条件查询
        public List<StudentInfoEntiy> Search(StudentInfoEntiy searchObj)
        {
            return dal.Search(searchObj);
        }
        #endregion
    }
}
