using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Extensions
{
	public static class StringExtension
	{
		public static bool IsNullOrEmpty(this string text)
		{
			return String.IsNullOrEmpty(text);
		}

		public static bool IsNullOrWhiteSpace(this string text)
		{
			return String.IsNullOrWhiteSpace(text);
		}
		public static string ReplaceTagInput(this string text)
		{
			var rep = text.Replace('"', ' ');
			var rep1 = rep.Replace("{ value :", "");
			var rep2 = rep1.Replace("[", "");
			var rep3 = rep2.Replace('}', ' ');
			var result = rep3.Replace(']', ' ');
			return result;
		}
		public static string RemoveDiacritics(this string text)
		{
			return new string(
				text
					.Normalize(NormalizationForm.FormD)
					.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
					.ToArray()
			).Normalize(NormalizationForm.FormC);
		}
	}
}
