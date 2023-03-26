﻿using GameOverlay.Drawing;
using GameOverlay.Windows;
using Guna.UI.WinForms;
using System;
using ZBase.Utilities;
using Color = System.Drawing.Color;

namespace ZBase.Cheats
{
	public class DrawCrosshair
	{
		#region things
		private readonly GraphicsWindow _window;
		GunaTrackBar guna;
		public DrawCrosshair()
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

		#endregion
		private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			var gfx = e.Graphics;
			gfx.ClearScene();

			//DrawTextWithOutline("Dj Hip House", 10, 5, 25, Color.DeepSkyBlue, Color.Black, true, true);
			if (Main.S.Crosshair)
			{

				//DrawCrosshair(CrosshairStyle.Diagonal,1920/2,1080/2,17,1,Color.Aquamarine);
				DrawText("卐", 1920 / 2 - 10, 1080 / 2 - 11, 20, Color.DarkRed, false, false);
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
				gfx.OutlineRectangle(GetBrushColor(Color.FromArgb(0, 0, 0)), GetBrushColor(color), x, y, x + width, y + height, thiccness);
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