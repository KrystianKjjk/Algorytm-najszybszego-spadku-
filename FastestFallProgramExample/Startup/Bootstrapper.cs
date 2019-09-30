using Autofac;
using FastestFallProgramExample.Communication;
using FastestFallProgramExample.View;
using FastestFallProgramExample.ViewModel;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestFallProgramExample.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootsrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<CounterLineWindow>().AsSelf();
            builder.RegisterType<CounterLineWindowViewModel>().AsSelf();

            builder.RegisterType<CounterLineDetailViewModel>().As<ICounterLineDetailViewModel>();
            builder.RegisterType<CounterLineViewModel>().As<ICounterLineViewModel>();

            return builder.Build();
        }
    }
}
