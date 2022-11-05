﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetConfDemo
{
    /// <summary>
    /// This is used to fill the void of not having a window factory or a generic OpenWindow<T>
    /// 
    /// Basically this allows us to use "OpenWindow" from a different thread/dispatcher
    /// and then the page will get retrieved from the scoped context once the handler/services
    /// has been created for the window
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ScopedWindow<T> : Window
        where T : Page
    {
        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);

            if (Page == null && args.NewHandler != null)
            {
                Page = args.NewHandler.MauiContext.Services.GetService<T>();

                // This will set me up as the scoped window returned for the service
                _ = args.NewHandler.MauiContext.Services.GetService<Window>();
            }


        }
    }
}
