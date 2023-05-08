using Android.App;
using Android.Content;
using Android.OS;
using System.Collections.Generic;

namespace AppProjet2
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class NotificationButtonReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.Action == "KILL_FOREGROUND_TASK")
            {
                var activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);
                var runningProcesses = activityManager.GetRunningServices(int.MaxValue);

                foreach (var process in runningProcesses)
                {
                    if (process.Process == "com.companyname.appprojet2")
                    {
                        activityManager.KillBackgroundProcesses(process.Process);
                        break;
                    }
                }

                ActivityManager am = (ActivityManager)context.GetSystemService(Context.ActivityService);
                am.KillBackgroundProcesses(context.PackageName);

                NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
                notificationManager.CancelAll();

                IList<ActivityManager.RunningServiceInfo> services = am.GetRunningServices(int.MaxValue);

                foreach (ActivityManager.RunningServiceInfo service in services)
                {
                    Intent intent_ = new Intent().SetComponent(service.Service);
                    context.StopService(intent_);
                }

                Process.KillProcess(Process.MyPid());
            }
        }
    }
}