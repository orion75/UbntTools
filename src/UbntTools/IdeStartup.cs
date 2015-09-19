using System;
using System.IO;
using System.Reflection;
using Xwt;

namespace UbntTools {

	public class IdeStartup : IApplication {

		#region IApplication implementation

		public int Run (string[] arguments) {

			var exename = Path.GetFileNameWithoutExtension (Assembly.GetEntryAssembly ().Location);
			if (Platform.IsLinux) {
				exename = exename.ToLower ();
				Application.Initialize (ToolkitType.Gtk3);
			} else if (Platform.IsWindows) {
				Application.Initialize (ToolkitType.Wpf);
			} else if (Platform.IsMac)
				Application.Initialize (ToolkitType.Cocoa);

			Runtime.SetProcessName (exename);

			MainWindows window = new MainWindows ();
			window.Show ();
			Application.Run ();
			window.Dispose ();
			Application.Dispose ();
			return 0;
		}

		#endregion
	}
}