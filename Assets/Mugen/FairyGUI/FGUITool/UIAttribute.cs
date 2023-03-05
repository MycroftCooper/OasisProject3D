using System;

namespace Medium.FGUI
{
	// 组件扩展时对应的组件类型
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class UIPackageItemExtension : System.Attribute
	{
		public string url;

		public UIPackageItemExtension(string url) {	this.url = url; }
	}
}
