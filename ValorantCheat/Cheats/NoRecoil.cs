using System.Threading;
using ZBase.Utilities;

namespace ZBase.Cheats
{
	public class NoRecoil
	{
		public static void Run()
		{
			while (true)
			{
				if (Main.S.NoRecoil) // make sure the cheats enabled in the menu
				{

					// Flags show if you are on the ground or not. 257 is standing on the ground, and 263 is crouching on the ground.
					if (Tools.HoldingKey("")) // while holding space
						G.Engine.ReduceRecoil();


				}
				Thread.Sleep(1); // reduce cpu usage again
			}
		}
	}
}
