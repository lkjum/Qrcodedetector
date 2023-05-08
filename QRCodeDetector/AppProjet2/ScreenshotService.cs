using Android.App;
using Android.Content;
using Android.OS;

namespace AppProjet2
{
    [Service(Enabled = true)]
    public class ScreenshotService : Service
    {
        private ScreenshotServiceBinder _binder;

        public override IBinder OnBind(Intent intent)
        {
            _binder = new ScreenshotServiceBinder(this);

            Intent foregroundServiceIntent = new Intent(this, typeof(ScreenshotForegroundService));
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                StartForegroundService(foregroundServiceIntent);
            else
                StartService(foregroundServiceIntent);

            return _binder;
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.NotSticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
