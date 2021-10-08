using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NATS.POC.Chat.Application.Queries;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using NATS.POC.Chat.Infrastructure.Repositories;
using NATS.POC.Chat.Infrastructure.Services;
using System.Reflection;

namespace NATS.POC.Chat.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection RegisterApplicationModule(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(ApplicationModule)));

            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageQueries, MessageQueries>();
            services.AddTransient<IMessageSenderService, MessageSenderService>();
            services.AddTransient<IMessageProcessorService, MessageProcessorService>();

            return services;
        }
    }
}
