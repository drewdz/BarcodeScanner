using Scanning.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scanning
{
    public interface IScanningService
    {
        #region Properties

        bool Encapsulated { get; }

        List<ScanItem> ScanResults { get; set; }

        #endregion Properties

        #region Methods

        Task BeginPreviewAsync(object context);

        Task StopScanning();

        Task DetectAsync(Action<List<ScanItem>> codeFound);

        Task DetectDataAsync(Action<byte[]> dataFound);

        #endregion Methods
    }
}
