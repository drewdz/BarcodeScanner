using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace Scanning
{
    public class PluginLoader : IMvxPluginLoader
    {
        public static PluginLoader Instance = new PluginLoader();

        public void EnsureLoaded()
        {
            var manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
    }
}
