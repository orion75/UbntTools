using System;
using System.Configuration;

namespace UbntTools {

	class Program {

		[STAThread]
		public static void Main (string[] arguments) {

			EmbeddedAssembly.Load ("Resources.Xwt.dll", "Xwt.dll");
			EmbeddedAssembly.Load ("Resources.Xwt.Gtk3.dll", "Xwt.Gtk3.dll");
			EmbeddedAssembly.Load ("Resources.Xwt.WPF.dll", "Xwt.WPF.dll");
			EmbeddedAssembly.Load ("Resources.Renci.SshNet.dll", "Renci.SshNet.dll");




			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

//			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
//				return EmbeddedAssembly.Get(args.Name);
//			};


			IdeStartup app = new IdeStartup ();
			app.Run (arguments);


		}

		static System.Reflection.Assembly CurrentDomain_AssemblyResolve (object sender, ResolveEventArgs args) {
			return EmbeddedAssembly.Get (args.Name);
		}

	}
}