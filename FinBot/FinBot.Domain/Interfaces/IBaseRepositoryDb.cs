namespace FinBot.Domain.Interfaces
{
    public interface IBaseRepositoryDb<T> : IRepositoryReader<T> where T : IEntity
    {
    }
}
