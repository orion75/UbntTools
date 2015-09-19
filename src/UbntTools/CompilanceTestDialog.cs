using System;
using Xwt;
using Xwt.Drawing;
using Renci.SshNet;
using System.ComponentModel;

namespace UbntTools {
	
	public class CompilanceTestDialog : Dialog {
		
		string _ip, _user, _pwd;
		TextEntry EntryIp, EntryUser;
		PasswordEntry EntryPasswd;
		Button BtnConnect, BtnClose;
		BackgroundWorker backgroundWorker;
		string mensaje = string.Empty;

		public CompilanceTestDialog () {
			backgroundWorker = new BackgroundWorker ();
			backgroundWorker.WorkerReportsProgress = false;
			backgroundWorker.WorkerSupportsCancellation = true;
			backgroundWorker.DoWork += BackgroundWorker_DoWork;
			backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

			Icon = Image.FromResource ("UbntTools.Resources.icon-64x64.png");
			Title = string.Empty;
			Table table = new Table ();

			EntryIp = new TextEntry { Text = "192.168.1.20" };
			EntryPasswd = new PasswordEntry { TooltipText = "Device Password" };
			EntryUser = new TextEntry { Text = "ubnt" };

			table.Add (new Label ("Ip Address:: ") { TextAlignment = Alignment.End }, 0, 0);
			table.Add (EntryIp, 1, 0);
			table.Add (new Label ("User: ") { TextAlignment = Alignment.End }, 0, 1);
			table.Add (EntryUser, 1, 1);
			table.Add (new Label ("Password: ") { TextAlignment = Alignment.End }, 0, 2);
			table.Add (EntryPasswd, 1, 2);

			BtnConnect = new Button ("_Connect");
			table.Add (BtnConnect, 2, 0);

			BtnClose = new Button ("_Salir");
			table.Add (BtnClose, 2, 1);

			BtnConnect.Clicked += (sender, e) => {
				this.Content.Sensitive = false;
				this.BtnClose.Sensitive = false;
				this.BtnConnect.Sensitive = false;
				_ip = this.EntryIp.Text;
				_user = this.EntryUser.Text;
				_pwd = this.EntryPasswd.Password;

				if (!backgroundWorker.IsBusy)
					backgroundWorker.RunWorkerAsync ();
			};

			BtnClose.Clicked += (sender, e) => { Respond (Command.Close);};

			Content = table;
		}


