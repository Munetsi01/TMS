using Api.Handlers;
using Api.Models.Task;
using Api.Models.User;
using Api.Validators;
using Core.Abstractions;
using Data.Entities;
using Data.Repositories;
using FluentValidation;

namespace Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Data.Entities.Task>, TaskRepository>();
        }

        public static void AddTokenProvider(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider<User>, JwtProvider>();
        }

        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginUserRequest>, LoginUserRequestValidator>();
            services.AddScoped<IHandler<LoginUserRequest, LoginUserResponse>, LoginUserHandler>();

            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>();
            services.AddScoped<IHandler<RegisterUserRequest, RegisterUserResponse>, RegisterUserHandler>();

            services.AddScoped<IValidator<ListUsersRequest>, ListUsersRequestValidator>();
            services.AddScoped<IHandler<ListUsersRequest, ListUsersResponse>, ListUsersHandler>();

            services.AddScoped<IValidator<ListTasksRequest>, ListTasksRequestValidator>();
            services.AddScoped<IHandler<ListTasksRequest, ListTasksResponse>, ListTasksHandler>();
        }
    }
}
