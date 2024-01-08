using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class NewsRepository : INewsRepository
    {

        protected readonly AllrideApiDbContext _context;
        public NewsRepository(AllrideApiDbContext context)
        {
            _context = context;
        }

        public News GetById(int Id)  // Haberin  İd sini getiren vt kod // News news
        {
            //return _context.news.SingleOrDefault(x=>x.Id == Id);
            return _context.news.Find(Id);
        }


        //public IEnumerable<News> GetLast2News()
        //{
        //    return _context.news.OrderByDescending(e => e.CreatedDate).Take(2);
        //}

        public News Post(News news)
        {
            return _context.news.Add(news).Entity;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


    }
    public class UserNewsReactionRepository : IUserNewsReactionRepository
    {
        protected readonly AllrideApiDbContext _context;
        public UserNewsReactionRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        //public UserNewsReaction Get(News news)
        //{
        //    return _context.user_news_reaction.SingleOrDefault(x => x.NewsId == news.Id);
        //}

        public int GetById(int Id)
        {
            return _context.user_news_reaction.SingleOrDefault(x => x.NewsId == Id).Id;

        }
        public UserNewsReaction Add(UserNewsReaction userNewsReaction)
        {
            return _context.Add(userNewsReaction).Entity;
        }

        public UserNewsReaction Get(int Id)   
        {
            return _context.user_news_reaction.Find(Id);
           // return _context.user_news_reaction.SingleOrDefault(x => x.Id == Id);
        }
        public UserNewsReaction GetUserIdWithNewsId(int userId, int NewsId)
        {
            // return _context.user_news_reaction.Include(x=>x.UserId).Include(y=>y.NewsId).SingleOrDefault(x=>x.)
            //return _context.user_news_reaction.Where(x => x.UserId == userId).Where(x => x.NewsId == NewsId).SingleOrDefault();
            return  _context.user_news_reaction.Include(o => o.NewsId).SingleOrDefault();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

