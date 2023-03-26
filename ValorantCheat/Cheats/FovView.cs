using System.Threading;
using ZBase.Utilities;

namespace ZBase.Cheats
{
	public class FovView
	{
		public static Siticone.UI.WinForms.SiticoneSlider Bar;

		public FovView(Siticone.UI.WinForms.SiticoneSlider BarBox)
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
