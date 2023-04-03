using System.Threading;
using ZBase.Utilities;

namespace ZBase.Cheats
{
	public class Bunnyhop
	{
		public static void Run()
		{
			while (true)
			{
				if (Main.S.BunnyhopEnabled) // make sure the cheats enabled in the menu
				{
					if (Tools.HoldingKey("0x20")) // while holding space
					{
						// Flags show if you are on the ground or not. 257 is standing on the ground, and 263 is crouching on the ground.
						if ((G.Engine.LocalPlayer.Flags == 257 || G.Engine.LocalPlayer.Flags == 263) && G.Engine.LocalPlayer.Velocity > 10)
						{
							G.Engine.Jump();
						}
					}
				}
				Thread.Sleep(1); // reduce cpu usage again
			}
		}
	}
}
