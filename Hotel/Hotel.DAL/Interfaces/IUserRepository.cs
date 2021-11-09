using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByLogin(string login);
    }
}
