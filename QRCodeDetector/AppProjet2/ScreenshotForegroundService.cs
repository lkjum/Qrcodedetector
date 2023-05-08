using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace AppProjet2
{
    [Service(Enabled = true, Name = "com.companyname.ScreenshotForegroundService", ForegroundServiceType = Android.Content.PM.ForegroundService.TypeMediaProjection, Permission = "android.permission.FOREGROUND_SERVICE")]
    public class ScreenshotForegroundService : Service
    {
        private NotificationChannel _notificationChannel;
        private NotificationManager _notificationManager;
        private Notification.Builder _notificationBuilder;

        [Obsolete]
        public override void OnCreate()
        {
            base.OnCreate();

            _notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var channelId = "screenshot_channel";
            var channelName = "Screenshot Channel";
            var importance = NotificationImportance.Default;
            _notificationChannel = new NotificationChannel(channelId, channelName, importance);
            _notificationManager.CreateNotificationChannel(_notificationChannel);

            var broadcastIntent = new Intent(this, typeof(NotificationButtonReceiver));
            broadcastIntent.PutExtra("button_click", true);
            broadcastIntent.SetAction("KILL_FOREGROUND_TASK");
            var broadcastPendingIntent = PendingIntent.GetBroadcast(this, 0, broadcastIntent, PendingIntentFlags.UpdateCurrent);


            _notificationBuilder = new Notification.Builder(this, channelId)
                .SetContentTitle("QRCode Detector")
                .SetContentText("QRCode Detector est en cours de fonctionnement")
                .SetSmallIcon(Resource.Drawable.ic_mtrl_chip_checked_black)
                .AddAction(Resource.Drawable.ic_mtrl_chip_close_circle, "Quitter", broadcastPendingIntent);
 
            _notificationBuilder.Build();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            StartForeground(1, _notificationBuilder.Build(), Android.Content.PM.ForegroundService.TypeMediaProjection);
            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            StopForeground(true);
            StopSelf();
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}