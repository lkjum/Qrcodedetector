package crc64d820167fea0c21a7;


public class ScreenshotServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AppProjet2.ScreenshotServiceBinder, AppProjet2", ScreenshotServiceBinder.class, __md_methods);
	}


	public ScreenshotServiceBinder ()
	{
		super ();
		if (getClass () == ScreenshotServiceBinder.class)
			mono.android.TypeManager.Activate ("AppProjet2.ScreenshotServiceBinder, AppProjet2", "", this, new java.lang.Object[] {  });
	}


	public ScreenshotServiceBinder (java.lang.String p0)
	{
		super (p0);
		if (getClass () == ScreenshotServiceBinder.class)
			mono.android.TypeManager.Activate ("AppProjet2.ScreenshotServiceBinder, AppProjet2", "System.String, mscorlib", this, new java.lang.Object[] { p0 });
	}

	public ScreenshotServiceBinder (crc64d820167fea0c21a7.ScreenshotService p0)
	{
		super ();
		if (getClass () == ScreenshotServiceBinder.class)
			mono.android.TypeManager.Activate ("AppProjet2.ScreenshotServiceBinder, AppProjet2", "AppProjet2.ScreenshotService, AppProjet2", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
