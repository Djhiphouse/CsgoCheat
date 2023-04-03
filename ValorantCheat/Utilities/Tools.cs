using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ZBase.Classes;

namespace ZBase.Utilities
{
	public class Tools
	{
		public static IntPtr handle = Memoryy.FindWindow(null, "Counter-Strike: Global Offensive - Direct3D 9");
		public static void InitializeGlobals()
		{
			new Thread(() =>
			{
				Thread.CurrentThread.IsBackground = true;
				while (true)
				{
					#region Misc
					RECT rect;
					Memoryy.GetWindowRect(handle, out rect);
					/* this is for windowed mode, will have checks in the future
                    rect.top += 26;
                    rect.left += 2;*/
					Main.ScreenSize = RectToSize(rect);
					Main.MidScreen = new Vector2(Main.ScreenSize.Width / 2, Main.ScreenSize.Height / 2);
					Main.ScreenRect = rect;
					#endregion

					// Get Players
					var oldEntityList = new List<Entity>();
					oldEntityList.Clear();
					for (int i = 1; i <= 64; i++)
					{
						Entity ent = new Entity(GetEntityBase(i));
						if (ent.Valid)
							oldEntityList.Add(ent);
					}
					G.EntityList = oldEntityList;
					Thread.Sleep(1000);
				}
			}).Start();
		}

		public static Vector2 CalcAngle(Vector3 src, Vector3 dst)
		{
			Vector2 vAngle;
			Vector3 delta = new Vector3((src.X - dst.X), (src.Y - dst.Y), (src.Z - dst.Z));
			double hyp = Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);

			vAngle.X = (float)(Math.Atan(delta.Z / hyp) * 57.295779513082f);
			vAngle.Y = (float)(Math.Atan(delta.Y / delta.X) * 57.295779513082f);

			if (delta.X >= 0.0)
				vAngle.Y += 180.0f;

			return vAngle;
		}

		public static Vector2 WorldToScreen(Vector3 target)
		{
			Vector2 _worldToScreenPos;
			Vector3 to;
			float w = 0.0f;
			float[] viewmatrix = new float[16];
			viewmatrix = G.Engine.ViewMatrix;

			to.X = viewmatrix[0] * target.X + viewmatrix[1] * target.Y + viewmatrix[2] * target.Z + viewmatrix[3];
			to.Y = viewmatrix[4] * target.X + viewmatrix[5] * target.Y + viewmatrix[6] * target.Z + viewmatrix[7];

			w = viewmatrix[12] * target.X + viewmatrix[13] * target.Y + viewmatrix[14] * target.Z + viewmatrix[15];

			// behind us
			if (w < 0.01f)
				return new Vector2(0, 0);

			to.X *= (1.0f / w);
			to.Y *= (1.0f / w);

			int width = Main.ScreenSize.Width;
			int height = Main.ScreenSize.Height;

			float x = width / 2;
			float y = height / 2;

			x += 0.5f * to.X * width + 0.5f;
			y -= 0.5f * to.Y * height + 0.5f;

			to.X = x;
			to.Y = y;

			_worldToScreenPos.X = to.X;
			_worldToScreenPos.Y = to.Y;
			return _worldToScreenPos;
		}

