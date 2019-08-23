using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;


namespace DITest
{
    class Program
    {
        static void Main(string[] args)
        {
            ////setup our DI
            //var serviceProvider = new ServiceCollection()
            //    .AddLogging()
            //    .AddSingleton<IFooService, FooService>()
            //    .AddSingleton<IBarService, BarService>()
            //    .BuildServiceProvider();

            ////configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);

            //var logger = serviceProvider.GetService<ILoggerFactory>()
            //    .CreateLogger<Program>();
            //logger.LogDebug("Starting application");

            ////do the actual work here
            //var bar = serviceProvider.GetService<IBarService>();
            //bar.DoSomeRealWork();

            //logger.LogDebug("All done!");

            // 注册三次, 前两次会被第三次的注册覆盖掉
            var services = new ServiceCollection();
            // 默认构造
            services.AddSingleton<IOperationSingleton, Operation>();
            // 自定义传入Guid空值
            services.AddSingleton<IOperationSingleton>(
              new Operation(Guid.Empty));
            // 自定义传入一个New的Guid
            services.AddSingleton<IOperationSingleton>(
              new Operation(Guid.NewGuid()));

            var provider = services.BuildServiceProvider();

            // 输出singletone1的Guid
            var singletone1 = provider.GetService<IOperationSingleton>();
            Console.WriteLine($"signletone1: {singletone1.OperationId}");

            // 输出singletone2的Guid
            var singletone2 = provider.GetService<IOperationSingleton>();
            Console.WriteLine($"signletone2: {singletone2.OperationId}");
            Console.WriteLine($"singletone1 == singletone2 ? : { singletone1 == singletone2 }");

            // 实例生命周期之Scoped
            var service2 = new ServiceCollection()
                .AddScoped<IOperationScoped, Operation>()
                .AddTransient<IOperationTransient, Operation>()
                .AddSingleton<IOperationSingleton, Operation>();

            var provice2 = service2.BuildServiceProvider();
            using(var scope1 = provice2.CreateScope())
            {
                var p = scope1.ServiceProvider;

                var scopeobj1 = p.GetService<IOperationScoped>();
                var transient1 = p.GetService<IOperationTransient>();
                var singleton1 = p.GetService<IOperationSingleton>();

                var scopeobj2 = p.GetService<IOperationScoped>();
                var transient2 = p.GetService<IOperationTransient>();
                var singleton2 = p.GetService<IOperationSingleton>();

                Console.WriteLine(
                    $"scope1: { scopeobj1.OperationId }," +
                    $"transient1: {transient1.OperationId}, " +
                    $"singleton1: {singleton1.OperationId}");

                Console.WriteLine($"scope2: { scopeobj2.OperationId }," +
                    $"transient2: {transient2.OperationId}, " +
                    $"singleton2: {singleton2.OperationId}");
            }
            using (var scope1 = provice2.CreateScope())
            {
                var p = scope1.ServiceProvider;

                var scopeobj1 = p.GetService<IOperationScoped>();
                var transient1 = p.GetService<IOperationTransient>();
                var singleton1 = p.GetService<IOperationSingleton>();

                var scopeobj2 = p.GetService<IOperationScoped>();
                var transient2 = p.GetService<IOperationTransient>();
                var singleton2 = p.GetService<IOperationSingleton>();

                Console.WriteLine(
                    $"scope1: { scopeobj1.OperationId }," +
                    $"transient1: {transient1.OperationId}, " +
                    $"singleton1: {singleton1.OperationId}");

                Console.WriteLine($"scope2: { scopeobj2.OperationId }," +
                    $"transient2: {transient2.OperationId}, " +
                    $"singleton2: {singleton2.OperationId}");
            }

        }
    }

    public interface IFooService
    {
        void DoThing(int number);
    }

    public interface IBarService
    {
        void DoSomeRealWork();
    }

    public class BarService: IBarService
    {
        private readonly IFooService _fooService;
        public BarService(IFooService fooService)
        {
            _fooService = fooService;
        }
        public void DoSomeRealWork()
        {
            for (int i = 0; i < 10; i++)
            {
                _fooService.DoThing(i);
            }
        }
    }

    public class FooService : IFooService
    {
        private readonly ILogger<FooService> _logger;
        public FooService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FooService>();
        }
        public void DoThing(int number)
        {
            _logger.LogInformation($"Doing the thing {number}");
        }
    }


    //================================================
    public interface IOperation
    {
        Guid OperationId { get; }
    }
    public interface IOperationSingleton : IOperation { }
    public interface IOperationTransient : IOperation { }
    public interface IOperationScoped : IOperation { }

    public class Operation:
        IOperationSingleton,
        IOperationTransient,
        IOperationScoped
    {
        private Guid _guid;

        public Operation()
        {
            _guid = Guid.NewGuid();
        }
        public Operation(Guid guid)
        {
            _guid = guid;
        }
        public Guid OperationId => _guid;

    }
}
