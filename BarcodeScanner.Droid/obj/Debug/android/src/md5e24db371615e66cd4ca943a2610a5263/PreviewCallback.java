package md5e24db371615e66cd4ca943a2610a5263;


public class PreviewCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.hardware.Camera.PreviewCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreviewFrame:([BLandroid/hardware/Camera;)V:GetOnPreviewFrame_arrayBLandroid_hardware_Camera_Handler:Android.Hardware.Camera/IPreviewCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("MWBarcodeScanner.PreviewCallback, MWBarcodeScanner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", PreviewCallback.class, __md_methods);
	}


	public PreviewCallback () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PreviewCallback.class)
			mono.android.TypeManager.Activate ("MWBarcodeScanner.PreviewCallback, MWBarcodeScanner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPreviewFrame (byte[] p0, android.hardware.Camera p1)
	{
		n_onPreviewFrame (p0, p1);
	}

	private native void n_onPreviewFrame (byte[] p0, android.hardware.Camera p1);

	java.util.ArrayList refList;
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
