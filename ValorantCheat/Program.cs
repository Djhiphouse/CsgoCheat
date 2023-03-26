using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ValorantCheat
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// [DllImport("kernel32.dll", SetLastError = true)]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();

		// Ausblenden des Konsolenfensters
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		const int SW_HIDE = 0;


		[STAThread]
		static void Main()
		{
			Cheat cheat = new Cheat();
			if (cheat.CheckbetaKey())
				cheat.Inject();

			// Ausblenden des Konsolenfensters



		}
	}
	public class Cheat
	{

		public bool CheckbetaKey()
		{
			WebClient Keys = new WebClient();
			//FadeInW("Beta Key: ", Status.Sucess);
			String key = Console.ReadLine();
			if (true/*Keys.DownloadString("https://pastebin.com/raw/gfmw9hCS").Contains(key)*/)
			{
				FadeIn("Sucessfully use Beta Key", Status.Sucess);
				return true;
			}
			else
			{
				FadeIn("Failed to use Beta Key", Status.Failed);
				Thread.Sleep(3000);
				return false;
			}


		}
		public enum Status
		{
			Sucess,
			Failed
		}
		public void LoadDriver()
		{
			FadeIn("Driver has been loaded.", Status.Sucess);
		}
		public void Inject()
		{
			var CSGO = Process.GetProcessesByName("csgo");
			if (CSGO.Length == 0)
			{
				new Cheat().FadeIn("CSGO not Found, Open it", Status.Failed);
				Thread.Sleep(2500);
				return;
			}
			new Cheat().LoadDriver();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
		public void FadeIn(String text, Enum state)
		{
			if (state.ToString() == Status.Sucess.ToString())
			{
				String sucessfully = "[+]";
				for (int i = 0; i < sucessfully.Length; i++)
				{
					Thread.Sleep(250);
					if (i == 1)
						Console.ForegroundColor = ConsoleColor.Green;
					else
						Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(sucessfully[i]);
				}
			}
			else if (state.ToString() == Status.Failed.ToString())
			{
				String Negative = "[-]";
				for (int i = 0; i < Negative.Length; i++)
				{
					Thread.Sleep(250);
					if (i == 1)
						Console.ForegroundColor = ConsoleColor.Red;
					else
						Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(Negative[i]);
				}
			}
			Console.Write(" ");
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			for (int i = 0; i < text.Length; i++)
			{
				Thread.Sleep(55);
				Console.Write(text[i]);
			}
			Console.WriteLine();
		}
		public void FadeInW(String text, Enum state)
		{
			if (state.ToString() == Status.Sucess.ToString())
			{
				String sucessfully = "[+]";
				for (int i = 0; i < sucessfully.Length; i++)
				{
					Thread.Sleep(250);
					if (i == 1)
						Console.ForegroundColor = ConsoleColor.Green;
					else
						Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(sucessfully[i]);
				}
			}
			else if (state.ToString() == Status.Failed.ToString())
			{
				String Negative = "[-]";
				for (int i = 0; i < Negative.Length; i++)
				{
					Thread.Sleep(250);
					if (i == 1)
						Console.ForegroundColor = ConsoleColor.Red;
					else
						Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(Negative[i]);
				}
			}
			Console.Write(" ");
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			for (int i = 0; i < text.Length; i++)
			{
				Thread.Sleep(55);
				Console.Write(text[i]);
			}
			//Console.WriteLine();
		}
	}
}
