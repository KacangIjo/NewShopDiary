using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShopDiaryApp.API.NotificationScheduler
{
    public class JobScheduler
    {
        public static async Task StartAsync()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<SentPushNotification>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(1, 50))
                  )
                .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}