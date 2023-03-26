using System.Threading;
using ZBase;
using ZBase.Utilities;

namespace ValorantCheat.Cheats
{
	public class FakeLag
	{

		public static void Run()
		{
			while (true)
			{
				if (Main.S.FakeLag) // make sure the cheats enabled in the menu
				{

					G.Engine.FakeLag(true);
					Thread.Sleep(14);
					G.Engine.FakeLag(false);
				}

				Thread.Sleep(1); // reduce cpu usage again
			}
		}


	}
}
