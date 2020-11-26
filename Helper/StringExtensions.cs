using System;
using System.Text;

namespace Protaru.Helper
{
    // https://www.mikesdotnetting.com/article/137/displaying-the-first-n-characters-of-text
    public static class StringExtensions
    {
        /// <summary>
        /// Returns part of a string up to the specified number of characters, while maintaining full words
        /// </summary>
        /// <param name="s">String to chop</param>
        /// <param name="length">Maximum characters to be returned</param>
        /// <returns>String</returns>
        public static string Chop(this string s, int length)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            string[] words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words[0].Length > length)
            {
                return words[0];
            }

            StringBuilder builder = new StringBuilder();

            foreach (string word in words)
            {
                if ((builder + word).Length > length)
                {
                    return string.Format("{0}...", builder.ToString().TrimEnd(' '));
                }

                builder.Append(word + " ");
            }

            return string.Format("{0}...", builder.ToString().TrimEnd(' '));
        }
    }
}
