using System;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using ZBase.Utilities;

namespace ZBase.Classes
{
	public class Engine
	{
		public int EnginePtr;

		public Engine(int Base)
		{
			EnginePtr = Base;
		}

		public Entity LocalPlayer
		{
			get
			{
				return new Entity(Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwLocalPlayer));
			}
		}

		public int GlowObjectManager
		{
			get
			{
				return Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwGlowObjectManager);
			}
		}

		// viewangles should never have a roll value
		public Vector2 ViewAngles
		{
			get
			{
				Vector3 v3 = Memoryy.ReadMemory<Vector3>(ClientState + Main.O.signatures.dwClientState_ViewAngles);
				return new Vector2(v3.X, v3.Y);
			}
			set
			{
				Memoryy.WriteMemory<Vector3>(ClientState + Main.O.signatures.dwClientState_ViewAngles, new Vector3(value.X, value.Y, 0));
			}
		}

		public float[] ViewMatrix
		{
			get
			{
				float[] temp = new float[16];
				for (int i = 0; i < 16; i++)
					temp[i] = Memoryy.ReadMemory<float>((int)Memoryy.Client + Main.O.signatures.dwViewMatrix + (i * 0x4));
				return temp;
			}
		}

		public int ClientState
		{
			get
			{
				return Memoryy.ReadMemory<int>(EnginePtr + Main.O.signatures.dwClientState);
			}
		}

		public float ModelBrightness
		{
			get
			{
				return Memoryy.ReadMemory<int>(EnginePtr + Main.O.signatures.model_ambient_min);
			}
			set
			{
				int thisPtr = (int)(Memoryy.Engine + Main.O.signatures.model_ambient_min - 0x2c);
				byte[] bytearray = BitConverter.GetBytes(value);
				int intbrightness = BitConverter.ToInt32(bytearray, 0);
				int xored = intbrightness ^ thisPtr;
				Memoryy.WriteMemory<int>(EnginePtr + Main.O.signatures.model_ambient_min, xored);
			}
		}

		public void Shoot()
		{
			Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwForceAttack, 5);
			Thread.Sleep(20);
			Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwForceAttack, 4);
		}

		public void Jump()
		{
			Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwForceJump, 6);
		}
		public void Aimbotinit()
		{
			AimbotScanner aimbot = new AimbotScanner("csgo");

		}
		public void ChangeFOV(int fov)
		{
			Memoryy.WriteMemory<int>(ClientState + Main.O.netvars.m_iFOV, fov);
		}
		public void Aimbottest()
		{



		}
		Vector3 Playerfeet = new Vector3();
		public void UpdateLocalPlayer()
		{


		}
		public void Aimbot()
		{

			int TARGET_BONE = 8;
			var LocalPlayerClient = LocalPlayer;
			var ClientstateClient = ClientState;
			int localplayerteam = Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.netvars.m_iTeamNum);
			float oldDistX = 1111111.0f;
			float oldDistY = 1111111.0f;

			for (int i = 0; i < 32; i++)
			{
				var entity = Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwEntityList + i * 0x10);
				if (entity == 0)
					continue;

				int entityteam = Memoryy.ReadMemory<int>(entity + Main.O.netvars.m_iTeamNum);
				int enitityhealth = Memoryy.ReadMemory<int>((int)entity + Main.O.netvars.m_iHealth);
				bool entityDormant = Memoryy.ReadMemory<bool>(entity + Main.O.signatures.m_bDormant);

				int target = 0;
				int targethealth = 0;
				bool targetDormant = true;
				Vector3 targetpos, localpos;

				if (localplayerteam != entityteam && enitityhealth > 0)
				{
					Vector3 localAngle, entitypos;

					localAngle.X = Memoryy.ReadMemory<float>(ClientstateClient + Main.O.signatures.dwClientState_ViewAngles + 0x0);
					localAngle.Y = Memoryy.ReadMemory<float>(ClientstateClient + Main.O.signatures.dwClientState_ViewAngles + 0x4);
					localAngle.Z = Memoryy.ReadMemory<float>((int)Memoryy.Client + Main.O.signatures.dwLocalPlayer + Main.O.netvars.m_vecViewOffset + 0x0);

					localpos.X = Memoryy.ReadMemory<float>((int)Memoryy.Client + Main.O.signatures.dwLocalPlayer + Main.O.netvars.m_vecOrigin + 0x0);
					localpos.Y = Memoryy.ReadMemory<float>((int)Memoryy.Client + Main.O.signatures.dwLocalPlayer + Main.O.netvars.m_vecOrigin + 0x4);
					localpos.Z = Memoryy.ReadMemory<float>((int)Memoryy.Client + Main.O.signatures.dwLocalPlayer + Main.O.netvars.m_vecOrigin + 0x8 + (int)localAngle.Z);

					var entityBones = Memoryy.ReadMemory<int>(entity + Main.O.netvars.m_dwBoneMatrix);

					entitypos.X = Memoryy.ReadMemory<float>(entityBones + 0x30 * TARGET_BONE + 0x0C);
					entitypos.Y = Memoryy.ReadMemory<float>(entityBones + 0x30 * TARGET_BONE + 0x1C);
					entitypos.Z = Memoryy.ReadMemory<float>(entityBones + 0x30 * TARGET_BONE + 0x2C);

					Vector3 tmp = localpos - entitypos;


				}
			}
		}
		int ShotsFired = 0;
		Vector3 Angle;
		Vector3 AimPunch;
		Vector3 OldAngle;

		public static Vector3 ClampAngle(Vector3 Anglee)
		{
			if (Anglee.Y < 89.0)
				Anglee.Y = 89.0f;

			if (Anglee.Y < -89.0f)
				Anglee.Y = -89.0f;

			while (Anglee.X > 180)
				Anglee.X -= 360;

			while (Anglee.X < -180)
				Anglee.X += 360;

			Anglee.Z = 0;

			return Anglee;
		}

		public void ReduceRecoil()
		{
			ShotsFired = Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.netvars.m_iShotsFired);
			if (ShotsFired > 1)
			{
				Angle = Memoryy.ReadMemory<Vector3>((int)Memoryy.Client + Main.O.netvars.m_aimPunchAngle);
				AimPunch = OldAngle - Angle * 2f;
				ClampAngle(AimPunch);
				Memoryy.WriteMemory<Vector3>((int)Memoryy.Client + Main.O.netvars.m_viewPunchAngle, AimPunch);

			}
			else
			{
				OldAngle = Memoryy.ReadMemory<Vector3>((int)Memoryy.Client + Main.O.netvars.m_aimPunchAngle);
			}
		}

		public void FakeLag(bool Toogle)
		{
			if (Toogle)
				Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwbSendPackets, 0);
			else
				Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwbSendPackets, 1);
		}
		public void ForceAttack()
		{
			Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwForceAttack, 5);
			Thread.Sleep(20);
			Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwForceAttack, 4);
		}
		public void DisableStun()
		{
			Netvars Netvar = new Netvars();

			Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.netvars.m_flFlashMaxAlpha, 0);

		}
		public void EnableStun()
		{
			Netvars Netvar = new Netvars();

			Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.netvars.m_thirdPersonViewAngles, 1);

		}

		public void FovChanger(int FOV)
		{
			MessageBox.Show("Fov: " + Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwClientState_ViewAngles));
			while (true)
			{
				Thread.Sleep(1);

				Memoryy.WriteMemory<int>((int)Memoryy.Engine + Main.O.signatures.dwClientState_ViewAngles, -60);
				Memoryy.WriteMemory<int>((int)Memoryy.Engine + Main.O.signatures.dwClientState_ViewAngles + 0x4, 100);
				Memoryy.WriteMemory<int>((int)Memoryy.Engine + Main.O.signatures.dwClientState_ViewAngles + 0x8, -10);
				//Memoryy.WriteMemory<int>((int)Memoryy.Client + Main.O.signatures.dwClientState_ViewAngles, 90);

			}

		}
	}
}
