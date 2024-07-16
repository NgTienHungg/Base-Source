using System;
using System.Globalization;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SerializableDateTime
    {
        public const string FORMAT = "dd/MM/yyyy HH:mm:ss";

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
                Debug.LogError($"Error parsing DateTime: {dateTimeString} - {e.Message}");
                return DateTime.MinValue;
            }
        }

        public override string ToString()
        {
            return dateTimeString;
        }
    }
}