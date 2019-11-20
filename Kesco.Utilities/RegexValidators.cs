using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Kesco.Utilities
{

	public static class RegexValidators
	{

	   public static bool ValidateEmail(string email)
	   {
		   bool invalid = false;
		   if (String.IsNullOrEmpty(email))
			  return false;

		   // Use IdnMapping class to convert Unicode domain names.
		   email = Regex.Replace(email, @"(@)(.+)$", (match) => {
			   // IdnMapping class with default property values.
			   IdnMapping idn = new IdnMapping();

			   string domainName = match.Groups[2].Value;
			   try {
				   domainName = idn.GetAscii(domainName);
			   } catch (ArgumentException) {
				   invalid = true;
			   }
			   return match.Groups[1].Value + domainName;
		   });

		   if (invalid)
			   return false;

		   // Return true if strIn is in valid e-mail format.
		   return Regex.IsMatch(email,
				  @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
				  RegexOptions.IgnoreCase);
	   }

	   public static bool ValidateUrl(string url)
	   {

		   if (String.IsNullOrEmpty(url))
			   return false;

		   return Regex.IsMatch(url,
				  @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$",
				  RegexOptions.IgnoreCase);
	   }

	}
}
