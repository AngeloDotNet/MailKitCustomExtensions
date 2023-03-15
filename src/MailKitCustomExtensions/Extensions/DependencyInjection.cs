﻿namespace MailKitCustomExtensions.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMailKitEmailSenderService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IEmailSender, MailKitEmailSender>()
            .AddSingleton<IEmailClient, MailKitEmailSender>();

        services
            .AddSerilogServices();

        services
            .Configure<SmtpOptions>(configuration.GetSection("Smtp"));

        return services;
    }
}