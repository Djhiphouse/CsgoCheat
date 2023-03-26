﻿using Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Classes;
using ZBase.Utilities;

namespace ZBase
{
	internal class Player
	{
		public static Mem Memory = new Mem();
		internal static int ModuleAddress;
		

		public static Mem Memoryreader
			 = new Mem();
		public static Signatures Offsetss = new Signatures();
		public static Netvars Netvar = new Netvars();

		public void MemoryOpen(int pid)
		{
			Memoryreader.OpenProcess(pid);
			SetThirdPersonView();
		}
		internal static int Local => Memoryreader.ReadInt(Memoryy.Client + Offsetss.dwLocalPlayer.ToString());

		

		

		internal static float FlashAlpha => Memoryreader.ReadFloat(Local + Netvar.m_flFlashMaxAlpha.ToString());

		internal static int Flags => Memoryreader.ReadInt(Local + Netvar.m_fFlags.ToString());

		internal static bool IsGround => Flags == 257 || Flags == 263;

		internal static bool IsScoping => Memoryreader.ReadInt(Local + Netvar.m_bIsScoped.ToString()) == 1;



		internal static readonly float DefaultFlashAlpha = 255;

		internal static readonly int DefaultFov = 90;



		#region - Weapon -

		internal static int HandsItemId
		{
			get
			{
				int activeWeapon = Memoryreader.ReadInt(Player.Local + Netvar.m_hActiveWeapon.ToString());

				int weapon = Memoryreader.ReadInt(Local + Offsetss.dwEntityList.ToString() + ((activeWeapon & 0xFFF) - 1) * 0x10);

				int index = Memoryreader.ReadInt(weapon + Netvar.m_iItemDefinitionIndex.ToString());

				return index;
			}
		}

		internal static Item HandsWeapon => (Item)HandsItemId;

		internal static bool HasHandsWeapon => !HasHandsPistol && !HasHandsGrenade && !HasHandsC4;

		internal static bool HasHandsPistol =>
			HandsItemId == (int)Pistol.WEAPON_CZ75A || HandsItemId == (int)Pistol.WEAPON_DEAGLE ||
			HandsItemId == (int)Pistol.WEAPON_FIVESEVEN || HandsItemId == (int)Pistol.WEAPON_GLOCK ||
			HandsItemId == (int)Pistol.WEAPON_HKP2000 || HandsItemId == (int)Pistol.WEAPON_P250 ||
			HandsItemId == (int)Pistol.WEAPON_REVOLVER || HandsItemId == (int)Pistol.WEAPON_TEC9 ||
			HandsItemId == (int)Pistol.WEAPON_USP_SILENCER || HandsItemId == (int)Pistol.WEAPON_DUAL_BERRETS;

		internal static bool HasHandsGrenade =>
			HandsItemId == (int)Grenade.WEAPON_DECOY || HandsItemId == (int)Grenade.WEAPON_FLASHBANG ||
			HandsItemId == (int)Grenade.WEAPON_HEGRENADE || HandsItemId == (int)Grenade.WEAPON_INCGRENADE ||
			HandsItemId == (int)Grenade.WEAPON_MOLOTOV || HandsItemId == (int)Grenade.WEAPON_SMOKEGRENADE;

		internal static bool HasHandsC4 => HandsItemId == (int)Other.WEAPON_C4;

		internal static bool HasHandsKnife =>
			HandsItemId == (int)Knife.WEAPON_KNIFE || HandsItemId == (int)Knife.WEAPON_KNIFE_BAYONET ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_BUTTERFLY || HandsItemId == (int)Knife.WEAPON_KNIFE_FALCHION ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_FLIP || HandsItemId == (int)Knife.WEAPON_KNIFE_GUT ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_KARAMBIT || HandsItemId == (int)Knife.WEAPON_KNIFE_M9_BAYONET ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_PUSH || HandsItemId == (int)Knife.WEAPON_KNIFE_T ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_TACTICAL;

		#endregion



		internal static void SetFlashAlpha(float value) => Memoryreader.WriteMemory(Local + Netvar.m_flFlashMaxAlpha.ToString(), "float", value.ToString());

		

		internal static void SetThirdPersonView() => Memoryreader.WriteMemory(Netvar.m_iObserverMode.ToString() + Local,"int", "1");

		

