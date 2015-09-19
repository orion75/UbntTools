using System;

namespace UbntTools.Entity {

	public class FirmWare {

		public String Version { get; private set; }

		public String Build { get; private set; }

		public String LongFirmware { get; private set; }

		public FirmWare (string longfirmware) {
			LongFirmware = longfirmware;
			string[] array = longfirmware.Split('.');
			Version = string.Format("{0}.{1}.{2}", array [2], array [3], array [4]);
			Build = array [5];
		}
	}
}

