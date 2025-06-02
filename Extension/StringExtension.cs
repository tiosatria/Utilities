namespace Utilities.Extension
{
    public static class StringExtension
    {

        public enum MaskingMode
        {
            First, Last
        }

        /// <summary>
        /// Mask a string with a specified character, leaving the last few characters visible.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maskChar"></param>
        /// <param name="visibleDigit"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string StringMask(this string str, char maskChar = '*', int visibleDigit = 5, MaskingMode mode = MaskingMode.Last)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length <= visibleDigit)
                return str;
            var maskedPart = new string(maskChar, str.Length - visibleDigit);
            var visiblePart = str.Substring(str.Length - visibleDigit);
            return mode switch
            {
                MaskingMode.First => maskedPart + visiblePart,
                MaskingMode.Last => visiblePart + maskedPart,
                _ => str
            };
        }

    }
}
