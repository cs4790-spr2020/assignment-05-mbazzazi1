using BlabberApp.Domain.Interfaces;

namespace BlabberApp.DataStore.Interfaces
{
    public interface IUserPlugin : IPlugin
    {
        IEntity ReadByUserEmail(string email);
    }
}