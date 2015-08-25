using Scanning.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scanning
{
    public abstract class ScanningServiceBase : IScanningService
    {
        #region Operations

        abstract public Task BeginPreviewAsync(object preview);

        abstract public Task DetectAsync(Action<List<ScanItem>> codeFound);

        abstract public Task DetectDataAsync(Action<byte[]> dataFound);

        virtual public async Task StopScanning() { }

        #endregion Operations

        #region Properties

        public virtual bool Encapsulated { get { return true; } }

        public virtual List<ScanItem> ScanResults { get; set; }

        #endregion Properties

        #region Internal Logic

        virtual protected List<ScanItem> DecodeScan(string data)
        {
            System.Diagnostics.Debug.WriteLine("Raw Scan: {0}", data);
            string[] codes = data.Replace("\0", "").Split(new char[] { '%' });
            return codes.Where(code => !string.IsNullOrEmpty(code)).Select(code => new ScanItem { Id = ScanItem.Counter, Text = code }).ToList();
        }

        #endregion Internal Logic
    }
}
