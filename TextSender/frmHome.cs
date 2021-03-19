using Common;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmHome : MaterialForm
    {
        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;
        private const int WM_NCHITTEST = 0x84;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        // ID for the About item on the system menu
        private int SYSMENU_ABOUT_ID = 0x1;

        public frmHome()
        {
            LanguageHelper.SetLang(Profile.getLang(), this, typeof(frmHome));

            InitializeComponent();
            panel1.Visible = false;

            Text += ((decimal)App.VERSION / 10).ToString("0.0");

            if (Profile.networkMode() == Profile.NetworkMode.LAN)
            {
                rbLan.Checked = true;
            }
            else
            {
                rbWan.Checked = true;
            }

            if (Profile.boxResolution() == Profile.BoxResolution.R1280_720)
            {
                rb1280.Checked = true;
                LocationCalc.setScreen(1280, 720);
            }
            else
            {
                rb1920.Checked = true;
                LocationCalc.setScreen(1920, 1080);
            }

            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            // materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);

            frmMain progEdit = new frmMain();
            tbcHome.TabPages[0].Controls.Add(progEdit);
            progEdit.Dock = DockStyle.Fill;

            ProgramRelease progRel = new ProgramRelease();
            // ucProgSend.Size = tbcHome.TabPages[1].Size;
            tbcHome.TabPages[1].Controls.Add(progRel);
            progRel.Dock = DockStyle.Fill;

            BgTemplate bgTemplate = new BgTemplate(tbcHome, progEdit );
            // ucProgSend.Size = tbcHome.TabPages[1].Size;
            tbcHome.TabPages[2].Controls.Add(bgTemplate);
            bgTemplate.Dock = DockStyle.Fill;

            BoxCtrl boxCtrl = new BoxCtrl();
            // ucProgSend.Size = tbcHome.TabPages[1].Size;
            tbcHome.TabPages[3].Controls.Add(boxCtrl);
            boxCtrl.Dock = DockStyle.Fill;
             
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            // MaximizeWindow(true);
            rtbReadme.LoadFile("readme.rtf");
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                //Console.WriteLine("msg + " + relativePoint );
                if (PointToClient(Cursor.Position).Y > 60)  //禁止拖拉窗口调整大小
                {
                    return;
                }
            }

            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {
                App.MsgBox("Delete data and restore to default");

                Close();
                try
                {
                    File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "screen.jpg"));
                    File.Delete(App.ZipPath);
                    Directory.Delete(App.SceneDir, true);
                    Directory.Delete(App.TmpDir, true);
                    Directory.Delete(App.ScrshotDir, true);
                    Directory.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), true);
                }
                catch (Exception)
                {
                }
                App.restart();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "Restore…");
        }

        private readonly MaterialSkinManager materialSkinManager;
        private int colorSchemeIndex;

        private void btTheme_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 3) colorSchemeIndex = 0;

            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;

                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;

                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;

                case 3:
                    Random ran = new Random();
                    int p = ran.Next(5, (int)Primary.BlueGrey500);
                    int a = ran.Next(0, (int)Accent.DeepOrange400);

                    materialSkinManager.ColorScheme = new ColorScheme((Primary)p, (Primary)(p + 2),
                        (Primary)(p - 4), (Accent)a, TextShade.WHITE);
                    break;
            }
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            // button1.BackColor = MaterialSkinManager.Instance.ColorScheme.AccentColor;
        }

        private void frmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btLang_Click(object sender, EventArgs e)
        {
            if (Profile.getLang().CompareTo("zh") == 0)
            {
                Profile.setLang("en");
            }
            else
            {
                Profile.setLang("zh");
            }

            Close();
            App.restart();
        }

 

        private void tbcHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage current = tbcHome.SelectedTab;
            IAdUserControl uc = current.Controls[0] as IAdUserControl;
            if (uc != null)
            {
                uc.OnActivate();
            }
        }

        private void rbWan_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    Profile.networkMode(Profile.NetworkMode.WAN);
                    App.Instance.stopBroadcast();
                }
            }
        }

        private void rbLan_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    Profile.networkMode(Profile.NetworkMode.LAN);
                    App.Instance.startBroadcast();
                }
            }
        }

        private void rb1280_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    LocationCalc.setScreen(1280, 720);
                    Profile.boxResolution(Profile.BoxResolution.R1280_720);
                }
            }
        }

        private void rb1920_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    LocationCalc.setScreen(1920, 1080);
                    Profile.boxResolution(Profile.BoxResolution.R1920_1080);
                }
            }
        }
    }

    public class LanguageHelper
    {

        #region SetAllLang
        /// <summary>
        /// Set language
        /// </summary>
        /// <param name="lang">language:zh-CN, en-US</param>
        private static void SetAllLang(string lang)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            Form frm = null;

            string name = "MainForm";

            frm = (Form)System.Reflection.Assembly.Load("CameraTest").CreateInstance(name);

            if (frm != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();
                resources.ApplyResources(frm, "$this");
                AppLang(frm, resources);
            }
        }
        #endregion

        #region SetLang
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lang">language:zh-CN, en-US</param>
        /// <param name="form">the form you need to set</param>
        /// <param name="formType">the type of the form </param>
        public static void SetLang(string lang, Form form, Type formType)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            if (form != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(formType);
                resources.ApplyResources(form, "$this");
                AppLang(form, resources);
            }
        }
        #endregion

        #region AppLang for control
        /// <summary>
        ///  loop set the propery of the control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="resources"></param>
        private static void AppLang(Control control, System.ComponentModel.ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        AppLang(c, resources);
                    }
                }
            }

            foreach (Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                AppLang(c, resources);
            }
        }
        #endregion

        #region AppLang for menuitem
        /// <summary>
        /// set the toolscript 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="resources"></param>
        private static void AppLang(ToolStripMenuItem item, System.ComponentModel.ComponentResourceManager resources)
        {
            if (item is ToolStripMenuItem)
            {
                resources.ApplyResources(item, item.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems)
                    {
                        AppLang(c, resources);
                    }
                }
            }
        }
        #endregion
    }
}