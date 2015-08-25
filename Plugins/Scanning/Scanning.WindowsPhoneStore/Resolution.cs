namespace Scanning.WindowsPhoneStore
{
    public struct Resolution
    {
        #region Properties

        public uint Width { get; set; }

        public uint Height { get; set; }

        public int Id { get; set; }

        public ulong Size
        {
            get
            {
                return Width * Height;
            }
        }

        public string Display
        {
            get
            {
                return string.Format("{0} x {1}", Width, Height);
            }
        }

        public static Resolution Zero
        {
            get
            {
                return new Resolution { Width = 0, Height = 0 };
            }
        }

        #endregion Properties
    }
}
