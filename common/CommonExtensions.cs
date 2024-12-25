using BytexDigital.Blazor.Components.CookieConsent;
using Microsoft.Extensions.DependencyInjection;

namespace common
{
    public static class CommonExtensions
    {
        public static string ToUpperFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToUpper(input[0]) + input.Substring(1);
        }


        public static void AddCommonCookieConsent(this IServiceCollection services, string policyUrl)
        {
            services.AddCookieConsent(o =>
            {
                o.Revision = 1;
                o.PolicyUrl = policyUrl;

                // Call optional
                o.UseDefaultConsentPrompt(prompt =>
                {
                    prompt.Position = ConsentModalPosition.BottomRight;
                    prompt.Layout = ConsentModalLayout.Bar;
                    prompt.SecondaryActionOpensSettings = false;
                    prompt.AcceptAllButtonDisplaysFirst = false;
                });

                
            });
        }
    }
}
