﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheLoyaltyProgramUnitTests
{
    public class FixedControllerProvider : ControllerFeatureProvider
    {
        private readonly Type[] controllerTypes;

        public FixedControllerProvider(params Type[] controllerTypes)
        {
            this.controllerTypes = controllerTypes;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            return this.controllerTypes.Contains(typeInfo);
        }
    }

    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddControllersByType(this IServiceCollection services, params Type[] controllerTypes)
        {
            return services
            .AddControllers()
            .ConfigureApplicationPartManager(mgr =>
            {
                mgr.FeatureProviders.Remove(mgr.FeatureProviders.First(f => f is ControllerFeatureProvider));
                mgr.FeatureProviders.Add(new FixedControllerProvider(controllerTypes));
            });
        }
    }
}