using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class SlugHelper
    {
        public static string ToSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            string normalized = input.ToLowerInvariant();

            string normalizedVietnamese = NormalizeVietnamese(normalized);

            string sanitized = Regex.Replace(normalizedVietnamese, @"[^a-z0-9\s-]", "");

            string slug = Regex.Replace(sanitized, @"\s+", "-").Trim('-');

            return slug;
        }

        private static string NormalizeVietnamese(string input)
        {
            string[] vietnameseChars = new string[]
            {
            "aáàảãạăắằẳẵặâấầẩẫậ",
            "oóòỏõọôốồổỗộơớờởỡợ",
            "eéèẻẽẹêếềểễệ",
            "iíìỉĩị",
            "uúùủũụưứừửữự",
            "yýỳỷỹỵ",
            "dđ"
            };

            string[] latinChars = new string[]
            {
            "a", "o", "e", "i", "u", "y", "d"
            };

            for (int i = 0; i < vietnameseChars.Length; i++)
            {
                foreach (var c in vietnameseChars[i].Substring(1))
                {
                    input = input.Replace(c, vietnameseChars[i][0]);
                }
            }

            return input;
        }
    }
}
