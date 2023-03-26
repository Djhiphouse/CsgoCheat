
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;
using ZBase.Classes;

namespace ZBase.Utilities
{
	// hazedumper auto offset updater - veri cool
	public class OffsetUpdater
	{
		public static void GetOffsetsFromFile()
		{
			string json = File.ReadAllText($@"{Application.StartupPath}\csgo.json");
			Main.O = JsonConvert.DeserializeObject<RootObject>(json);
		}
		public static void UpdateOffsets()
		{
			//https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.json
			System.Net.WebClient wc = new System.Net.WebClient();
			String raw = wc.DownloadString("https://pastebin.com/raw/qkVYUf1b");
			//string webData = Encoding.UTF8.GetString(raw);
			File.WriteAllText($@"{Application.StartupPath}\csgo.json", raw);
			GetOffsetsFromFile();
		}
	}
}
