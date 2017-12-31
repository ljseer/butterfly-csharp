﻿using System;
using Butterfly.OpenTracing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Butterfly.Client.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddButterfly(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddButterfly().Configure<ButterflyOptions>(configuration);
        }

        public static IServiceCollection AddButterfly(this IServiceCollection services, Action<ButterflyOptions> configure)
        {
            return services.AddButterfly().Configure<ButterflyOptions>(configure);
        }

        private static IServiceCollection AddButterfly(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<HttpTracingHandler>();
            services.AddSingleton<ISpanContextFactory, SpanContextFactory>();
            services.AddSingleton<ISampler, FullSampler>();
            services.AddSingleton<ITracer, Tracer>();
            services.AddSingleton<IServiceTracer, ServiceTracer>();
            services.AddSingleton<ISpanRecorder, AsyncSpanRecorder>();     
            services.AddSingleton<IButterflyCollector, ButterflyCollector>();
            services.AddSingleton<IButterflyDispatcher, ButterflyDispatcher>();
            services.AddSingleton<IButterflySender, HttpButterflySender>();
            services.AddSingleton<IHostedService, ButterflyHostedService>();
            return services;
        }
    }
}