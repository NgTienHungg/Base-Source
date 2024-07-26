using System;
using System.Globalization;
using Sirenix.OdinInspector;

namespace ViewPager.BoatPacking
{
    [Serializable]
    public class SerializableDateTime
    {
        public const string FORMAT = "dd/MM/yyyy HH:mm:ss";

        [HideLabel]
        public string dateTimeString;

        public SerializableDateTime()
        {
            dateTimeString = DateTime.MinValue.ToString(FORMAT);
        }

        public SerializableDateTime(DateTime dateTime)
        {
            dateTimeString = dateTime.ToString(FORMAT);
        }

        public DateTime ToDateTime()
        {
            try
            {
                return DateTime.ParseExact(dateTimeString, FORMAT, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                // Common.LogError($"Error parsing DateTime: {dateTimeString} - {e.Message}");
                return DateTime.MinValue;
            }
        }

        public override string ToString()
        {
            return dateTimeString;
        }
    }
}