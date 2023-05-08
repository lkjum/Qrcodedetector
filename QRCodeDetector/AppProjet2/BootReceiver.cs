using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using System;

namespace AppProjet2
{
    [BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                Intent serviceIntent = new Intent(context, typeof(BootService));
                serviceIntent.SetFlags(ActivityFlags.NewTask);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    context.StartForegroundService(serviceIntent);
                else
                    context.StartService(serviceIntent);
            }
            catch(Exception ex)
            {
                Log.Error("Boot", ex.Message);
            }
        }
    }
}