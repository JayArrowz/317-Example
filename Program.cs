using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetScape.Core;
using NetScape.Modules.Messages;
using NetScape.Modules.Messages.Models;
using NetScape.Modules.ThreeOneSeven.Game;
using NetScape.Modules.ThreeOneSeven.LoginProtocol;
using NetScape.Modules.ThreeOneSeven.World.Updating;
using System;
using System.Collections.Generic;
namespace ANewServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Module> modules = new()
            {
                new ThreeOneSevenGameModule(),
                new MessagesModule(
                    typeof(ThreeOneSevenEncoderMessages.Types),
                    typeof(ThreeOneSevenDecoderMessages.Types)
                ),
                new ThreeOneSevenLoginModule(),
                new ThreeOneSevenUpdatingModule()
            };
            ServerHandler.RunServer<MyPlayer>("appsettings.json", BuildDbOptions, modules);
            Console.ReadLine();
        }

        private static void BuildDbOptions(DbContextOptionsBuilder optionsBuilder, IConfigurationRoot configurationRoot)
        {
            optionsBuilder.UseNpgsql(configurationRoot.GetConnectionString("NetScape"),
                x => x.MigrationsAssembly(typeof(Program)
                    .Assembly.FullName));
        }
    }
}
