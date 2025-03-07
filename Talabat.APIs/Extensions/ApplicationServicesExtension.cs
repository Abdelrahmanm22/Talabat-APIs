﻿using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)   //// el caller hya hya el parameter ely hytb3tlk
        {
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //make it generic too
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            #region validation error
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState
                                                .Where(p => p.Value.Errors.Count() > 0)
                                                .SelectMany(p => p.Value.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            #endregion

            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService,OrderService>();
            return Services;
        }
    }
}
