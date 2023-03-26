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
	public class ThirdPerson
	{
		public static void Run()
		{
			while (true)
			{
				if (Main.S.ThirdPerson) // make sure the cheats enabled in the menu
				{
					if ((G.Engine.LocalPlayer.NoStun == 0))
					{
						G.Engine.EnableStun();
					}
					else
					{
						G.Engine.DisableStun();
					}
					// Flags show if you are on the ground or not. 257 is standing on the ground, and 263 is crouching on the ground.
					
					}
				
				Thread.Sleep(1); // reduce cpu usage again
			}
		}
	}
}
