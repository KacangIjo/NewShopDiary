using PushNotificationServer.Models.ViewModels;
using PushNotificationServer.Services;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PushNotificationServer.NotificationScheduler
{
    public class JobScheduler
    {
        public static async Task StartAsync()
        {
            //InventoryDataService invDataService = new InventoryDataService();
            //List<InventoryViewModel> UserList= new List<InventoryViewModel>();
            //UserList = await invDataService.GetAll();
            
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<SentPushNotification>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(1)
                  )
                .Build();
                await sched.ScheduleJob(job, trigger);

        }
    }
}