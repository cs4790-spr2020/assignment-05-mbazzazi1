using BlabberApp.DataStore.Adapters;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.DataStore.Plugins;

namespace BlabberApp.Services
{
    public class BlabServiceFactory
    {
        public BlabAdapter CreateBlabAdapter(IBlabPlugin plugin = null)
        {
            if (plugin == null)
            {
                plugin = this.CreateBlabPlugin();
            }

            return new BlabAdapter(plugin);
        }
        public IBlabPlugin CreateBlabPlugin(string type = "")
        {
            if (type.ToUpper().Equals("MYSQL"))
            {
                return new MySqlBlab();
            }

            return new InMemory();
        }

        public BlabService CreateBlabService(BlabAdapter adapter = null)
        {
            if (adapter == null)
            {
                adapter = this.CreateBlabAdapter();
            }

            return new BlabService(adapter);
        }
    }
}
