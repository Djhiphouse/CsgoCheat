using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValorantCheat.Cheats;
using ZBase;
using ZBase.Cheats;
using ZBase.Utilities;
using Color = System.Drawing.Color;

namespace ValorantCheat
{
	public partial class Form1 : Form

	{

		public System.Drawing.Color ESPColor = System.Drawing.Color.AliceBlue;
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr CreateRoundRectRgn
		(
			int nLeftRect,     // x-coordinate of upper-left corner
			int nTopRect,      // y-coordinate of upper-left corner
			int nRightRect,    // x-coordinate of lower-right corner
			int nBottomRect,   // y-coordinate of lower-right corner
			int nWidthEllipse, // height of ellipse
			int nHeightEllipse // width of ellipse
		);

		public Form1()
		{

			InitializeComponent();
			//this.ShowInTaskbar = false;
			this.FormBorderStyle = FormBorderStyle.None;
			Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			SmothnessSlider.Visible = false;
			NameFadein.Start();
			if (Main.RunStartup())
			{
				//timer1.Start();
				OffsetUpdater.UpdateOffsets();
				#region Start Threads
				// found the process and everything, lets start our cheats in a new thread
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					CheckMenu();
				}).Start();

				Tools.InitializeGlobals();

				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					Bunnyhop.Run();
				}).Start();

				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					Visuals v = new Visuals(ColorSelcterRGB, TracerColorRGB);
					v.Run();
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					//ThirdPerson.Run();
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					Aimbot v = new Aimbot(FovBar, HotKeyAimbot);
					v.Run();
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					DrawFOV v = new DrawFOV(FovBar);
					v.Run();
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					DrawCrosshair v = new DrawCrosshair();
					v.Run();
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					TriggerBot.Run(HotKeyTriggerBot);
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					Radar RadarHack = new Radar();
					RadarHack.Run();
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					NoRecoil.Run();
				}).Start();
				new Thread(() =>
				{
					HeadposESP ESP = new HeadposESP();
					Thread.CurrentThread.IsBackground = true;
					ESP.Run();
				}).Start();
				new Thread(() =>
				{
					Thread.CurrentThread.IsBackground = true;
					FakeLag.Run();
				}).Start();

				#endregion
			}
		}
		public void CheckMenu()
		{
			Size Formsize = new Size(this.Width, this.Height);
			//Size Panelsize = new Size(guna2TabControl1.Size.Width, guna2TabControl1.Size.Height);
			bool show = true;
			// Here we make the main variables equal to what our menu checkboxes say
			while (true)
			{
				Main.S.BunnyhopEnabled = BunnyHop.Checked;
				Main.S.ESP = ESPCheck.Checked;
				//Main.S.ThirdPerson = ThirdPersoncheck.Checked;
				Main.S.Aimbot = AimbotCheck.Checked;
				Main.S.FOV = FovCheck.Checked;
				Main.S.Crosshair = CrosshairCheck.Checked;
				Main.S.TriggerBot = TriggerBotCheck.Checked;
				Main.S.EspName = ESPNameCheck.Checked;
				Main.S.TriggerFreeforall = FreeforallCheck.Checked;
				Main.S.NoRecoil = NoRecoilCheck.Checked;
				Main.S.Head = HeadPos.Checked;
				Main.S.Body = BodyPos.Checked;
				Main.S.tracers = Tracer.Checked;
				Main.S.HeadposESP = HeadPosesp.Checked;
				Main.S.TeammateCheck = Teammatecheck.Checked;
				Main.S.Snaplines = Tracer.Checked;
				Main.S.HealthCheck = HeathbarCheck.Checked;
				Main.S.FakeLag = Fakelag.Checked;
				Main.S.OnlyTriggerAtSniper = OnlySniperCheck.Checked;
				Main.S.AimLockOption = AimbotCheck.Checked;
				Main.S.DisableOnSniperAimbotCheck = DsiableonSniper.Checked;
				Main.S.SkeletESP = SkelettESP.Checked;
				//FOV ENABLE
				//Main.S.FovViewChanger = RadarCheck.Checked;
				//
				Main.S.RadarHack = RadarCheck.Checked;
				CheckForIllegalCrossThreadCalls = false;

				if ((Memoryy.GetAsyncKeyState(System.Windows.Forms.Keys.Insert) & 1) > 0)
				{
					if (show)
					{
						this.Size = new Size(1, 1);
						this.WindowState = FormWindowState.Normal;
						show = false;
					}
					else
					{
						this.Size = Formsize;
						this.WindowState = FormWindowState.Minimized;
						show = true;

					}

				}


				Thread.Sleep(50); // Greatly reduces cpu usage
			}
		}
		private void gunaButton3_Click(object sender, EventArgs e)
		{

		}

		private void gunaButton1_Click(object sender, EventArgs e)
		{
			VisualsPage.Hide();
			SettingsPage.Hide();
			AimbotPage.Show();

		}

		private void gunaButton2_Click(object sender, EventArgs e)
		{
			AimbotPage.Hide();
			SettingsPage.Hide();
			VisualsPage.Show();

		}

		private void gunaButton4_Click(object sender, EventArgs e)
		{
			AimbotPage.Hide();
			VisualsPage.Hide();
			SettingsPage.Show();
		}

		private void gunaCheckBox14_CheckedChanged(object sender, EventArgs e)
		{
			if (gunaCheckBox14.Checked)
			{
				this.Opacity = 0.5;
			}

			else
			{
				this.Opacity = 1;
			}
		}

		private void gunaCheckBox15_CheckedChanged(object sender, EventArgs e)
		{
			if (gunaCheckBox15.Checked)
			{
				this.TopMost = true;
			}

			else
			{
				this.TopMost = false;
			}
		}

		private void gunaButton7_Click(object sender, EventArgs e)
		{
			Form2 credits = new Form2();
			credits.Show();
		}

		private void gunaButton5_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void gunaButton3_Click_1(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void gunaPictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.Activate();
			this.BringToFront();
		}

		private void siticoneSlider3_Scroll(object sender, EventArgs e)
		{

		}

		private void gunaCheckBox3_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void VisualsPage_Paint(object sender, PaintEventArgs e)
		{

		}

		private void ESPCheck_CheckedChanged(object sender, EventArgs e)
		{
			SkinWithESP.Visible = ESPCheck.Checked;
		}

		private void HeathbarCheck_CheckedChanged(object sender, EventArgs e)
		{
			HealthBarPrereview.Visible = HeathbarCheck.Checked;
		}

		private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
		{

		}

		private void ColorBar_MouseMove(object sender, MouseEventArgs e)
		{
			Bitmap bitmap = (Bitmap)ColorBar.Image;
			Color color = bitmap.GetPixel(e.X, e.Y);
			SelectedColor.BackColor = color;
			SkinWithESP.BorderColor = SelectedColor.BackColor;
		}

		private void ColorBar_MouseDown(object sender, MouseEventArgs e)
		{
			SkinWithESP.BorderColor = SelectedColor.BackColor;
			ColorSelcterRGB.BackColor = SelectedColor.BackColor;
		}

		private void ColorBar_DoubleClick(object sender, EventArgs e)
		{

		}

		private void TracerColorRGB_MouseDoubleClick(object sender, MouseEventArgs e)
		{

		}

		private void ColorPicker2_Click(object sender, EventArgs e)
		{
			//TracerColorRGB.BackColor = SelectedColor.BackColor;
		}

		private void ColorPicker2_MouseMove(object sender, MouseEventArgs e)
		{
			Bitmap bitmap = (Bitmap)ColorPicker2.Image;
			Color color = bitmap.GetPixel(e.X, e.Y);
			ColorPickerSelecter2.BackColor = color;
			PrereviewColorTracer.BackColor = color;

		}

		private void ColorPicker2_MouseDown(object sender, MouseEventArgs e)
		{
			TracerColorRGB.BackColor = ColorPickerSelecter2.BackColor;
			//MessageBox.Show("Updated to Backcolor: " + TracerColorRGB.BackColor);
		}

		public String ShopNameCredit = "Blacksite Store     ";
		public int curNums = 0;
		public bool fadeback = false;
		private void NameFadein_Tick(object sender, EventArgs e)
		{
			if (fadeback)
			{

			}
			ShopName.Text += ShopNameCredit[curNums];
			curNums++;
			if (curNums == ShopNameCredit.Length)
			{

				ShopName.Text = "";
				curNums = 0;
			}


		}

		private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void AimbotHotkey_Click(object sender, EventArgs e)
		{
			KeysafeAimbot.Text = "Waiting for keyPress";
			Task.Run(() => { Tools.GetPressedHotKey(KeysafeAimbot, HotKeyAimbot); });

		}

		private void gunaButton7_Click_1(object sender, EventArgs e)
		{
			TriggerbotHotKeyPess.Text = "Waiting for keyPress";
			Task.Run(() => { Tools.GetPressedHotKey(TriggerbotHotKeyPess, HotKeyTriggerBot); });
		}
	}
}
