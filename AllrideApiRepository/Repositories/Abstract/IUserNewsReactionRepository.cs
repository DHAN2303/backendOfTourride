using AllrideApiCore.Entities.Users;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IUserNewsReactionRepository
    {
        //public UserNewsReaction Get(News news);
        public UserNewsReaction GetUserIdWithNewsId(int userId, int NewsId);
        public UserNewsReaction Add(UserNewsReaction userNewsReaction);
        public UserNewsReaction Get(int Id);
        public int GetById(int Id);
        public void SaveChanges();
    }
}
