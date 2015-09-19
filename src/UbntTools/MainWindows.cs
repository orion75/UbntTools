using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xwt;
using Xwt.Drawing;
using UbntDiscovery.Core;
using System.Threading;
using System.Diagnostics;
using UbntTools.Entity;

namespace UbntTools {

	public class MainWindows : Window {

		Menu menu;
		ListView list;
		ListStore Store;
		Label lbcount;
		SearchTextEntry SearchEntry;
		TextEntry EntrySearch;
		Button BtnScan, BtnClear, BtnExit, BtnCompilanceTest;
		DataField<string>  plataformField, hostnameField, ssidField, modeField, ipField, macField, firmwareField, uptimeField;


		public List<Device> Devices { get; private set; }
		public DeviceDiscovery DeviceDiscovery { get; private set; }

		public MainWindows () {
			Title = "Ubnt Tools";
			if (Platform.IsLinux)
				Width = 820;
			if (Platform.IsWindows)
				Width = 650;
			Height = 350;
			Icon = Image.FromResource ("UbntTools.Resources.icon-64x64.png");

			list = new ListView ();
			plataformField = new DataField<string> ();
			hostnameField = new DataField<string> ();
			ssidField = new DataField<string> ();
			modeField = new DataField<string> ();
			ipField = new DataField<string> ();
			macField = new DataField<string> ();
			firmwareField = new DataField<string> ();
			uptimeField = new DataField<string> ();

			Store = new ListStore (plataformField, hostnameField, ssidField, modeField, ipField, macField, firmwareField, uptimeField);
			list.DataSource = Store;
			list.GridLinesVisible = GridLines.Both;

			var pp = new ListViewColumn ("title");

			pp.CanResize = true;

			list.Columns.Add (new ListViewColumn ("Producto             ", new TextCellView { Editable = false, TextField = plataformField }));
			list.Columns.Add (new ListViewColumn ("Hostname                              ", new TextCellView { Editable = false, TextField = hostnameField }));
			list.Columns.Add (new ListViewColumn ("SSID                       ", new TextCellView { Editable = false, TextField = ssidField }));
//			list.Columns.Add (new ListViewColumn ("Mode       ", new TextCellView { Editable = false, TextField = modeField }));
			list.Columns.Add (new ListViewColumn ("Ip Address     ", new TextCellView { Editable = false, TextField = ipField }));
			list.Columns.Add (new ListViewColumn ("MAC Address            ", new TextCellView { Editable = false, TextField = macField }));
//			list.Columns.Add (new ListViewColumn ("Uptime", new TextCellView { Editable = false, TextField = uptimeField }));
			list.Columns.Add (new ListViewColumn ("Versión", new TextCellView { Editable = false, TextField = firmwareField }));



			var boxcontent = new VBox ();
			var box_search_count = new HBox ();
			var box_search = new HBox ();

			lbcount = new Label ("Total: 0  ");

			box_search_count.PackStart (box_search, true);
			box_search_count.PackStart (lbcount);
			box_search.PackStart (new Label ("Buscar : "));
			if (!Platform.IsWindows) {
				SearchEntry = new SearchTextEntry () { MinWidth = 250 };
				box_search.PackStart (SearchEntry);
			} else {
				EntrySearch = new TextEntry () { MinWidth = 200 };
				box_search.PackStart (EntrySearch);
			}
			boxcontent.PackStart (box_search_count);


			boxcontent.PackStart (list, true);

			var buttonbox = new HBox ();
			BtnScan = new Button ("S_canear") { MinWidth = 100 };
			BtnClear = new Button ("_Limpiar") { MinWidth = 100 };
			BtnExit = new Button ("_Salir  "){ MinWidth = 100 };
			BtnCompilanceTest = new Button ("Compilance _Test"){ MinWidth = 100 };

			buttonbox.PackStart (BtnScan, false);
			buttonbox.PackStart (BtnClear, false);
			buttonbox.PackStart (BtnExit, false);
			buttonbox.PackStart (BtnCompilanceTest, false);

			boxcontent.PackStart (buttonbox, hpos: WidgetPlacement.End);
			Content = boxcontent;



			list.ButtonPressed += list_ButtonPressed;
			menu = new Menu ();
			var item_1 = new MenuItem ("Detalles");
			item_1.Clicked += (sender, e) =>  OpenDeviceDialog ();
			var item_2 = new MenuItem ("Web Ui");
			item_2.Clicked += (sender, e) =>  {
				Process.Start (string.Format ("http://{0}", Devices[list.SelectedRow].FirstAddress));
			};
			menu.Items.Add (item_1);
			menu.Items.Add (item_2);
			menu.Items.Add (new SeparatorMenuItem ());
			menu.Items.Add (new MenuItem ("Reiniciar"));

			BtnScan.Clicked += (sender, e) => Scan ();
			BtnExit.Clicked += (sender, e) => Close ();
			BtnClear.Clicked += (sender, e) => {
				DeviceDiscovery.EndDiscoverDevices();
				Devices.Clear ();
				lbcount.Text = "Total: 0  ";
				Store.Clear();
			};
			BtnCompilanceTest.Clicked += (sender, e) => {
				var dlg = new CompilanceTestDialog ();
				dlg.Run (this);
				dlg.Dispose ();
			};
			CloseRequested += MainWindows_CloseRequested;
			Scan ();
		}

