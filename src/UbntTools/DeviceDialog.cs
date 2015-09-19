using System;
using Xwt;
using UbntTools.Entity;

namespace UbntTools {

	public class DeviceDialog : Dialog {

		public DeviceDialog (Device device) {
			
			Table table = new Table ();



			table.Add (new Label ("HW Address: ") { TextAlignment = Alignment.End }, 0, 0);
			table.Add (new Label (device.FormatedMacAddress), 1, 0);
			table.Add (new Label ("Ip Address: ") { TextAlignment = Alignment.End }, 0, 1);
			table.Add (new Label (device.FirstAddress.ToString ()), 1, 1);
			table.Add (new Label ("Version: ") { TextAlignment = Alignment.End }, 0, 2);
			table.Add (new Label (device.Firmware.Version), 1, 2);
			table.Add (new Label ("Build Number: ") { TextAlignment = Alignment.End }, 0, 3);
			table.Add (new Label (device.Firmware.Build), 1, 3);
			table.Add (new Label ("Uptime: ") { TextAlignment = Alignment.End }, 0, 4);
			table.Add (new Label ( string.Format ("{0:%d} dias {1:hh\\:mm\\:ss}", device.Uptime, device.Uptime)), 1, 4);


			 

			Buttons.Add (new DialogButton ("Close", Command.Close)  );

			Content = table;

		}
	}
}