using System;
using System.Drawing;
using System.Numerics;
using System.Text;
using ZBase.Utilities;

namespace ZBase.Classes
{
	public class Entity
	{
		public int EntityBase;

		public Entity(int Base)
		{
			this.EntityBase = Base;
		}

		public int Health
		{
			get
			{
				return Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_iHealth);
			}
		}

		public int GlowIndex
		{
			get
			{
				return Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_iGlowIndex);
			}
		}

		public int Team
		{
			get
			{
				return Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_iTeamNum);
			}
		}

		public int Flags
		{
			get
			{
				return Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_fFlags);
			}
		}
		public string PlayerName
		{
			get
			{
				IntPtr playerNameAddress = Memoryy.ReadMemory<IntPtr>(EntityBase + Main.O.netvars.m_szCustomName);
				byte[] playerNameBytes = new byte[64];



				return Encoding.ASCII.GetString(playerNameBytes).TrimEnd('\0');
			}
		}
		public int NoStun
		{
			get
			{
				return Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.netvars.m_flFlashMaxAlpha);
			}
		}
		public bool TriggerisTriggerd()
		{


			int LocalPlayer = Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwLocalPlayer); //Read the localplayer pointer
			int LocalTeam = Memoryy.ReadMemory<int>((int)LocalPlayer + Main.O.netvars.m_iTeamNum); //Get localplayer team
			int CrosshairID = Memoryy.ReadMemory<int>((int)LocalPlayer + Main.O.netvars.m_iCrosshairId); //Get crosshair ID


			if (CrosshairID > 0 && CrosshairID < 64) //Check if we have a player in our crosshair
			{
				int EntityListPlayer = Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwEntityList + (CrosshairID - 1) * 0x10); //Get the entity of the player in our crosshair
				int crossTeam = Memoryy.ReadMemory<int>((int)EntityListPlayer + Main.O.netvars.m_iTeamNum); //Get players team
				int crossHealth = Memoryy.ReadMemory<int>((int)EntityListPlayer + Main.O.netvars.m_iHealth); //Get health if player
				if (crossHealth > 0)
				{
					if (!Main.S.TriggerFreeforall)
					{
						if (LocalTeam != crossTeam) //Making sure its an enemy, and the player is alive
						{
							return true; //Alive enemy in crosshair
						}
					}
					else
						return true;
				}

			}


			return false;

		}
		public bool IsTeammate
		{
			get
			{
				return Team == G.Engine.LocalPlayer.Team;
			}
		}

		public Vector3 Position
		{
			get
			{
				return Memoryy.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecOrigin);
			}
		}

		public Vector3 EyePosition
		{
			get
			{
				return Position + Memoryy.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecViewOffset);
			}
		}

		public Vector3 HeadPosition
		{
			get
			{
				return GetBonePosition(8);
			}
		}

		public Vector3 GetBonePosition(int BoneID)
		{
			int bonematrix = Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_dwBoneMatrix);
			float x = Memoryy.ReadMemory<float>(bonematrix + 0x30 * BoneID + 0x0C);
			float y = Memoryy.ReadMemory<float>(bonematrix + 0x30 * BoneID + 0x1C);
			float z = Memoryy.ReadMemory<float>(bonematrix + 0x30 * BoneID + 0x2C);
			return new Vector3(x, y, z);
		}

		public bool Spotted
		{
			get
			{
				return Memoryy.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bSpotted);
			}
			set
			{
				Memoryy.WriteMemory<bool>(EntityBase + Main.O.netvars.m_bSpotted, Convert.ToByte(value));
			}
		}

		public bool IsLocalPlayer
		{
			get
			{
				return G.Engine.LocalPlayer.EntityBase == EntityBase;
			}
		}

		public int FlashDuration
		{
			get
			{
				return Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_flFlashDuration);
			}
			set
			{
				Memoryy.WriteMemory<int>(EntityBase + Main.O.netvars.m_flFlashDuration, value);
			}
		}

		public bool Dormant
		{
			get
			{
				return Memoryy.ReadMemory<bool>(EntityBase + Main.O.signatures.m_bDormant);
			}
		}

		public int CrosshairID
		{
			get
			{
				return Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_iCrosshairId);
			}
		}

		public void Glow(Color color)
		{
			GlowStruct GlowObj = new GlowStruct();

			GlowObj = Memoryy.ReadMemory<GlowStruct>(G.Engine.GlowObjectManager + GlowIndex * 0x38);

			GlowObj.r = (float)color.R / 255;
			GlowObj.g = (float)color.G / 255;
			GlowObj.b = (float)color.B / 255;
			GlowObj.a = (float)color.A / 255;
			GlowObj.m_bRenderWhenOccluded = true;
			GlowObj.m_bRenderWhenUnoccluded = false;

			Memoryy.WriteMemory<GlowStruct>(G.Engine.GlowObjectManager + GlowIndex * 0x38, GlowObj);
		}

		public void Cham(Color color)
		{
			// these only show while visible
			Memoryy.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender, color.R);
			Memoryy.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 1, color.G);
			Memoryy.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 2, color.B);
			Memoryy.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 3, color.A);
		}
		public void ResetChams()
		{
			Cham(Color.FromArgb(255, 255, 255, 0));
		}
		public bool Alive
		{
			get
			{
				if (Health > 0 && Health <= 100)
					return true;
				return false;
			}
		}

		public bool Dead
		{
			get
			{
				if (!Alive)
					return true;
				return false;
			}
		}

		public bool IsPlayer
		{
			get
			{
				if (Team == 2 || Team == 3)
					return true;
				else if (Team == 0 || Team == 1)
					return false;
				else
					return false;
			}
		}

		public WeaponID WeaponID
		{
			get
			{
				int weaponbase = Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_hActiveWeapon);
				int yes = weaponbase & 0xFFF;
				int ok = Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwEntityList + (yes - 1) * 16);
				return (WeaponID)Memoryy.ReadMemory<int>(ok + Main.O.netvars.m_iItemDefinitionIndex);
			}
		}

		public string WeaponName
		{
			get
			{
				switch (this.WeaponID)
				{
					case WeaponID.DEAGLE:
						return "Desert Eagle";
					case WeaponID.BERETTAS:
						return "Dual Berettas";
					case WeaponID.FIVESEVEN:
						return "Five-SeveN";
					case WeaponID.GLOCK:
						return "Glock-18";
					case WeaponID.AK47:
						return "AK-47";
					case WeaponID.AUG:
						return "AUG";
					case WeaponID.AWP:
						return "AWP";
					case WeaponID.FAMAS:
						return "FAMAS";
					case WeaponID.G3SG1:
						return "G3SG1";
					case WeaponID.GALILAR:
						return "Galil";
					case WeaponID.M249:
						return "M249";
					case WeaponID.M4A4:
						return "M4A1";
					case WeaponID.MAC10:
						return "MAC-10";
					case WeaponID.P90:
						return "P90";
					case WeaponID.UMP45:
						return "UMP-45";
					case WeaponID.XM1014:
						return "XM1014";
					case WeaponID.BIZON:
						return "PP-Bizon";
					case WeaponID.MAG7:
						return "MAG-7";
					case WeaponID.NEGEV:
						return "Negev";
					case WeaponID.SAWEDOFF:
						return "Sawed-Off";
					case WeaponID.TEC9:
						return "Tec-9";
					case WeaponID.TASER:
						return "Taser";
					case WeaponID.P2000:
						return "P2000";
					case WeaponID.MP7:
						return "MP7";
					case WeaponID.MP9:
						return "MP9";
					case WeaponID.NOVA:
						return "Nova";
					case WeaponID.P250:
						return "P250";
					case WeaponID.MP5SD:
						return "MP5-SD";
					case WeaponID.SCAR20:
						return "SCAR-20";
					case WeaponID.SG556:
						return "SG 553";
					case WeaponID.SSG08:
						return "SSG 08";
					case WeaponID.KNIFE:
						return "Knife";
					case WeaponID.FLASHBANG:
						return "Flashbang";
					case WeaponID.HEGRENADE:
						return "Grenade";
					case WeaponID.SMOKEGRENADE:
						return "Smoke Grenade";
					case WeaponID.MOLOTOV:
						return "Molotov";
					case WeaponID.DECOY:
						return "Decoy";
					case WeaponID.INCGRENADE:
						return "Incendiary Grenade";
					case WeaponID.C4:
						return "C4";
					case WeaponID.M4A1S:
						return "M4A1-S";
					case WeaponID.USPS:
						return "USP-S";
					case WeaponID.CZ75A:
						return "CZ75-Auto";
					case WeaponID.REVOLVER:
						return "R8 Revolver";
					default:
						return "Knife";
				}
			}
		}

		public bool Valid
		{
			get
			{
				if (Dormant)
					return false;
				if (Dead)
					return false;
				if (!IsPlayer)
					return false;
				return true;
			}
		}

		public Vector2 AimPunchAngle
		{
			get
			{
				return Memoryy.ReadMemory<Vector2>(EntityBase + Main.O.netvars.m_aimPunchAngle);
			}
		}
		public int ShotsFired
		{
			get
			{
				return Memoryy.ReadMemory<int>(EntityBase + Main.O.netvars.m_iShotsFired);
			}
		}
		public float Velocity
		{
			get
			{
				Vector3 vel = Memoryy.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecVelocity);
				return (float)Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);
			}
		}
		public void SetonRadar(bool Active)
		{

			Memoryy.WriteMemory<bool>(EntityBase + Main.O.netvars.m_bSpotted, Active);

		}
		public bool Scoped
		{
			get
			{
				return Memoryy.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bIsScoped);
			}
		}

		public float Distance
		{
			get
			{
				return Vector3.Distance(G.Engine.LocalPlayer.Position, Position);
			}
		}
	}
}
