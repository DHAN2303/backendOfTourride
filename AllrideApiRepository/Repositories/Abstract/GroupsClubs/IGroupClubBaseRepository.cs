namespace AllrideApiRepository.Repositories.Abstract.GroupsClubs
{
    public interface IGroupClubBaseRepository<T> where T : class
    {
        string GetMedia(T entity);
    }
}
