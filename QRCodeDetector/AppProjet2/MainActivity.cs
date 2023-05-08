using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Android.Content;
using Android.Provider;
using Android.Widget;
using Android.Content.PM;
using AndroidX.Core.App;
using Android;

namespace AppProjet2
{
    [Activity(Label = "QRCode Detector", Theme = "@style/TransparentTheme", MainLauncher = true, NoHistory = true, ClearTaskOnLaunch = true)]
    public class MainActivity : AppCompatActivity
    {
        private ScreenshotServiceConnection _serviceConnection;
        private ScreenshotReceiver screenshotReceiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (!IsTaskAlreadyRunning())
            {
                Intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);

                _serviceConnection = new ScreenshotServiceConnection();
                var intent = new Intent(this, typeof(ScreenshotService));
                BindService(intent, _serviceConnection, Bind.AutoCreate);

                if (CheckSelfPermission(Android.Manifest.Permission.ReadExternalStorage) != Permission.Granted)
                    ActivityCompat.RequestPermissions(this, new[] { Android.Manifest.Permission.ReadExternalStorage }, 1);

                if (CheckSelfPermission(Android.Manifest.Permission.ReadFrameBuffer) != Permission.Granted)
                    ActivityCompat.RequestPermissions(this, new[] { Android.Manifest.Permission.ReadFrameBuffer }, 1);

                if (CheckSelfPermission(Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                    ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.WriteExternalStorage }, 1);

                if (CheckSelfPermission(Manifest.Permission.SystemAlertWindow) != Permission.Granted)
                    ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.SystemAlertWindow }, 1);
       
                if (CheckSelfPermission(Manifest.Permission.KillBackgroundProcesses) != Permission.Granted)
                    ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.KillBackgroundProcesses }, 1);
 
                if (CheckSelfPermission(Manifest.Permission.ReceiveBootCompleted) != Permission.Granted)
                    ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.ReceiveBootCompleted }, 1);

                if (!Settings.CanDrawOverlays(this))
                    StartActivityForResult(new Intent(Settings.ActionManageOverlayPermission, Android.Net.Uri.Parse("package:" + PackageName)), 0);

                var intent_ = new IntentFilter(Intent.ActionMediaScannerScanFile);
                screenshotReceiver = new ScreenshotReceiver();
                RegisterReceiver(screenshotReceiver, intent_);

                var _observer = new ScreenshotObserver(new Handler(), this);
                ContentResolver.RegisterContentObserver(MediaStore.Images.Media.ExternalContentUri, true, _observer);

                Toast.MakeText(this, "QRCode Detector activé", ToastLength.Long).Show();

                Finish();
            }
            Finish();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Obsolete]
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 0)
            {
                if (Settings.CanDrawOverlays(this))
                    StartService(new Intent(this, typeof(ScreenshotService)));
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (!IsTaskAlreadyRunning())
            {
                UnbindService(_serviceConnection);
                UnregisterReceiver(screenshotReceiver);
            }
        }
        private bool IsTaskAlreadyRunning()
        {
            var activityManager = (ActivityManager)GetSystemService(Context.ActivityService);
            var runningProcesses = activityManager.GetRunningServices(int.MaxValue);
            foreach (var process in runningProcesses)
            {
                if (process.Process == "com.companyname.appprojet2")
                    return true;
            }
            return false;
        }
    }
}
