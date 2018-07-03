using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayBook.JobsScheduler
{
    public class Scheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<AccountRemover>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                           
                .WithSimpleSchedule(x => x            
                    .WithIntervalInMinutes(1)          
                    .RepeatForever())                   
                .Build();                                

            await scheduler.ScheduleJob(job, trigger);        
        }
    }
}