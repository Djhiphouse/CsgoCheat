using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Numerics;
using ZBase;
using ZBase.Classes;
using ZBase.Utilities;

namespace ValorantCheat.Cheats
{
	public class HeadposESP
	{

		#region things
		public GraphicsWindow _window;

		public HeadposESP()
		{
			// initialize a new Graphics object
			// GraphicsWindow will do the remaining initialization
			var graphics = new Graphics
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true,
				UseMultiThreadedFactories = false,
				VSync = true,
				WindowHandle = IntPtr.Zero
			};

			// it is important to set the window to visible (and topmost) if you want to see it!
			_window = new StickyWindow(Memoryy.Process.MainWindowHandle, graphics)
			{
				IsTopmost = true,
				IsVisible = true,
				FPS = 144,
				X = Main.ScreenRect.left,
				Y = Main.ScreenRect.top,
				Width = Main.ScreenSize.Width,
				Height = Main.ScreenSize.Height
			};

			_window.DrawGraphics += _window_DrawGraphics;
		}

		public void Run()
		{
			// creates the window and setups the graphics
			_window.StartThread();
		}
		private const float StartRadius = 20.0f; // Anfangsradius des Sphären-Objekts
		private const float MinRadius = 1.0f; // Minimaler Radius des Sphären-Objekts
		private const float MaxRadius = 10.0f; // Maximaler Radius des Sphären-Objekts
		private const float DistanceThreshold = 200.0f; // Schwellenwert für die Entfernung zum Spieler
		private float _currentRadius = StartRadius;
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		#endregion
		private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			var gfx = e.Graphics;
			gfx.ClearScene();

			//DrawTextWithOutline("Cheat made by DJ HIP HOUSE#2002", 10, 5, 15, Color.DarkRed, Color.Black, false, true);
			//	DrawText("https://discord.gg/w9dSyF4Dt7", 1710, 5, 15, Color.Chocolate, false, true);
			if (Main.S.HeadposESP)
			{
				int RadiusOfHaad = 0;
				float LocalPlayerPos = 0;
				foreach (Entity Player in G.EntityList)
				{
					if (Main.S.TeammateCheck)
						if (Player.IsTeammate)
							return;


					if (Player.EntityBase != G.Engine.LocalPlayer.EntityBase)
					{

						Vector2 Player2DHeadPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z + 10));
						if (!Tools.IsNullVector2(Player2DHeadPos) && Player.Valid)
						{
							float distance = Player.Distance;



							double maxSize = 12;
							double minSize = 4;
							double size = maxSize - ((distance - 1) / (400 - 1)) * (maxSize - minSize);
							if (size < minSize) size = minSize;
							if (size > maxSize) size = maxSize;
							//Player.Glow(System.Drawing.Color.AliceBlue);
							DrawCircle(Player2DHeadPos.X, Player2DHeadPos.Y + (int)size * 2, (int)size, Color.Green, 1);

						}
					}
				}
			}


			#region drawing functions

			void DrawBoxEdge(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
			{
				gfx.DrawRectangleEdges(GetBrushColor(color), x, y, x + width, y + height, thiccness);
			}

			void DrawText(string text, float x, float y, int size, Color color, bool bold = false, bool italic = false)
			{
				if (Tools.InScreenPos(x, y))
				{
					gfx.DrawText(gfx.CreateFont("Arial", size, bold, italic), GetBrushColor(color), x, y, text);
				}
			}

			void DrawTextWithOutline(string text, float x, float y, int size, Color color, Color outlinecolor, bool bold = true, bool italic = false)
			{
				DrawText(text, x - 1, y + 1, size, outlinecolor, bold, italic);
				DrawText(text, x + 1, y + 1, size, outlinecolor, bold, italic);
				DrawText(text, x, y, size, color, bold, italic);
			}

			void DrawTextWithBackground(string text, float x, float y, int size, Color color, Color backcolor, bool bold = false, bool italic = false)
			{
				if (Tools.InScreenPos(x, y))
				{
					gfx.DrawTextWithBackground(gfx.CreateFont("Arial", size, bold, italic), GetBrushColor(color), GetBrushColor(backcolor), x, y, text);
				}
			}

			void DrawLine(float fromx, float fromy, float tox, float toy, Color color, float thiccness = 2.0f)
			{
				gfx.DrawLine(GetBrushColor(color), fromx, fromy, tox, toy, thiccness);
			}

			void DrawFilledBox(float x, float y, float width, float height, Color color)
			{
				gfx.FillRectangle(GetBrushColor(color), x, y, x + width, y + height);
			}

			void DrawCircle(float x, float y, float radius, Color color, float thiccness = 1)
			{
				gfx.DrawCircle(GetBrushColor(color), x, y, radius, thiccness);
			}

			void DrawCrosshair(CrosshairStyle style, float x, float y, float size, float thiccness, Color color)
			{
				gfx.DrawCrosshair(GetBrushColor(color), x, y, size, thiccness, style);
			}

			void DrawFillOutlineBox(float x, float y, float width, float height, Color color, Color fillcolor, float thiccness = 1.0f)
			{
				gfx.OutlineFillRectangle(GetBrushColor(color), GetBrushColor(fillcolor), x, y, x + width, y + height, thiccness);
			}

			void DrawBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
			{
				gfx.DrawRectangle(GetBrushColor(color), x, y, x + width, y + height, thiccness);
			}

			void DrawOutlineBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
			{
				//gfx.OutlineRectangle(Color.Red, GetBrushColor(color), x, y, x + width, y + height, thiccness);
			}

			void DrawRoundedBox(float x, float y, float width, float height, float radius, Color color, float thiccness = 2.0f)
			{
				gfx.DrawRoundedRectangle(GetBrushColor(color), x, y, x + width, y + height, radius, thiccness);
			}

			SolidBrush GetBrushColor(Color color)
			{
				return gfx.CreateSolidBrush(color.R, color.G, color.B, color.A);
			}
			#endregion
		}
	}
}
