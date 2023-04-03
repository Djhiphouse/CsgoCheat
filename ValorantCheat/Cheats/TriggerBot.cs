using System.Threading;
using System.Windows.Forms;
using ZBase.Utilities;

namespace ZBase.Cheats
{
	public class TriggerBot
	{
		public static void Run(Label HotKey)
		{
			while (true)
			{


				if (Main.S.TriggerBot) // make sure the cheats enabled in the menu
				{

					//MessageBox.Show("KEY: " + HotKey.Text);
					if (Tools.HoldingKey(HotKey.Text)) // while holding space
					{

						// Flags show if you are on the ground or not. 257 is standing on the ground, and 263 is crouching on the ground.
						if (Main.S.OnlyTriggerAtSniper)
						{

							if (G.Engine.LocalPlayer.WeaponName == "AWP" || G.Engine.LocalPlayer.WeaponName == "SCAR-20")
							{
								//	MessageBox.Show("Triggert");
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

								Thread.Sleep(15);
								G.Engine.ForceAttack();
								//MessageBox.Show("KEY: " + HotKey.Text);
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
