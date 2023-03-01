using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEntity;
using MyDAL;
namespace MyBLL
{
    public class ProfessionInfoBLL
    {
        ProfessionInfoDAL dal = new ProfessionInfoDAL();
        #region 新增
        public int Add(ProfessionInfoEntity entity)
        {
            return dal.Add(entity);
        }
        #endregion

        #region 删除
        public int Delete(int id)
        {
           return dal.Delete(id);
        }
        #endregion

        #region 修改
        public int Update(ProfessionInfoEntity entity)
        {
            return dal.Update(entity);
        }
        #endregion

        #region 列表
        public List<ProfessionInfoEntity> List()
        {
            return dal.List();
        }
        #endregion

        #region 详情
        public ProfessionInfoEntity Detail(int id)
        {
            return dal.Detail(id);
        }
        #endregion
    }
}
