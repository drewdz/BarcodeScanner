namespace Scanning.Entities
{
    public class ScanItem
    {
        #region Properties

        public int Id { get; set; }

        public string Text { get; set; }

        public bool Selected { get; set; }

        private static int _Count = 0;
        public static int Counter
        {
            get
            {
                _Count++;
                if (_Count == int.MaxValue) _Count = 0;
                return _Count;
            }
        }

        #endregion Properties

        #region Helpers

        public static void ResetCount(int value = 0)
        {
            _Count = value;
        }

        #endregion Helpers
    }
}
