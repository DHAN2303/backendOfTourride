using AllrideApiRepository.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class BaseRepository<T, TBContext> : IBaseRepository<T>
        where T : class
        where TBContext : DbContext  // Contexti ne olduğunu belirttik 
    {
        protected TBContext Context;
        public BaseRepository(TBContext context)
        {
            Context = context;
        }
        public T Add(T entity)
        {
            return Context.Add(entity).Entity; // Burdaki Entry sayesinde hangi tabloyu kullanacağını kendisi çözüyor.

        }

        public T Update(T entity)
        {
            Context.Update(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> expression)  //Burada bir koşulum yok ve 1 tane sonuc bekliyorum
        {
            return Context.Set<T>().FirstOrDefault(expression);
        }
        /// <summary>
        ///  Null ise bütün dataları dönecek, değil ise koşulu sağlayan datalar dönecek
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
                return Context.Set<T>().ToList();
            else
            {
                return Context.Set<T>().Where(expression);
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public T Get(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}
