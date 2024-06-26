﻿using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Totvs.Ats.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssembly(
                Assembly.GetExecutingAssembly(),
                includeInternalTypes: true);
        });

        return services;
    }
}