using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public override User GetById(int id)
        {
            User result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.Users.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<User>.Factory.StartNew(() =>
                {
                    return context.Users.Where(t => t.Id == id).FirstOrDefault();
                });                
            }
            
        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var user = context.Users.FirstOrDefault(t => t.Id == id);
                context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var user = context.Users.FirstOrDefault(t => t.Id == id);
                context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}