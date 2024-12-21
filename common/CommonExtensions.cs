﻿using BytexDigital.Blazor.Components.CookieConsent;
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


        public static void AddCommonCookieConsent(this IServiceCollection services)
        {
            services.AddCookieConsent(o =>
            {
                o.Revision = 1;
                o.PolicyUrl = "/cookie-policy";

                // Call optional
                o.UseDefaultConsentPrompt(prompt =>
                {
                    prompt.Position = ConsentModalPosition.BottomRight;
                    prompt.Layout = ConsentModalLayout.Bar;
                    prompt.SecondaryActionOpensSettings = false;
                    prompt.AcceptAllButtonDisplaysFirst = false;
                });

                o.Categories.Add(new CookieCategory
                {
                    TitleText = new()
                    {
                        ["en"] = "Google Services",
                        ["de"] = "Google Dienste"
                    },
                    DescriptionText = new()
                    {
                        ["en"] = "Allows the integration and usage of Google services.",
                        ["de"] = "Erlaubt die Verwendung von Google Diensten."
                    },
                    Identifier = "google",
                    IsPreselected = true,

                    Services = new()
            {
                new CookieCategoryService
                {
                    Identifier = "google-maps",
                    PolicyUrl = "https://policies.google.com/privacy",
                    TitleText = new()
                    {
                        ["en"] = "Google Maps",
                        ["de"] = "Google Maps"
                    },
                    ShowPolicyText = new()
                    {
                        ["en"] = "Display policies",
                        ["de"] = "Richtlinien anzeigen"
                    }
                },
                new CookieCategoryService
                {
                    Identifier = "google-analytics",
                    PolicyUrl = "https://policies.google.com/privacy",
                    TitleText = new()
                    {
                        ["en"] = "Google Analytics",
                        ["de"] = "Google Analytics"
                    },
                    ShowPolicyText = new()
                    {
                        ["en"] = "Display policies",
                        ["de"] = "Richtlinien anzeigen"
                    }
                }
            }
                });
            });
        }
    }
}
