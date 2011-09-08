using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Autofac;
using Logos.Infrastructure;
using Logos.ApplicationServices.CommandHandlers;
using Logos.ApplicationServices.Commands;
using Logos.Domain.Core;
using Logos.Infrastructure.Persistence;
using Logos.UI.Views;
using Logos.UI.ViewModels;
using Logos.Domain.Events;
using Logos.ReadModel.EventHandlers;
using Logos.ReadModel;
using Autofac.Core;
using Logos.UI.Commands;
using Logos.Infrastructure.Common;
using Raven.Client;
using Raven.Client.Document;

namespace Logos.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (IContainer iocContainer = InitializeContainer())
            {
                MainWindow mainWindow = new MainWindow();


                IEventStore storage = iocContainer.Resolve<IEventStore>();

                storage.ReplayAllEvents();

                mainWindow.DataContext = iocContainer.Resolve<MainWindowViewModel>();


                mainWindow.ShowDialog();
            }
        }

        IContainer InitializeContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(c =>
                {
                    var bus = new InMemoryCommandMessageBus();

                    bus.RegisterHandler<ImportRepository>(c.Resolve<ImportRepositoryCommandHandler>().Handle);
                    bus.RegisterHandler<TagSourcefile>(c.Resolve<TagSourcefileCommandHandler>().Handle);

                    return bus;
                })
                .As<ICommandSender>()
                .SingleInstance();

            builder
                .Register(c =>
                    {
                        var bus = new InMemoryEventBus();

                        bus.RegisterHandler<RepositoryImported>(c.Resolve<RepositoryImportedEventHandler>().Handle);
                        bus.RegisterHandler<SourcefileImported>(c.Resolve<SourcefileImportedEventHandler>().Handle);
                        bus.RegisterHandler<SourcefileTagged>(c.Resolve<SourcefileTaggedEventHandler>().Handle);

                        return bus;
                    })
                .As<IEventPublisher>()
                .SingleInstance();
            builder.RegisterType<ImportRepositoryCommandHandler>().SingleInstance();
            builder.RegisterType<TagSourcefileCommandHandler>().SingleInstance();

            builder.RegisterType<QueryDatabase>().As<IQueryDatabase>().SingleInstance();
            builder.RegisterType<GithubReadModel>().As<IGithubReadModel>().SingleInstance();

            builder.RegisterType<RepositoryImportedEventHandler>();
            builder.RegisterType<SourcefileImportedEventHandler>();
            builder.RegisterType<SourcefileTaggedEventHandler>();
            builder.RegisterType<MessengerAdapter>().As<IMessengerAdapter>().SingleInstance();

            builder.Register<Func<RepositoryListViewModel>>(c =>
                {
                    IComponentContext cc = c.Resolve<IComponentContext>();
                    return () => cc.Resolve<RepositoryListViewModel>();
                });

            builder.Register<Func<RepositoryListDto, RepositoryViewModel>>(c =>
            {
                IComponentContext cc = c.Resolve<IComponentContext>();
                return repository => cc.Resolve<RepositoryViewModel>(new NamedParameter("repository", repository));
            });

            builder.Register<Func<SourcefileDto, SourcefileViewModel>>(c =>
            {
                IComponentContext cc = c.Resolve<IComponentContext>();
                return sourcefile => cc.Resolve<SourcefileViewModel>(new NamedParameter("sourcefile", sourcefile));
            });

            builder.RegisterType<ImportRepositoryCommand>();

            builder
                .RegisterType<GithubRepositoryRepository>()
                .As<IGithubRepositoryRepository>()
                .SingleInstance();

            builder.RegisterType<RavenDbEventStore>().As<IEventStore>().SingleInstance();

            builder.RegisterType<MainWindowViewModel>();
            builder.RegisterType<RepositoryListViewModel>();
            builder.RegisterType<RepositoryViewModel>();
            builder.RegisterType<SourcefileViewModel>();

            builder.RegisterInstance<IDocumentStore>(CreateRavenDbDocumentStore()).SingleInstance();


            return builder.Build();
        }

        IDocumentStore CreateRavenDbDocumentStore()
        {
            IDocumentStore newDocumentStore = new DocumentStore()
                     {
                         Url = "http://localhost:8080"
                     };

            newDocumentStore.Initialize();

            return newDocumentStore;
        }
    }
}
