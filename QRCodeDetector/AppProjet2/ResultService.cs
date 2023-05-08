using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;

namespace AppProjet2
{
    [Service(Enabled = true)]
    public class ResultService: Service
    {
        private AlertDialog dialog;
        private WindowManagerLayoutParams _layoutParams;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var result = intent.GetStringExtra("link");

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("QRCode détecté");
            alert.SetMessage("Voulez-vous lire son contenu ? ( " + result + " )");
            alert.SetPositiveButton("Oui", (senderAlert, args) =>
            {
                var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(result));
                intent.AddFlags(ActivityFlags.NewTask);
                StartActivity(intent);
            });
            alert.SetNegativeButton("Non", (senderAlert, args) =>
            {
                dialog.Dismiss();
            });
            dialog = alert.Create();
            dialog.Window.Attributes = _layoutParams;
            dialog.Show();
            

            return StartCommandResult.NotSticky;
        }
        public override void OnCreate()
        {
            base.OnCreate();

            _layoutParams = new WindowManagerLayoutParams(
                WindowManagerLayoutParams.WrapContent,
                WindowManagerLayoutParams.WrapContent,
                WindowManagerTypes.ApplicationOverlay,
                WindowManagerFlags.NotFocusable,
                Format.Translucent 
            );
            _layoutParams.Gravity = GravityFlags.Center;
        }
    }
}