using Android.App;
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Widget;
using System;
using System.Threading.Tasks;

namespace AppProjet2
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class ScreenshotReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
        }
    }
    public class ScreenshotObserver : ContentObserver
    {
        private Context _context;

        public ScreenshotObserver(Handler handler, Context context) : base(handler)
        {
            _context = context;
        }

        public override async void OnChange(bool selfChange, Android.Net.Uri uri)
        {
            base.OnChange(selfChange, uri);

            KeyguardManager keyguardManager = (KeyguardManager)_context.GetSystemService(Context.KeyguardService);
            if (keyguardManager.IsKeyguardLocked)
                return;

            PowerManager powerManager = (PowerManager)_context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = powerManager.NewWakeLock(WakeLockFlags.Full | WakeLockFlags.AcquireCausesWakeup, "BootReceiver::WakeLock");
            wakeLock.Acquire(500);

            try
            {
                var projection = new string[] { MediaStore.Images.ImageColumns.Data };
                var cursor = _context.ContentResolver.Query(
                    uri, projection, null, null, MediaStore.Images.ImageColumns.DateAdded + " desc limit 1");

                if (cursor != null && cursor.MoveToFirst())
                {
                    var path = cursor.GetString(cursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data));

                    if (path.ToLower().Contains("screenshot"))
                    {
                        var stream = Application.Context.ContentResolver.OpenInputStream(uri);

                        string result = await Task.Run(async () =>
                        {
                            Bitmap bitmap = BitmapFactory.DecodeStream(stream);
                            byte[] bitmapToArray = QRCodeAnalyser.ConvertBitmapToByteArray(bitmap);
                            string byteStr = QRCodeAnalyser.ConvertToByte(bitmapToArray);
                            var result = await QRCodeAnalyser.DetectQRCode(byteStr);
                            return result;
                        });

                        if (result == "NOK")
                        {

                        }
                        else if (result.StartsWith("http://") || result.StartsWith("https://"))
                        {
                            var serviceIntent = new Intent(_context, typeof(ResultService));
                            serviceIntent.PutExtra("link", result);
                            _context.StartService(serviceIntent);
                        }
                        else
                        {
                            Toast.MakeText(_context, "Contenu du QRCode: " + result, ToastLength.Long).Show();
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                Toast.MakeText(_context, ex.Message, ToastLength.Long).Show();
                Log.Debug("Boot", ex.Message);
            }
        }
    }
}