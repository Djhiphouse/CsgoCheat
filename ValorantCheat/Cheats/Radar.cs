using System.Threading;
using ZBase;
using ZBase.Classes;
using ZBase.Utilities;

namespace ValorantCheat.Cheats
{
	internal class Radar
	{


		public void Run()
		{
			//MessageBox.Show("Started");
			while (true)
			{
				if (Main.S.RadarHack)
				{


					foreach (Entity Player in G.EntityList)
					{
						if (Player.EntityBase != G.Engine.LocalPlayer.EntityBase)
						{
							if (Player.Alive && !Player.IsLocalPlayer && !Player.IsTeammate)
							{
								Player.SetonRadar(true);
								//MessageBox.Show("Showing Player");
							}
						}
					}
					Thread.Sleep(1);
				}
			}
		}
	}
}

