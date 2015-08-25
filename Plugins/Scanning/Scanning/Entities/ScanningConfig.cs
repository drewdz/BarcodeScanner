using Cirrious.CrossCore.Plugins;

using System.Collections.Generic;

namespace Scanning.Entities
{
    public class ScanningConfigCollection : ICollection<ScanningConfig>, IMvxPluginConfiguration
    {
        #region Fields

        private readonly List<ScanningConfig> _Items;

        #endregion Fields

        #region Constructors

        public ScanningConfigCollection()
        {
            _Items = new List<ScanningConfig>();
        }

        public ScanningConfigCollection(List<ScanningConfig> items)
        {
            _Items = items;
        }

        #endregion Constructors

        #region Properties

        public int Count
        {
            get { return _Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion Properties

        #region Operations

        public void Add(ScanningConfig item)
        {
            _Items.Add(item);
        }

        public void Clear()
        {
            _Items.Clear();
        }

        public bool Contains(ScanningConfig item)
        {
            return _Items.Contains(item);
        }

        public void CopyTo(ScanningConfig[] array, int arrayIndex)
        {
            if ((array == null) || (array.Length == 0)) return;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                _Items.Add(array[i]);
            }
        }

        public bool Remove(ScanningConfig item)
        {
            return _Items.Remove(item);
        }

        public IEnumerator<ScanningConfig> GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        #endregion Operations
    }

    public class ScanningConfig : IMvxPluginConfiguration
    {
        #region Constants

        public const int MWB_CODE_MASK_NONE = 0x00000000;
        public const int MWB_CODE_MASK_QR = 0x00000001;
        public const int MWB_CODE_MASK_DM = 0x00000002;
        public const int MWB_CODE_MASK_RSS = 0x00000004;
        public const int MWB_CODE_MASK_39 = 0x00000008;
        public const int MWB_CODE_MASK_EANUPC = 0x00000010;
        public const int MWB_CODE_MASK_128 = 0x00000020;
        public const int MWB_CODE_MASK_PDF = 0x00000040;
        public const int MWB_CODE_MASK_AZTEC = 0x00000080;
        public const int MWB_CODE_MASK_25 = 0x00000100;
        public const int MWB_CODE_MASK_93 = 0x00000200;
        public const int MWB_CODE_MASK_CODABAR = 0x00000400;
        public const int MWB_CODE_MASK_DOTCODE = 0x00000800;
        public const int MWB_CODE_MASK_11 = 0x00001000;
        public const int MWB_CODE_MASK_MSI = 0x00002000;
        public const int MWB_CODE_MASK_ALL = 0x0fffffff;

        #endregion Constants

        #region Fields

        public int Code { get; set; }

        public string UserName { get; set; }

        public string Key { get; set; }

        public int MaxThreads { get; set; }

        #endregion Fields
    }
}