		void list_ButtonPressed(object sender, ButtonEventArgs e) {
			if (e.Button == PointerButton.Left && e.MultiplePress == 2 && Devices != null && Devices.Count > 0)
					OpenDeviceDialog ();
			if (e.Button == PointerButton.Right && e.MultiplePress == 1 && Devices != null && Devices.Count > 0)
				menu.Popup ();
		}


		void Scan () {
			if (DeviceDiscovery == null) {
				Devices = new List<Device> ();
				DeviceDiscovery = new DeviceDiscovery (this.AddDevice);
			}
			try {
				if (!DeviceDiscovery.IsScanning) {           
					DeviceDiscovery.BeginDiscoverDevices ();
				} else {
					DeviceDiscovery.EndDiscoverDevices ();
					Devices.Clear ();
					lbcount.Text = "Total: 0  ";
					Store.Clear ();
					Thread.Sleep (100);
					DeviceDiscovery.BeginDiscoverDevices ();
				}
			} catch (Exception ex) {
				MessageDialog.ShowError ("Error", ex.Message);
			}
		}

		public void AddDevice(Device device) {
			Application.Invoke (delegate {
				if (Devices.IndexOf (device) == -1) {
					Devices.Add (device);
					var r = Store.AddRow ();
					Store.SetValue (r, plataformField, device.LongPlatform);
					Store.SetValue (r, hostnameField, device.Hostname);
					Store.SetValue (r, ssidField, device.SSID);
					Store.SetValue (r, modeField, device.WirelessModeDescription);
					Store.SetValue (r, ipField, device.FirstAddress.ToString ());
					Store.SetValue (r, macField, device.FormatedMacAddress);
					Store.SetValue (r, firmwareField, device.Firmware.Version);
					Store.SetValue (r, uptimeField, string.Format ("{0:%d} dias {1:hh\\:mm\\:ss}", device.Uptime, device.Uptime));
					lbcount.Text = string.Format ("Total: {0}  ", Devices.Count);
				}
			});
		}

		void OpenDeviceDialog () {
			var dlg = new DeviceDialog (Devices[list.SelectedRow]);
			dlg.Run (this);
			dlg.Dispose ();
		}

		void MainWindows_CloseRequested (object sender, Xwt.CloseRequestedEventArgs args) {
			var question = new QuestionMessage ("Salir de la aplicación", "¿Seguro que quieres salir de la aplicación?\t\t");
			question.Buttons.Add (new Command ("_No"));
			question.Buttons.Add (new Command ("_Si"));
			question.DefaultButton = 0;

			args.AllowClose = MessageDialog.AskQuestion (question).Id == "_Si";
			if (args.AllowClose)
				Application.Exit ();
		}

		protected override void Dispose (bool disposing) {
			base.Dispose (disposing);
		}
	}
}