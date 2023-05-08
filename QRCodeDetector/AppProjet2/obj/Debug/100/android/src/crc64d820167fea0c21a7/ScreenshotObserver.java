package crc64d820167fea0c21a7;


public class ScreenshotObserver
	extends android.database.ContentObserver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onChange:(ZLandroid/net/Uri;)V:GetOnChange_ZLandroid_net_Uri_Handler\n" +
			"";
		mono.android.Runtime.register ("AppProjet2.ScreenshotObserver, AppProjet2", ScreenshotObserver.class, __md_methods);
	}


	public ScreenshotObserver (android.os.Handler p0)
	{
		super (p0);
		if (getClass () == ScreenshotObserver.class)
			mono.android.TypeManager.Activate ("AppProjet2.ScreenshotObserver, AppProjet2", "Android.OS.Handler, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onChange (boolean p0, android.net.Uri p1)
	{
		n_onChange (p0, p1);
	}

	private native void n_onChange (boolean p0, android.net.Uri p1);

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
