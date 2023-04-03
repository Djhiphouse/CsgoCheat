using System.Threading;
using ZBase.Utilities;

namespace ZBase.Cheats
{
	public class FovView
	{
		public static Siticone.Desktop.UI.WinForms.SiticoneTrackBar Bar;

		public FovView(Siticone.Desktop.UI.WinForms.SiticoneTrackBar BarBox)
		{
			Bar = BarBox;
		}
		public void Run()
		{
			int latestvalue = 0;
			while (true)
			{
				if (Main.S.FovViewChanger) // make sure the cheats enabled in the menu
				{

					G.Engine.FovChanger(Bar.Value);





				}
				Thread.Sleep(65); // reduce cpu usage again
			}
		}
	}
}
