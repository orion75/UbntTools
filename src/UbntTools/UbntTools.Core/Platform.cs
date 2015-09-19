using System;
using System.IO;
using System.Runtime.InteropServices;

namespace UbntTools {

	public class Platform {

		public readonly static bool IsWindows;
		public readonly static bool IsMac;
		public readonly static bool IsLinux;

		static Platform () {
			IsWindows = Path.DirectorySeparatorChar == '\\';
			IsMac = !IsWindows && IsRunningOnMac ();
			IsLinux = !IsWindows && !IsMac;

			if (Platform.IsMac)
				InitMacFoundation ();
		}

		public static void Initialize () {
			//no-op, triggers static ctor
		}

		[DllImport ("libc")]
		static extern int uname (IntPtr buf);
		//From Managed.Windows.Forms/XplatUI
		static bool IsRunningOnMac () {
			IntPtr buf = IntPtr.Zero;
			try {
				buf = Marshal.AllocHGlobal (8192);
				// This is a hacktastic way of getting sysname from uname ()
				if (uname (buf) == 0) {
					string os = Marshal.PtrToStringAnsi (buf);
					if (os == "Darwin")
						return true;
				}
			} catch {
			} finally {
				if (buf != IntPtr.Zero)
					Marshal.FreeHGlobal (buf);
			}
			return false;
		}

		[DllImport ("libc")]
		extern static IntPtr dlopen (string name, int mode);

		static void InitMacFoundation () {
			dlopen ("/System/Library/Frameworks/Foundation.framework/Foundation", 0x1);
		}
	}
}