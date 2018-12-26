package md5905e9b2dcff1d841c4aff07781525750;


public class TreeNodesContainerBase
	extends android.widget.FrameLayout
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Syncfusion.Android.TreeView.TreeNodesContainerBase, Syncfusion.SfTreeView.XForms.Android", TreeNodesContainerBase.class, __md_methods);
	}


	public TreeNodesContainerBase (android.content.Context p0)
	{
		super (p0);
		if (getClass () == TreeNodesContainerBase.class)
			mono.android.TypeManager.Activate ("Syncfusion.Android.TreeView.TreeNodesContainerBase, Syncfusion.SfTreeView.XForms.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public TreeNodesContainerBase (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == TreeNodesContainerBase.class)
			mono.android.TypeManager.Activate ("Syncfusion.Android.TreeView.TreeNodesContainerBase, Syncfusion.SfTreeView.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public TreeNodesContainerBase (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == TreeNodesContainerBase.class)
			mono.android.TypeManager.Activate ("Syncfusion.Android.TreeView.TreeNodesContainerBase, Syncfusion.SfTreeView.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
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
