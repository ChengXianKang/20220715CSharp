using Microsoft.EntityFrameworkCore;
using MyBBSWebApi.DAL.Core;

namespace MyBBSWebApi.DAL.Factorys
{
    public class DbContextFactory
    {
        private static MyBBSDbContext _dbContext = null;
        private DbContextFactory()
        {

        }
        public static MyBBSDbContext GetDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = new MyBBSDbContext();
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            return _dbContext;
        }
    }
}