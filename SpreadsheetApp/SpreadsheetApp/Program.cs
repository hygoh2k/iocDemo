using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Common.Model;
using Spreadsheet.Model;
using Spreadsheet.Model.Spreadsheet.Model;

namespace SpreadsheetApp
{
    class Program
    {
        /// <summary>
        /// IoC container 
        /// </summary>
        private static Castle.Windsor.WindsorContainer _container;

        /// <summary>
        /// Registers the commands that derived from ICommand and their dependencies
        /// into IoC container.
        /// </summary>
        /// todo: move to configuration file
        static void RegisterComponents()
        {
            //register CreateSpreadsheetCommand with the key "C"
            _container.Register(Component.For<SpreadsheetCommand>()
                .ImplementedBy<CreateSpreadsheetCommand>()
                .DependsOn(Dependency.OnValue("cmdKey", "C"))
            );

            //register UpdateSpreadsheetCommand with key "N"
            _container.Register(Component.For<SpreadsheetCommand>()
                .ImplementedBy<UpdateSpreadsheetCommand>()
                .DependsOn(Dependency.OnValue("cmdKey", "N"))

            );

            //register SumSpreadsheetCommand with key "S"
            _container.Register(Component.For<SpreadsheetCommand>()
                .ImplementedBy<SumSpreadsheetCommand>()
                .DependsOn(Dependency.OnValue("cmdKey", "S"))
            );

            //register the SpreadsheetCmdCollection that hold the Commands Instances
            //Command Instances will be injected into Collection by IoC Container
            _container.Register(Component.For<SpreadsheetCmdCollection>());

            //register the SpreadsheetPrinter that display spreadsheet in text format
            _container.Register(Component.For<ISheetPrinter>()
                .ImplementedBy<SpreadsheetPrinter>());


        }

        /// <summary>
        /// setting up the IoC container, which is required to resolve the component dependencies
        /// </summary>
        static void SetupContainer()
        {
            _container = new WindsorContainer();
            //Allow windsor to resolve constructor that has an ICollection as parameter
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel));
        }

        /// <summary>
        /// main entry point of the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //setup Dependency Container
            SetupContainer();
            //register Dependencies
            RegisterComponents();

            int exitCode = 0x0;
            try
            {
                var cmdCollection = _container.Resolve<SpreadsheetCmdCollection>();
                var sheetPrinter = _container.Resolve<ISheetPrinter>();

                //variable that hold the current sheet
                ISheet currentSheet = null;
                while (true)
                {
                    Console.Write("enter command:");

                    //extract a series of parameters in user input with a space separator
                    //format of the command line: <command key> <param1> <param2> <param3> ...
                    //e.g. N 5 6
                    string[] input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    //command key is the first parameter
                    string cmdKey = input.Length > 0 ? input[0] : "";

                    if (cmdKey.Equals("Q"))
                    {
                        //break the loop amd end this application
                        break;
                    }

                    //the subsequent parameters will be used as command arguments
                    string[] cmdArgs = input.Length > 1 ? input.Skip(1).ToArray() : new string[] { };

                    //start matching the command key with the registered commands 
                    if (cmdCollection.CommandCollection.ContainsKey(cmdKey))
                    {
                        var cmd = cmdCollection.CommandCollection[cmdKey];
                        var result = cmd.Execute(currentSheet, cmdArgs);
                        if (result.Success)
                        {
                            currentSheet = result.Spreadsheet;
                            sheetPrinter.PrintContent(currentSheet, Console.Out);

                        }
                        else
                        {
                            Console.WriteLine(result.ErrorMessage);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Command");
                    }

                }
            }
            catch (Exception ex)
            {
                //unexpected error goes to here
                exitCode = ex.HResult;
            }
            finally
            {
                //set exit code
                Environment.ExitCode = exitCode;
                //dispose dependencies
                _container.Dispose();
            }
        }
    }
}
