using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services method to manage the application language
    /// </summary>
    public class LanguageService : ILanguageService
    {
        /// <summary>
        /// Set the UI language
        /// </summary>
        /// <param name="context">The HttpContext to change the language for</param>
        /// <param name="language">The language to show on the UI</param>
        public void ChangeUiLanguage(HttpContext context, string language)
        {
            string culture = SetCulture(language);
            UpdateCultureCookie(context, culture);
        }

        /// <summary>
        /// Set the culture
        /// </summary>
        /// <param name="language">The language to set the culture for</param>
        /// <returns>The culture representing the request language or default ("en")</returns>
        public string SetCulture(string language)
        {
            // Default language is "en", french is "fr", and spanish is "es".
            return (language == "French")
                ? "fr"
                : (language == "Spanish")
                ? "es"
                : "en";
        }

        /// <summary>
        /// Update the culture cookie
        /// </summary>
        /// <param name="context">The HttpContext to write the cookie to</param>
        /// <param name="culture">The culture to write to the cookie</param>
        public void UpdateCultureCookie(HttpContext context, string culture)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
        }
    }
}