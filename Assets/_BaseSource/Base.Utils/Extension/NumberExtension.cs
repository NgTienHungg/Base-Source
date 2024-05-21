using System.Globalization;

namespace Base.Utils.Extension
{
    public static class NumberExtension
    {
        public static string ToCurrencyFormat(this int value) {
            return value.ToString("#,0", CultureInfo.InvariantCulture).Replace(',', '.')
                   + " " + RegionInfo.CurrentRegion.ISOCurrencySymbol;
        }
    }
}