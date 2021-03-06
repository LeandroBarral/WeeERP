﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wee.Common.Contracts;
using Microsoft.Extensions.FileProviders;
using Wee.Common.Reflection;
using Wee.Common;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Wee.UI.Core.Registers
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class RazorViewFileProvidersRegister : BaseFileProvidersRegister, IWeeRegister<IServiceCollection>
    {
        private IServiceCollection _serviceCollection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="folderPath"></param>
        public RazorViewFileProvidersRegister(IServiceCollection serviceCollection, string folderPath)
            : base(folderPath)
        {
            _serviceCollection = serviceCollection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IServiceCollection Invoke<T>()
            where T : class
        {
            var providers = base.LoadProviders<T>();

            if (providers.Count > 0)
            {
                //Add the file provider to the Razor view engine
                _serviceCollection.Configure<RazorViewEngineOptions>(options =>
                {
                    foreach (var provider in providers)
                    {
                        options.FileProviders.Add(provider);
                    }
                });
            }

            return _serviceCollection;
        }
    }
}
