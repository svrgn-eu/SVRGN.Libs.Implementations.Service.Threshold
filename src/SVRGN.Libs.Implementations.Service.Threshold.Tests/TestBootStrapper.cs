using Microsoft.Extensions.DependencyInjection;
using SVRGN.Libs.Contracts.Service.Logging;
using SVRGN.Libs.Contracts.Service.Object;
using SVRGN.Libs.Contracts.Service.Threshold;
using SVRGN.Libs.Implementations.DependencyInjection;
using SVRGN.Libs.Implementations.Service.ConsoleLogger;
using SVRGN.Libs.Implementations.Service.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRGN.Libs.Implementations.Service.Threshold.Tests
{
    public static class TestBootStrapper
    {
        //TODO: seperate BootStrapper from Base project and upload into nuget
        #region Properties

        #endregion Properties

        #region Methods

        #region Register
        public static void Register(IServiceCollection services)
        {
            if (!DiContainer.SetServiceCollection(services))
            {
                services = DiContainer.GetServiceCollection();
            }

            services.AddTransient<IThresholdService, ThresholdService>();
            services.AddTransient<ILogService, DebugLogService>();
            services.AddSingleton<IObjectService, ObjectService>();
            TestBootStrapper.RegisterEntities(services);

            DiContainer.SetServiceProvider(services.BuildServiceProvider());
        }
        #endregion Register


        #region RegisterEntities: registers special classes, like AI stuff
        /// <summary>
        /// registers special classes, like AI stuff
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterEntities(IServiceCollection services)
        {
            services.AddTransient<IThreshold, Threshold>();
        }
        #endregion RegisterEntities

        #endregion Methods
    }
}