		public static int GetEntityBase(int PlayerLoopID)
		{
			return Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwEntityList + (PlayerLoopID * 0x10));
		}

		public static int GetEntityBaseFromCrosshair(int CrosshairID)
		{
			return Memoryy.ReadMemory<int>((int)Memoryy.Client + Main.O.signatures.dwEntityList + (CrosshairID - 1) * 0x10);
		}

		// for a health esp gradient
		public static Color HealthGradient(float Percent) //percent hp
		{
			if (Percent < 0 || Percent > 1) { return Color.Black; }

			int Red, Green;
			if (Percent < 0.5)
			{
				Red = 255;
				Green = (int)(255 * Percent);
			}
			else
			{
				Green = 255;
				Red = 255 - (int)(255 * (Percent - 0.5) / 0.5);
			}

			return Color.FromArgb(Red, Green, 0);
		}

		public static float HealthToPercent(int Health)
		{
			return Health / 100f;
		}

		public static float IntegerToFloat(int Value)
		{
			return Value / 255f;
		}

		public static bool HoldingKey(String keyid)
		{


			if ((Memoryy.GetAsyncKeyState(Convert.ToInt32(keyid, 16)) & 0x8000) > 0)
				return true;
			else
				return false;
		}
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out POINT lpPoint);

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public static implicit operator Vector2(POINT point)
			{
				return new Vector2(point.X, point.Y);
			}
		}
		public static void MoveCursor(Vector2 position)
		{
			// mabye move to closest player's 2d head pos? :]
			float X = (position.X - (Main.ScreenSize.Width) / 2) / 2;
			float Y = (position.Y - (Main.ScreenSize.Height) / 2) / 2;
			Memoryy.mouse_event(Mouse.MOUSEEVENTF_ABSOLUTE | Mouse.MOUSEEVENTF_MOVE, (uint)X, (uint)Y, 0, 0);
		}
		public static void SmoothMove(Vector2 position, int steps, int delay)
		{
			// Calculate the distance to be moved
			float distanceX = position.X - Cursor.Position.X;
			float distanceY = position.Y - Cursor.Position.Y;
			float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

			// Calculate the step size for each iteration
			float stepX = distanceX / steps;
			float stepY = distanceY / steps;

			// Move the cursor gradually in small steps
			for (int i = 0; i < steps; i++)
			{
				float deltaX = stepX * (i + 1);
				float deltaY = stepY * (i + 1);
				float ratio = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY) / distance;
				float newX = Cursor.Position.X + deltaX;
				float newY = Cursor.Position.Y + deltaY;
				Memoryy.mouse_event(Mouse.MOUSEEVENTF_ABSOLUTE | Mouse.MOUSEEVENTF_MOVE, (uint)newX, (uint)newY, 0, 0);
				Thread.Sleep((int)(delay * ratio));
			}

			// Move the cursor to the final position
			Memoryy.mouse_event(Mouse.MOUSEEVENTF_ABSOLUTE | Mouse.MOUSEEVENTF_MOVE, (uint)position.X, (uint)position.Y, 0, 0);
		}



		public static bool IsNullVector2(Vector2 vector)
		{
			if (vector == new Vector2(0, 0))
				return true;
			return false;
		}

		public Entity GetFovPlayer(int FOV)
		{
			Entity e = null;
			foreach (Entity Player in G.EntityList)
			{
				if (!Player.IsTeammate)
				{
					Vector2 vector = WorldToScreen(Player.HeadPosition);
					if (!IsNullVector2(vector))
					{
						float dist = Vector2.Distance(Main.MidScreen, vector);
						if (dist < FOV && e == null)
						{
							e = Player;
							//MoveCursor(new Vector2(e.Position.X, e.Position.Y));
						}
						else if (dist < FOV)
						{
							Vector2 vector3 = WorldToScreen(e.HeadPosition);
							Vector2 vector4;
							vector4 = new Vector2(vector3.X, vector3.Y);
							if (Vector2.Distance(Main.MidScreen, vector4) > dist)
							{
								e = Player;

								MoveCursor(WorldToScreen(new Vector3(e.Position.X, e.HeadPosition.Y, e.Position.Z)));
							}
						}

					}
				}

			}
			return e;
		}

		public static bool InScreenPos(float x, float y)
		{
			if (x < Main.ScreenSize.Width && x >= 0 && y < Main.ScreenSize.Height && y >= 0)
				return true;
			else
				return false;
		}
		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(int vKey);
		public static string GetPressedHotKey(Label Keysafed, Label RealKey)
		{
			bool Pressed = false;
			Thread.Sleep(1000);
			string hex = "21";
			while (!Pressed)
			{
				for (int i = 0; i < 256; i++)
				{
					if (GetAsyncKeyState(i) != 0)
					{

						char c = (char)i;
						int code = (int)c;
						hex = code.ToString("X");
						Keysafed.Text = $"Key: '{c}'";
						RealKey.Text = "0x" + hex;
						Pressed = true;
					}
				}
			}

			return "0x" + hex;
		}

		public static Size RectToSize(RECT rect)
		{
			return new Size(rect.right - rect.left, rect.bottom - rect.top);
		}
	}
}
