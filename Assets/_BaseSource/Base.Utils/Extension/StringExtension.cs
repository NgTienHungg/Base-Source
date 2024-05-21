public static class StringExtension
{
    public static string Color(this string content, string color) {
        return $"<color={color}>{content}</color>";
    }
    
    public static string Format(this string content, params object[] args) {
        return string.Format(content, args);
    }
}