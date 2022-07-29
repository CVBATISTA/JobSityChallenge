using FluentValidation.Results;
using JobSityNETChallenge.Application.Interfaces;
using JobSityNETChallenge.Application.Services;
using JobSityNETChallenge.Domain.Commands;
using JobSityNETChallenge.Domain.Core.Events;
using JobSityNETChallenge.Domain.Events.ChatMessage;
using JobSityNETChallenge.Domain.Events.ChatRoom;
using JobSityNETChallenge.Domain.Interfaces;
using JobSityNETChallenge.Infra.CrossCutting.Bus;
using JobSityNETChallenge.Infra.Data.Context;
using JobSityNETChallenge.Infra.Data.EventSourcing;
using JobSityNETChallenge.Infra.Data.Repository;
using JobSityNETChallenge.Infra.Data.Repository.EventSourcing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace JobSityNETChallenge.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IStockService, StockService>();
            services.AddHttpClient<StockService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<ChatRoomCreatedEvent>, ChatRoomEventHandler>();
            services.AddScoped<INotificationHandler<ChatMessageCreatedEvent>, ChatMessageEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateChatRoomCommand, ValidationResult>, ChatRoomCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateChatRoomCommand, ValidationResult>, ChatRoomCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveChatRoomCommand, ValidationResult>, ChatRoomCommandHandler>();

            // Infra - Data
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<JobSityNETChallengeContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }
    }
}