using Quartz;

namespace Geolocation.App.Jobs
{
    public static class SchedulerConfiguratorExtension
    {
        public static void AddJobAndTrigger<T>(
            this IServiceCollectionQuartzConfigurator quartz,
            string cron)
            where T : IJob
        {
            // Use the name of the IJob as the appsettings.json key
            string jobName = typeof(T).Name;

            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(jobName + "-trigger")
                .WithCronSchedule(cron, x => x.WithMisfireHandlingInstructionIgnoreMisfires())); // use the schedule from configuration
        }
    }
}