﻿using System.Threading;
using ZBase.Utilities;

namespace ZBase.Cheats
{
	public class TriggerBot
	{
		public static void Run()
		{
			while (true)
			{
				if (Main.S.TriggerBot) // make sure the cheats enabled in the menu
				{
					if (Tools.HoldingKey(Keys.VK_SHIFT)) // while holding space
					{
						// Flags show if you are on the ground or not. 257 is standing on the ground, and 263 is crouching on the ground.
						if (Main.S.OnlyTriggerAtSniper)
						{
							if (G.Engine.LocalPlayer.WeaponName == "AWP" && G.Engine.LocalPlayer.WeaponName == "SCAR-20")
							{
								if (G.Engine.LocalPlayer.TriggerisTriggerd())
								{
									Thread.Sleep(1);
									G.Engine.ForceAttack();
								}
								else
								{
									//MessageBox.Show("false");
								}
							}
						}
						else
						{
							if (G.Engine.LocalPlayer.TriggerisTriggerd())
							{
								Thread.Sleep(5);
								G.Engine.ForceAttack();
							}
							else
							{
								//MessageBox.Show("false");
							}
						}



					}
				}
				Thread.Sleep(1); // reduce cpu usage again
			}
		}
	}
}
