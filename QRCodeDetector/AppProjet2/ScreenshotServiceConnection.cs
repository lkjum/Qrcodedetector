using Android.Content;
using Android.OS;

namespace AppProjet2
{
    public class ScreenshotServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public ScreenshotServiceBinder Binder { get; set; }


        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as ScreenshotServiceBinder;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Binder = null;
        }
    }
}