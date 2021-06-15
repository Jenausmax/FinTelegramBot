namespace FinBot.Domain.Interfaces
{
    public interface IBaseRepositoryDb : IRepositoryDbUser, IRepositoryDbMeeting
    {
        void Save();
    }
}
