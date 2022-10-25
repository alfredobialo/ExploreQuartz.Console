// See https://aka.ms/new-console-template for more information

using ExploreQuartz.Lib;

Console.WriteLine("Hello, World!");

SimpleScheduler scheduler =  new SimpleScheduler();
var sch = await scheduler.CreateSimpleScheduler();
await sch.Start(CancellationToken.None);
