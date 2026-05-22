using Microsoft.AspNetCore.Authentication;

namespace Demo.Api;

public static class ConfigureServices
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBasicAuthentication()
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(BasicAuthenticationHandler.SchemeName, policy =>
                {
                    policy.AddAuthenticationSchemes(BasicAuthenticationHandler.SchemeName);
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
                    BasicAuthenticationHandler.SchemeName, 
                    null);

            return services;
        }
    }
}
