using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace App5.UWP.BackgroundTaskHandler
{
    public class BackgroundTasksFactory
    {
        public static BackgroundTaskRegistration RegisterBackgroundTask(string name, IBackgroundTrigger trigger, IBackgroundCondition condition)
        {
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {

                if (cur.Value.Name == name)
                    // The task is already registered.
                    return (BackgroundTaskRegistration)(cur.Value);
            }

            //Register new background task:
            var builder = new BackgroundTaskBuilder();

            builder.Name = name;
            //builder.TaskEntryPoint = taskEntryPoint;
            //SetTrigger()
            
            builder.SetTrigger(trigger);
            builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));

            if (condition != null)
            {
                builder.AddCondition(condition);
            }

            BackgroundTaskRegistration task = builder.Register();

            return task;


        }
    }
}
