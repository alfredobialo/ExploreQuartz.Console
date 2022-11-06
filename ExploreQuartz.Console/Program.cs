// See https://aka.ms/new-console-template for more information

using System.Configuration;
using ExploreQuartz.Lib;
using ExploreQuartz.Lib.Jobs;
using Quartz;

Console.WriteLine("Hello, World!");

SimpleScheduler scheduler =  new SimpleScheduler();
var sch = await scheduler.CreateSimpleScheduler();
await sch.Start(CancellationToken.None);

    // defined a JobDetail : Required so we could inform our custom Job Processor about what to expect
IJobDetail jobDetail = JobBuilder.Create<HelloWorldJob>()
    .WithDescription("Create Recurring Expense Job")
    .WithIdentity("expense-0001", "expenses")
    .UsingJobData("channel", "org-hq")
    .UsingJobData("expenseId", "expense-0001")
    .UsingJobData("createdBy", "alfredobialo")
    .UsingJobData("expenseTotal", 512_999.00)
    .Build();
    
// How do we Trigger the Job?

ITrigger trigger = TriggerBuilder.Create()
    .ForJob(jobDetail)
    .StartNow()
    .WithSimpleSchedule( x => 
        x.WithInterval(TimeSpan.FromSeconds(5))
        .RepeatForever()
    )
    .WithIdentity("expense-trigger","expenses")
    .WithDescription("Trigger all Recurring Expenses")
    // override expense Id with Trigger JobDataMap
    .UsingJobData("expenseId", "trigger-expense-0002")
    .Build();


await sch.ScheduleJob(jobDetail, trigger);

await Task.Delay(TimeSpan.FromSeconds(20));
await sch.Shutdown();
