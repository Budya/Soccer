using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class ChatRepository : BaseRepository<Chat>
    {
        public override Chat GetById(int id)
        {
            Chat result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.Chats.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<Chat> GetByIdAsync(int id)
        {

            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<Chat>.Factory.StartNew(() =>
                {
                    return context.Chats.Where(t => t.Id == id).FirstOrDefault();
                });
            }

        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var chat = context.Chats.FirstOrDefault(t => t.Id == id);
                context.Entry(chat).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var chat = context.Chats.FirstOrDefault(t => t.Id == id);
                context.Entry(chat).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}