package mono.org.opencv.android;


public class CameraBridgeViewBase_CvCameraViewListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		org.opencv.android.CameraBridgeViewBase.CvCameraViewListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCameraFrame:(Lorg/opencv/core/Mat;)Lorg/opencv/core/Mat;:GetOnCameraFrame_Lorg_opencv_core_Mat_Handler:OpenCV.Android.CameraBridgeViewBase/ICvCameraViewListenerInvoker, OpenCV\n" +
			"n_onCameraViewStarted:(II)V:GetOnCameraViewStarted_IIHandler:OpenCV.Android.CameraBridgeViewBase/ICvCameraViewListenerInvoker, OpenCV\n" +
			"n_onCameraViewStopped:()V:GetOnCameraViewStoppedHandler:OpenCV.Android.CameraBridgeViewBase/ICvCameraViewListenerInvoker, OpenCV\n" +
			"";
		mono.android.Runtime.register ("OpenCV.Android.CameraBridgeViewBase+ICvCameraViewListenerImplementor, OpenCV", CameraBridgeViewBase_CvCameraViewListenerImplementor.class, __md_methods);
	}


	public CameraBridgeViewBase_CvCameraViewListenerImplementor ()
	{
		super ();
		if (getClass () == CameraBridgeViewBase_CvCameraViewListenerImplementor.class)
			mono.android.TypeManager.Activate ("OpenCV.Android.CameraBridgeViewBase+ICvCameraViewListenerImplementor, OpenCV", "", this, new java.lang.Object[] {  });
	}


	public org.opencv.core.Mat onCameraFrame (org.opencv.core.Mat p0)
	{
		return n_onCameraFrame (p0);
	}

	private native org.opencv.core.Mat n_onCameraFrame (org.opencv.core.Mat p0);


	public void onCameraViewStarted (int p0, int p1)
	{
		n_onCameraViewStarted (p0, p1);
	}

	private native void n_onCameraViewStarted (int p0, int p1);


	public void onCameraViewStopped ()
	{
		n_onCameraViewStopped ();
	}

	private native void n_onCameraViewStopped ();

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
