using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

using Scanning.Entities;

namespace Scanning.Droid
{
    public class Plugin : IMvxConfigurablePlugin
    {
        private ScanningConfigCollection _Config;

        public void Load()
        {
            Mvx.LazyConstructAndRegisterSingleton<IScanningService>(() =>
            {
                return new ScanningService(_Config);
            });
        }

        public void Configure(IMvxPluginConfiguration configuration)
        {
            if ((configuration == null) || !(configuration is ScanningConfigCollection)) return;
            _Config = configuration as ScanningConfigCollection;
        }
    }
}