		void BackgroundWorker_DoWork (object sender, DoWorkEventArgs e) {
			var stringcommand = "echo 'echo '\"'\"'\t<option value=\"511\">Compilance Test</option>\r\n\t<option value=\"32\">Argentina</option>\r\n\t<option value=\"51\">Armenia</option>\r\n\t<option value=\"533\">Aruba</option>\r\n\t<option value=\"36\">Australia</option>\r\n\t<option value=\"40\">Austria</option>\r\n\t<option value=\"31\">Azerbaijan</option>\r\n\t<option value=\"48\">Bahrain</option>\r\n\t<option value=\"52\">Barbados</option>\r\n\t<option value=\"112\">Belarus</option>\r\n\t<option value=\"56\">Belgium</option>\r\n\t<option value=\"84\">Belize</option>\t\r\n\t<option value=\"68\">Bolivia</option>\r\n\t<option value=\"70\">Bosnia and Herzegovina</option>\r\n\t<option value=\"76\">Brazil</option>\r\n\t<option value=\"96\">Brunei Darussalam</option>\r\n\t<option value=\"100\">Bulgaria</option>\r'>/etc/persistent/rc.poststart";
			var stringcommand1 = "echo '\t<option value=\"116\">Cambodia</option>\r\n\t<option value=\"124\">Canada</option>\r\n\t<option value=\"152\">Chile</option>\r\n\t<option value=\"156\">China</option>\r\n\t<option value=\"170\">Colombia</option>\r\n\t<option value=\"188\">Costa Rica</option>\r\n\t<option value=\"191\">Croatia</option>\r\n\t<option value=\"196\">Cyprus</option>\r\n\t<option value=\"203\">Czech Republic</option>\r'>>/etc/persistent/rc.poststart";
			var stringcommand2 = "echo '\t<option value=\"208\">Denmark</option>\r\n\t<option value=\"214\">Dominican Republic</option>\r\n\t<option value=\"218\">Ecuador</option>\r\n\t<option value=\"818\">Egypt</option>\r\n\t<option value=\"222\">El Salvador</option>\r\n\t<option value=\"233\">Estonia</option>\r\n\t<option value=\"246\">Finland</option>\r\n\t<option value=\"250\">France</option>\r'>>/etc/persistent/rc.poststart";
			var stringcommand3 = "echo '\t<option value=\"268\">Georgia</option>\r\n\t<option value=\"276\">Germany</option>\r\n\t<option value=\"300\">Greece</option>\r\n\t<option value=\"304\">Greenland</option>\r\n\t<option value=\"308\">Grenada</option>\r\n\t<option value=\"316\">Guam</option>\r\n\t<option value=\"320\">Guatemala</option>\r\n\t<option value=\"332\">Haiti</option>\r\n\t<option value=\"340\">Honduras</option>\r\n\t<option value=\"344\">Hong Kong</option>\r\n\t<option value=\"348\">Hungary</option>\r'>>/etc/persistent/rc.poststart";
			var stringcommand4 = "echo '\t<option value=\"352\">Iceland</option>\r\n\t<option value=\"356\">India</option>\r\n\t<option value=\"360\">Indonesia</option>\r\n\t<option value=\"368\">Iraq</option>\r\n\t<option value=\"372\">Ireland</option>\r\n\t<option value=\"376\">Israel</option>\r\n\t<option value=\"380\">Italy</option>\r\n\t<option value=\"388\">Jamaica</option>\r\n\t<option value=\"400\">Jordan</option>\r\n\t<option value=\"404\">Kenya</option>\r\n\t<option value=\"410\">Korea Republic</option>\r\n\t<option value=\"414\">Kuwait</option>\r'>>/etc/persistent/rc.poststart";
			var stringcommand5 = "echo '\t<option value=\"428\">Latvia</option>\r\n\t<option value=\"422\">Lebanon</option>\r\n\t<option value=\"438\">Liechtenstein</option>\r\n\t<option value=\"440\">Lithuania</option>\r\n\t<option value=\"442\">Luxembourg</option>\r\n\t<option value=\"446\">Macau</option>\r\n\t<option value=\"807\">Macedonia</option>\r\n\t<option value=\"458\">Malaysia</option>\r\n\t<option value=\"470\">Malta</option>\r\n\t<option value=\"484\">Mexico</option>\r\n\t<option value=\"492\">Monaco</option>\r'>>/etc/persistent/rc.poststart";
			var stringcommand6 = "echo '\t<option value=\"499\">Montenegro</option>\r\n\t<option value=\"504\">Morocco</option>\r\n\t<option value=\"524\">Nepal</option>\r\n\t<option value=\"528\">Netherlands</option>\r\n\t<option value=\"530\">Netherlands Antilles</option>\r\n\t<option value=\"554\">New Zealand</option>\r\n\t<option value=\"566\">Nigeria</option>\r\n\t<option value=\"578\">Norway</option>\r\n\t<option value=\"512\">Oman</option>\r\n\t<option value=\"586\">Pakistan</option>\r\n\t<option value=\"591\">Panama</option>\r\n\t<option value=\"598\">Papua New Guinea</option>\r\n\t<option value=\"604\">Peru</option>\r\n\t<option value=\"608\">Philippines</option>\r'>>/etc/persistent/rc.poststart";
			var stringcommand7 = "echo '\t<option value=\"616\">Poland</option>\r\n\t<option value=\"620\">Portugal</option>\r\n\t<option value=\"630\">Puerto Rico (U.S. territory)</option>\r\n\t<option value=\"634\">Qatar</option>\r\n\t<option value=\"642\">Romania</option>\r\n\t<option value=\"643\">Russia</option>\r\n\t<option value=\"646\">Rwanda</option>\r\n\t<option value=\"652\">Saint Barthelemy</option>\r\n\t<option value=\"682\">Saudi Arabia</option>\r\n\t<option value=\"688\">Serbia</option>\r\n\t<option value=\"702\">Singapore</option>\r\n\t<option value=\"703\">Slovakia</option>\r\n\t<option value=\"705\">Slovenia</option>\r\n\t<option value=\"710\">South Africa</option>\r\n\t<option value=\"724\">Spain</option>\r\n\t<option value=\"144\">Sri Lanka</option>\r'>>/etc/persistent/rc.poststart";
			var stringcommand8 = "echo '\t<option value=\"752\">Sweden</option>\r\n\t<option value=\"756\">Switzerland</option>\r\n\t<option value=\"158\">Taiwan</option>\r\n\t<option value=\"764\">Thailand</option>\r\n\t<option value=\"780\">Trinidad and Tobago</option>\r\n\t<option value=\"788\">Tunisia</option>\r\n\t<option value=\"792\">Turkey</option>\r\n\t<option value=\"804\">Ukraine</option>\r\n\t<option value=\"784\">United Arab Emirates</option>\r\n\t<option value=\"826\">United Kingdom</option>\r\n\t<option value=\"840\">United States</option>\r\n\t<option value=\"858\">Uruguay</option>\r\n\t<option value=\"860\">Uzbekistan</option>\r\n\t<option value=\"862\">Venezuela</option>\r\n\t<option value=\"704\">Viet Nam</option>'\"'\"'>/etc/ccodes.inc'>>/etc/persistent/rc.poststart";


			using (var sshclient = new SshClient (_ip, _user, _pwd)) {
				try {
					sshclient.Connect ();

					using (var cmd = sshclient.CreateCommand ("cat /etc/version")) {
						cmd.Execute();
						mensaje = "Version del Producto: " +  cmd.Result.TrimEnd() + "\n";
					}

					using (var cmd = sshclient.CreateCommand (stringcommand))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand1))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand2))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand3))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand4))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand5))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand6))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand7))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand (stringcommand8))
						cmd.Execute ();

					using (var cmd = sshclient.CreateCommand ("chmod +x /etc/persistent/rc.poststart"))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand ("cfgmtd -w -p /etc/"))
						cmd.Execute ();
					using (var cmd = sshclient.CreateCommand ("/etc/persistent/rc.poststart"))
						cmd.Execute ();

					sshclient.Disconnect ();
				} catch {
					throw new InvalidOperationException ("No se pudo conectar con el equipo.\nVerifique la direccion ip o las credenciales"); 
				}

			}
		}


		void BackgroundWorker_RunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e) {
			if (e.Error != null) {
				MessageDialog.ShowError (this, "Ocurrio un error", e.Error.Message);
			} else {
				MessageDialog.ShowMessage (this, "Contry codes creados en el dispositivo.", mensaje);
			}
			this.Content.Sensitive = true;
			this.BtnClose.Sensitive = true;
			this.BtnConnect.Sensitive = true;
			this.EntryIp.SetFocus ();
		}

	}
}