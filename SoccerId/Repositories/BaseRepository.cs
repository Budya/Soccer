using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public abstract class BaseRepository<TClass> : IRepository<TClass> where TClass : class
    {
        public BaseRepository()
        {

        }
        public TClass Add(TClass item)
        {
            TClass result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.Set<TClass>().Add(item);
                context.SaveChanges();
            }
            return result;
        }

        public async Task<TClass> AddAsync(TClass item)
        {
            TClass result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.Set<TClass>().Add(item);
                await context.SaveChangesAsync();
            }
            return result;
        }

        public virtual void Remove(int id) { }

        public virtual Task RemoveAsync(int id)
        {
            return null;
        }

        public IEnumerable<TClass> Find(Func<TClass, bool> predicate, string navigationProperty = "")
        {
            IEnumerable<TClass> result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                if (String.IsNullOrEmpty(navigationProperty))
                {
                    result = (from t in context.Set<TClass>().Where(predicate)
                              select t).ToList();
                }
                else
                {
                    result = (from t in context.Set<TClass>().Include(navigationProperty).Where(predicate)
                              select t).ToList();
                }
            }
            return result;
        }

        public async Task<IEnumerable<TClass>> FindAsync(Func<TClass, bool> predicate, string navigationProperty = "")
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<IEnumerable<TClass>>.Factory.StartNew(() =>
                {

                    if (String.IsNullOrEmpty(navigationProperty))
                    {
                        return (from t in context.Set<TClass>().Where(predicate)
                                select t).ToList();
                    }
                    else
                    {
                        return (from t in context.Set<TClass>().Include(navigationProperty).Where(predicate)
                                select t).ToList();
                    }
                });
            }
        }

        public IEnumerable<TClass> GetAll()
        {
            IEnumerable<TClass> result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = (from t in context.Set<TClass>()
                          select t).ToList();
            }
            return result;
        }

        public async Task<IEnumerable<TClass>> GetAllAsync()
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<IEnumerable<TClass>>.Factory.StartNew(() => {
                    return (from t in context.Set<TClass>()
                            select t).ToList();
                });
            }
        }

        public virtual TClass GetById(int id)
        {
            return null;
        }

        public virtual Task<TClass> GetByIdAsync(int id)
        {
            return null;
        }

        public void Update(TClass item)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public async Task UpdateAsync(TClass item)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                context.Entry(context.Set<TClass>()).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}