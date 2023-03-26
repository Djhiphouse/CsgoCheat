using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZBase.Classes;
using ZBase.Utilities;

namespace ZBase.Cheats
{
	public class FastShoot
	{
		public static void Run()
		{
			while (true)
			{
				if (Main.S.FastShoot) // make sure the cheats enabled in the menu
				{
					if (Tools.HoldingKey(Keys.VK_SHIFT)) // while holding space
					{
						// Flags show if you are on the ground or not. 257 is standing on the ground, and 263 is crouching on the ground.
						
							G.Engine.ForceAttack();
						
					}
				}
				Thread.Sleep(65); // reduce cpu usage again
			}
		}
	}
}
