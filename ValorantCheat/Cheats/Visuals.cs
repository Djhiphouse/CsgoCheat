using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Numerics;
using System.Windows.Forms;
using ZBase.Classes;
using ZBase.Utilities;
using Color = System.Drawing.Color;

namespace ZBase.Cheats
{
	public class Visuals
	{
		#region things
		private readonly GraphicsWindow _window;
		public readonly Panel ESPColorRGB;
		public readonly Panel TracerColor;
		public Visuals(Panel rgbESP, Panel Tracer)
		{
			TracerColor = Tracer;
			ESPColorRGB = rgbESP;
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

			DrawTextWithOutline("Blacksite Store", 10, 5, 15, Color.DarkRed, Color.Black, false, true);
			DrawText("https://discord.gg/blacksite", 1710, 5, 15, Color.Chocolate, false, true);

			foreach (Entity Player in G.EntityList)
			{
				if (Player.EntityBase != G.Engine.LocalPlayer.EntityBase)
				{
					//Skelett
					Vector2 head = Tools.WorldToScreen(Player.GetBonePosition(8));
					Vector2 Stomach = Tools.WorldToScreen(Player.GetBonePosition(7));
					Vector2 Hip = Tools.WorldToScreen(Player.GetBonePosition(4));
					Vector2 HipDown = Tools.WorldToScreen(Player.GetBonePosition(3));
					//Shouldeer right
					Vector2 ShoulderRight = Tools.WorldToScreen(Player.GetBonePosition(10));

					//Shoulder left
					Vector2 ShoulderLeft = Tools.WorldToScreen(Player.GetBonePosition(38));

					//armRight
					Vector2 ArmRight = Tools.WorldToScreen(Player.GetBonePosition(36));

					//ArmLeft
					Vector2 ArmLeft = Tools.WorldToScreen(Player.GetBonePosition(39));

					//LeftLeg
					Vector2 LeftLeg = Tools.WorldToScreen(Player.GetBonePosition(72));
					Vector2 LeftLeg2 = Tools.WorldToScreen(Player.GetBonePosition(77));
					Vector2 LeftLeg3 = Tools.WorldToScreen(Player.GetBonePosition(78));
					Vector2 LeftLeg4 = Tools.WorldToScreen(Player.GetBonePosition(73));
					Vector2 LeftLeg5 = Tools.WorldToScreen(Player.GetBonePosition(74));

					//Right Leg
					Vector2 RightLeg = Tools.WorldToScreen(Player.GetBonePosition(65));
					Vector2 RightLeg2 = Tools.WorldToScreen(Player.GetBonePosition(70));
					Vector2 RightLeg3 = Tools.WorldToScreen(Player.GetBonePosition(71));
					Vector2 RightLeg4 = Tools.WorldToScreen(Player.GetBonePosition(66));
					Vector2 RightLeg5 = Tools.WorldToScreen(Player.GetBonePosition(67));

					Vector2 Player2DPos = Tools.WorldToScreen(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z - 5));
					Vector2 Player2DHeadPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z + 10));
					if (!Tools.IsNullVector2(Player2DPos) && !Tools.IsNullVector2(Player2DHeadPos) && Player.Valid)
					{
						float BoxHeight = Player2DPos.Y - Player2DHeadPos.Y;
						float BoxWidth = (BoxHeight / 2) * 1.25f; //little bit wider box

						Color drawcolor;
						if (Player.IsTeammate)
						{
							drawcolor = Color.Blue;


						}
						else
							drawcolor = ESPColorRGB.BackColor;


						#region Box
						//if (Main.S.EspName)
						//DrawTextWithOutline(Player.WeaponName, Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y - 14, 15, Color.AliceBlue, Color.Aqua, false, true);
						if (Main.S.SkeletESP)
						{


							if (!Player.IsTeammate && Main.S.EspName)
							{

								//UppderBody
								DrawLine(head.X, head.Y, Stomach.X, Stomach.Y, Color.AliceBlue);
								DrawLine(Stomach.X, Stomach.Y, HipDown.X, HipDown.Y, Color.AliceBlue);
								DrawLine(Stomach.X, Stomach.Y, ShoulderLeft.X, ShoulderLeft.Y, Color.AliceBlue);
								DrawLine(Stomach.X, Stomach.Y, ShoulderRight.X, ShoulderRight.Y, Color.AliceBlue);

								//arms
								DrawLine(ShoulderRight.X, ShoulderRight.Y, ArmRight.X, ArmRight.Y, Color.AliceBlue);
								DrawLine(ShoulderLeft.X, ShoulderLeft.Y, ArmLeft.X, ArmLeft.Y, Color.AliceBlue);

								//Leg left
								DrawLine(HipDown.X, HipDown.Y, LeftLeg.X, LeftLeg.Y, Color.AliceBlue);
								DrawLine(LeftLeg.X, LeftLeg.Y, LeftLeg2.X, LeftLeg2.Y, Color.AliceBlue);
								DrawLine(LeftLeg2.X, LeftLeg2.Y, LeftLeg3.X, LeftLeg3.Y, Color.AliceBlue);
								DrawLine(LeftLeg3.X, LeftLeg3.Y, LeftLeg4.X, LeftLeg4.Y, Color.AliceBlue);
								DrawLine(LeftLeg4.X, LeftLeg4.Y, LeftLeg5.X, LeftLeg5.Y, Color.AliceBlue);

								//Leg right
								DrawLine(HipDown.X, HipDown.Y, RightLeg.X, RightLeg.Y, Color.AliceBlue);
								DrawLine(RightLeg.X, RightLeg.Y, RightLeg2.X, RightLeg2.Y, Color.AliceBlue);
								DrawLine(RightLeg2.X, RightLeg2.Y, RightLeg3.X, RightLeg3.Y, Color.AliceBlue);
								DrawLine(RightLeg3.X, RightLeg3.Y, RightLeg4.X, RightLeg4.Y, Color.AliceBlue);
								DrawLine(RightLeg4.X, RightLeg4.Y, RightLeg5.X, RightLeg5.Y, Color.AliceBlue);
							}
							else if (!Main.S.EspName)
							{
								//UppderBody
								DrawLine(head.X, head.Y, Stomach.X, Stomach.Y, Color.AliceBlue);
								DrawLine(Stomach.X, Stomach.Y, HipDown.X, HipDown.Y, Color.AliceBlue);
								DrawLine(Stomach.X, Stomach.Y, ShoulderLeft.X, ShoulderLeft.Y, Color.AliceBlue);
								DrawLine(Stomach.X, Stomach.Y, ShoulderRight.X, ShoulderRight.Y, Color.AliceBlue);

								//arms
								DrawLine(ShoulderRight.X, ShoulderRight.Y, ArmRight.X, ArmRight.Y, Color.AliceBlue);
								DrawLine(ShoulderLeft.X, ShoulderLeft.Y, ArmLeft.X, ArmLeft.Y, Color.AliceBlue);

								//Leg left
								DrawLine(HipDown.X, HipDown.Y, LeftLeg.X, LeftLeg.Y, Color.AliceBlue);
								DrawLine(LeftLeg.X, LeftLeg.Y, LeftLeg2.X, LeftLeg2.Y, Color.AliceBlue);
								DrawLine(LeftLeg2.X, LeftLeg2.Y, LeftLeg3.X, LeftLeg3.Y, Color.AliceBlue);
								DrawLine(LeftLeg3.X, LeftLeg3.Y, LeftLeg4.X, LeftLeg4.Y, Color.AliceBlue);
								DrawLine(LeftLeg4.X, LeftLeg4.Y, LeftLeg5.X, LeftLeg5.Y, Color.AliceBlue);

								//Leg right
								DrawLine(HipDown.X, HipDown.Y, RightLeg.X, RightLeg.Y, Color.AliceBlue);
								DrawLine(RightLeg.X, RightLeg.Y, RightLeg2.X, RightLeg2.Y, Color.AliceBlue);
								DrawLine(RightLeg2.X, RightLeg2.Y, RightLeg3.X, RightLeg3.Y, Color.AliceBlue);
								DrawLine(RightLeg3.X, RightLeg3.Y, RightLeg4.X, RightLeg4.Y, Color.AliceBlue);
								DrawLine(RightLeg4.X, RightLeg4.Y, RightLeg5.X, RightLeg5.Y, Color.AliceBlue);
							}
						}

						if (Main.S.ESP)
						{
							if (!Player.IsTeammate && Main.S.EspName)
							{
								DrawOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor);

							}
							else if (!Main.S.EspName)
							{
								DrawOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor);

							}
						}




