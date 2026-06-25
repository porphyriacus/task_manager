using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.DTOs.TaskDTO;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Services;
using TaskManager.Core.Validators;

namespace TaskManager.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<TaskCreateDto>, TaskCreateDtoValidator>()
                    .AddScoped<IValidator<TaskUpdateDto>, TaskUpdateDtoValidator>()
                    .AddScoped<IValidator<TaskFilteredDto>, TaskFilteredDtoValidator>();

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