		internal enum Item : int
		{
			WEAPON_DEAGLE = 1,
			WEAPON_ELITE = 2,
			WEAPON_FIVESEVEN = 3,
			WEAPON_GLOCK = 4,
			WEAPON_AK47 = 7,
			WEAPON_AUG = 8,
			WEAPON_AWP = 9,
			WEAPON_FAMAS = 10,
			WEAPON_G3SG1 = 11,
			WEAPON_GALILAR = 13,
			WEAPON_M249 = 14,
			WEAPON_M4A1 = 16,
			WEAPON_MAC10 = 17,
			WEAPON_P90 = 19,
			WEAPON_UMP45 = 24,
			WEAPON_XM1014 = 25,
			WEAPON_BIZON = 26,
			WEAPON_MAG7 = 27,
			WEAPON_NEGEV = 28,
			WEAPON_SAWEDOFF = 29,
			WEAPON_TASER = 31,
			WEAPON_HKP2000 = 32,
			WEAPON_MP7 = 33,
			WEAPON_MP9 = 34,
			WEAPON_NOVA = 35,
			WEAPON_P250 = 36,
			WEAPON_SCAR20 = 38,
			WEAPON_SG556 = 39,
			WEAPON_SSG08 = 40,
			WEAPON_KNIFE = 42,
			WEAPON_FLASHBANG = 43,
			WEAPON_HEGRENADE = 44,
			WEAPON_SMOKEGRENADE = 45,
			WEAPON_MOLOTOV = 46,
			WEAPON_DECOY = 47,
			WEAPON_INCGRENADE = 48,
			WEAPON_C4 = 49,
			WEAPON_KNIFE_T = 59,
			WEAPON_M4A1_SILENCER = 60,
			WEAPON_CZ75A = 63,
			WEAPON_REVOLVER = 64,
			WEAPON_KNIFE_BAYONET = 500,
			WEAPON_KNIFE_FLIP = 505,
			WEAPON_KNIFE_GUT = 506,
			WEAPON_KNIFE_KARAMBIT = 507,
			WEAPON_KNIFE_M9_BAYONET = 508,
			WEAPON_KNIFE_TACTICAL = 509,
			WEAPON_KNIFE_FALCHION = 512,
			WEAPON_KNIFE_BUTTERFLY = 515,
			WEAPON_KNIFE_PUSH = 516,
			WEAPON_TEC9 = 262174,
			WEAPON_USP_SILENCER = 262205
		}

		internal enum Weapon : int
		{
			WEAPON_AK47 = 7,
			WEAPON_AUG = 8,
			WEAPON_AWP = 9,
			WEAPON_FAMAS = 10,
			WEAPON_G3SG1 = 11,
			WEAPON_GALILAR = 13,
			WEAPON_M249 = 14,
			WEAPON_M4A1 = 16,
			WEAPON_MAC10 = 17,
			WEAPON_P90 = 19,
			WEAPON_UMP45 = 24,
			WEAPON_XM1014 = 25,
			WEAPON_BIZON = 26,
			WEAPON_MAG7 = 27,
			WEAPON_NEGEV = 28,
			WEAPON_SAWEDOFF = 29,
			WEAPON_MP7 = 33,
			WEAPON_MP9 = 34,
			WEAPON_NOVA = 35,
			WEAPON_SCAR20 = 38,
			WEAPON_SG556 = 39,
			WEAPON_SSG08 = 40,
			WEAPON_M4A1_SILENCER = 60
		}

		internal enum Pistol : int
		{
			WEAPON_DEAGLE = 1,
			WEAPON_DUAL_BERRETS = 2,
			WEAPON_FIVESEVEN = 3,
			WEAPON_GLOCK = 4,
			WEAPON_HKP2000 = 32,
			WEAPON_P250 = 36,
			WEAPON_CZ75A = 63,
			WEAPON_REVOLVER = 64,
			WEAPON_TEC9 = 262174,
			WEAPON_USP_SILENCER = 262205
		}

		internal enum Grenade : int
		{
			WEAPON_FLASHBANG = 43,
			WEAPON_HEGRENADE = 44,
			WEAPON_SMOKEGRENADE = 45,
			WEAPON_MOLOTOV = 46,
			WEAPON_DECOY = 47,
			WEAPON_INCGRENADE = 48
		}

		internal enum Knife : int
		{
			WEAPON_KNIFE = 42,
			WEAPON_KNIFE_T = 59,
			WEAPON_KNIFE_BAYONET = 500,
			WEAPON_KNIFE_FLIP = 505,
			WEAPON_KNIFE_GUT = 506,
			WEAPON_KNIFE_KARAMBIT = 507,
			WEAPON_KNIFE_M9_BAYONET = 508,
			WEAPON_KNIFE_TACTICAL = 509,
			WEAPON_KNIFE_FALCHION = 512,
			WEAPON_KNIFE_BUTTERFLY = 515,
			WEAPON_KNIFE_PUSH = 516
		}

		internal enum Other : int
		{
			WEAPON_C4 = 49,
			WEAPON_TASER = 31
		}
	}
}
