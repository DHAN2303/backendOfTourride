using AllrideApiCore.Entities;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface INewsRepository
    {
        public News GetById(int Id);  //News news
        //public IEnumerable<News> GetLast2News();
        public News Post(News news);
        public void SaveChanges();
        public List<News> GetAll();
        public int NewsTotalCount();
        public List<News> GetLast2News();
    }
}
