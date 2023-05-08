using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Widget;
using System;

namespace AppProjet2
{
    [Service(Enabled = true, Name = "com.companyname.OnBootForegroundService", ForegroundServiceType = Android.Content.PM.ForegroundService.TypeMediaProjection, Permission = "android.permission.FOREGROUND_SERVICE")]
    public class BootService : Service
    {
        private ScreenshotReceiver screenshotReceiver;
        private NotificationManager _notificationManager;
        private NotificationChannel _notificationChannel;
        private Notification.Builder _notificationBuilder;

        [Obsolete]
        public override void OnCreate()
        {
            base.OnCreate();

            try
            {
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
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                Log.Debug("Boot", ex.Message);
            }
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            StartForeground(1, _notificationBuilder.Build(), Android.Content.PM.ForegroundService.TypeMediaProjection);

            var intent__ = new IntentFilter(Intent.ActionMediaScannerScanFile);
            screenshotReceiver = new ScreenshotReceiver();
            RegisterReceiver(screenshotReceiver, intent__);

            var _observer = new ScreenshotObserver(new Handler(), this);
            ContentResolver.RegisterContentObserver(MediaStore.Images.Media.ExternalContentUri, true, _observer);

            Toast.MakeText(this, "QRCode Detector activé", ToastLength.Long).Show();

            return StartCommandResult.NotSticky;
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}