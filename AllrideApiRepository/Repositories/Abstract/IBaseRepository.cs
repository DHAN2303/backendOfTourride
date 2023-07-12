using System.Linq.Expressions;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IBaseRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null);  // GetAll() da koşullu sorgula yazabilmemiz için Linq ların expressionlarından yararlanıyoruz.
        void Delete(T entity);
        T Get(T Entity);  //Expression<Func<T, bool>> expression
        void SaveChanges();
    }
}