						//DrawFillOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, Color.FromArgb(50, 198, 198, 198));
						//DrawBoxEdge(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, 1);
						#endregion
						#region Health Bar
						if (Main.S.HealthCheck)
						{
							float Health = Player.Health;
							Color HealthColor = Tools.HealthGradient(Tools.HealthToPercent((int)Health));
							float x = Player2DPos.X - (BoxWidth / 2) - 8;
							float y = Player2DHeadPos.Y;
							float w = 4;
							float h = BoxHeight;
							float HealthHeight = (Health * h) / 100;
							if (!Player.IsTeammate && Main.S.EspName)
							{
								DrawBox(x, y, w, h, Color.Black, 1);
								DrawFilledBox(x + 1, y + 1, 2, HealthHeight - 1, HealthColor);

							}
							else if (!Main.S.EspName)
							{
								DrawBox(x, y, w, h, Color.Black, 1);
								DrawFilledBox(x + 1, y + 1, 2, HealthHeight - 1, HealthColor);

							}

						}

						#endregion
						#region Snaplines
						if (Main.S.Snaplines)
						{
							if (!Player.IsTeammate && Main.S.EspName)
							{
								DrawLine(Main.MidScreen.X, Main.MidScreen.Y + Main.MidScreen.Y, Player2DPos.X, Player2DPos.Y, TracerColor.BackColor);

							}
							else if (!Main.S.EspName)
							{
								DrawLine(Main.MidScreen.X, Main.MidScreen.Y + Main.MidScreen.Y, Player2DPos.X, Player2DPos.Y, TracerColor.BackColor);

							}
						}

						#endregion
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

			SolidBrush GetBrushColor(Color color)
			{
				return gfx.CreateSolidBrush(color.R, color.G, color.B, color.A);
			}
			#endregion
		}
	}
}