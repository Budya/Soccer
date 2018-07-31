using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class AgreedPlayersListRepository : BaseRepository<AgreedPlayersList>
    {
        public override AgreedPlayersList GetById(int id)
        {
            AgreedPlayersList result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.AgreedPlayersLists.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<AgreedPlayersList> GetByIdAsync(int id)
        {

            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<AgreedPlayersList>.Factory.StartNew(() =>
                {
                    return context.AgreedPlayersLists.Where(t => t.Id == id).FirstOrDefault();
                });
            }

        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var agreedPlayersList = context.AgreedPlayersLists.FirstOrDefault(t => t.Id == id);
                context.Entry(agreedPlayersList).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var agreedPlayersList = context.AgreedPlayersLists.FirstOrDefault(t => t.Id == id);
                context.Entry(agreedPlayersList).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}