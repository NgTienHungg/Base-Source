using System.Collections.Generic;
using System.Reflection;

public class ValueDropdown<T>
{
    public static IEnumerable<string> GetAllStrings()
    {
        var soundNames = new List<string>();
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        foreach (var field in fields)
        {
            if (field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
            {
                soundNames.Add((string)field.GetValue(null));
            }
        }

        return soundNames;
    }
}