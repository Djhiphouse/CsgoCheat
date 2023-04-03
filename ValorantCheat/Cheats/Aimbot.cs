using GameOverlay.Drawing;
using GameOverlay.Windows;
using Siticone.Desktop.UI.WinForms;

using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using ZBase.Classes;
using ZBase.Utilities;
using Color = System.Drawing.Color;

namespace ZBase.Cheats
{
	public class Aimbot
	{
		public float maxAngle = 360f; // maximaler Winkel des Sichtfelds
		public float maxDistance = 1000f; // maximale Entfernung des Sichtfelds
		SiticoneTrackBar FOV;
		#region things
		private readonly GraphicsWindow _window;
		public Label HotKeyAimbot;
		//public Guna2ComboBox KeyPressedHotkey;

		public Aimbot(SiticoneTrackBar FOVValue, Label Hotkey)
		{
			HotKeyAimbot = Hotkey;
			FOV = FOVValue;
			// initialize a new Graphics object
			// GraphicsWindow will do the remaining initialization
			var graphics = new GameOverlay.Drawing.Graphics
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
			Keys pressedkey = new Keys();
			var gfx = e.Graphics;
			gfx.ClearScene();
			float nearestDistance = float.MaxValue;
			float minDistance = float.MaxValue;
			//DrawTextWithOutline("Cheat made by DJ HIP HOUSE#2002", 10, 5, 15, Color.DarkRed, Color.Black, false, true);
			//DrawText("https://discord.gg/w9dSyF4Dt7", 1710, 5, 15, Color.Chocolate, false, true);
			if (Main.S.Aimbot)
			{
				if (HotKeyAimbot.Text == "")
					return;

				//	MessageBox.Show("Aimbot");
				if (Main.S.DisableOnSniperAimbotCheck)
				{
					if (G.Engine.LocalPlayer.WeaponName == "AWP" || G.Engine.LocalPlayer.WeaponName == "SCAR-20" || G.Engine.LocalPlayer.WeaponName.Contains("SSG"))
						return;
				}



				foreach (Entity Player in G.EntityList)
				{//&& !Player.IsTeammate



					if (Player.EntityBase != G.Engine.LocalPlayer.EntityBase && !Player.IsLocalPlayer)
					{

						Vector2 Player2DPos = Tools.WorldToScreen(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z - 5));
						Vector2 Player2DHeadPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z + 10));
						Vector2 Player2DHeadPosAim = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z));
						Vector2 Player2DBodyPosAim = Tools.WorldToScreen(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z));
						Vector2 CrosshairPos = new Vector2((int)1920 / 2, (int)1080 / 2);
						if (Main.S.Head)
						{
							if (!Tools.IsNullVector2(Player2DPos) && !Tools.IsNullVector2(Player2DHeadPos) && Player.Valid)
							{

								if (G.Engine.LocalPlayer.WeaponName == "AWP" || G.Engine.LocalPlayer.WeaponName == "SCAR-20")
									maxDistance = FOV.Value + 100;
								else
									maxDistance = FOV.Value;

								PointF direction = new PointF(Player2DPos.X - CrosshairPos.X, Player2DPos.Y - CrosshairPos.Y);
								float angle = (float)(Math.Atan2(direction.Y, direction.X) * 180.0 / Math.PI);

								if (angle < 0.0f)
								{
									angle += 360.0f;
								}

								if (angle <= maxAngle)
								{
									float distance = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

									if (distance <= maxDistance)
									{
										if (Main.S.TeammateCheck)
										{
											if (!Player.IsTeammate)
											{

												if (Tools.HoldingKey(HotKeyAimbot.Text)) // while holding space
													Tools.MoveCursor(Player2DHeadPosAim);
											}

										}
										else
										{
											if (Tools.HoldingKey(HotKeyAimbot.Text)) // while holding space
												Tools.MoveCursor(Player2DHeadPosAim);
										}

									}
								}


							}

						}
						else if (Main.S.Body)
						{
							maxDistance = FOV.Value;
							PointF direction = new PointF(Player2DPos.X - CrosshairPos.X, Player2DPos.Y - CrosshairPos.Y);
							float angle = (float)(Math.Atan2(direction.Y, direction.X) * 180.0 / Math.PI);

							if (angle < 0.0f)
							{
								angle += 360.0f;
							}

							if (angle <= maxAngle)
							{
								float distance = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

								if (distance <= maxDistance)
								{
									float BoxHeight = Player2DPos.Y - Player2DHeadPos.Y;

									Vector2 BodyPosAimming = Tools.WorldToScreen(Player.GetBonePosition(4));
									if (Main.S.TeammateCheck)
									{
										if (!Player.IsTeammate)
										{
											if (Tools.HoldingKey(HotKeyAimbot.Text)) // while holding space
												Tools.MoveCursor(BodyPosAimming);
										}

									}
									else
									{
										if (Tools.HoldingKey(HotKeyAimbot.Text)) // while holding space
											Tools.MoveCursor(BodyPosAimming);
									}

								}
							}
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
				gfx.OutlineRectangle(GetBrushColor(Color.FromArgb(0, 0, 0)), GetBrushColor(color), x, y, x + width, y + height, thiccness);
			}

			void DrawRoundedBox(float x, float y, float width, float height, float radius, Color color, float thiccness = 2.0f)
			{
				gfx.DrawRoundedRectangle(GetBrushColor(color), x, y, x + width, y + height, radius, thiccness);
			}

			GameOverlay.Drawing.SolidBrush GetBrushColor(Color color)
			{
				return gfx.CreateSolidBrush(color.R, color.G, color.B, color.A);
			}
			#endregion
		}
	}
}
