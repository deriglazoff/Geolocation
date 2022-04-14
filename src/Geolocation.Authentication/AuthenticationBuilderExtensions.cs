using System;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Geolocation.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        public static IServiceCollection AddBasicAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            return services;
        }

    }
}