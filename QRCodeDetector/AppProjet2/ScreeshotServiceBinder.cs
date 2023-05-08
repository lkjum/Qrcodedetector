using Android.OS;

namespace AppProjet2
{
    public class ScreenshotServiceBinder : Binder
    {
        private ScreenshotService _service;

        public ScreenshotServiceBinder()
        {
        }

        public ScreenshotServiceBinder(ScreenshotService service)
        {
            _service = service;
        }

        public ScreenshotService GetService()
        {
            return _service;
        }
    }
}