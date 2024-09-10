namespace _01_FrameWork
{
    public static class ToolsString
    {
        public static string ShortenString(this string str, int maxLength)
        {
            if (str.Length > maxLength)
            {
                return str.Substring(0, maxLength - 3) + "..."; 
            }
            return str;
        }
    }
}
