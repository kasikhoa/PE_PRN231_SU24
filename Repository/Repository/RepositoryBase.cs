using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RepositoryBase<T> where T : class
    {
        private readonly EnglishPremierLeague2024DbContext englishPremierLeague2024DbContext;

        public RepositoryBase()
        {
            englishPremierLeague2024DbContext = new EnglishPremierLeague2024DbContext();
        }

        public void Create(T entity)
        {
            englishPremierLeague2024DbContext.Set<T>().Add(entity);
            englishPremierLeague2024DbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            englishPremierLeague2024DbContext.Set<T>().Remove(entity);
            englishPremierLeague2024DbContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return englishPremierLeague2024DbContext.Set<T>();
        }

        public void Update(T entity)
        {
            englishPremierLeague2024DbContext.Set<T>().Attach(entity);
            englishPremierLeague2024DbContext.Entry(entity).State = EntityState.Modified;
            englishPremierLeague2024DbContext.SaveChanges();
        }

    }
}
