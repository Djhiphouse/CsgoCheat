using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZBase.Classes;
using ZBase.Utilities;

namespace ZBase
{
	public class Main
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool AllocConsole();
		public static Size ScreenSize;
		public static Vector2 MidScreen;
		public static RECT ScreenRect;

		public static RootObject O;
		public static Settings S = new Settings();
		public static bool RunStartup()
		{
			var CSGO = Process.GetProcessesByName("csgo");
			if (CSGO.Length != 0)
			{
				Memoryy.Process = CSGO[0];
				Memoryy.ProcessHandle = Memoryy.OpenProcess(0x0008 | 0x0010 | 0x0020, false, Memoryy.Process.Id);
				Player player = new Player();
				player.MemoryOpen(Memoryy.Process.Id);
				foreach (ProcessModule Module in Memoryy.Process.Modules)
				{
					if ((Module.ModuleName == "client.dll"))
						Memoryy.Client = Module.BaseAddress;

					if ((Module.ModuleName == "engine.dll"))
					{
						Memoryy.Engine = Module.BaseAddress;
						G.Engine = new Engine((int)Module.BaseAddress);
					}
				}
				return true;
			}
			else
			{
				MessageBox.Show("Please start CSGO Running the cheat", "Error", MessageBoxButtons.OK);
				Environment.Exit(1);
				return false;
			}
		}
	}
}
