using Common;
using System;
using System.Windows.Forms;

namespace TextSender
{
    public partial class SetSceneTiming : UserControl
    {
        private Log log;

        public SetSceneTiming()
        {
            log = new Log(GetType());
            InitializeComponent();
        }

        private string sceneName;
        private string sceneTiming;

        public string SceneName
        {
            get
            {
                return sceneName;
            }

            set
            {
                sceneName = value;
                name.Text = sceneName;
            }
        }

        public string SceneTiming
        {
            get
            {
                return sceneTiming;
            }

            set
            {
                sceneTiming = value;
                timing.Text = sceneTiming;
            }
        }

        private void setup_Click(object sender, EventArgs e)
        {
            frmSetTime frm = new frmSetTime(frmSetTime.Type.TimeOnly, SceneTiming);
            frm.timeChanged += sceneTimeChanged;
            frm.ShowDialog();
        }

        private void sceneTimeChanged(object sender, frmSetTime.TimeChangedEventArgs e)
        {
            SceneTiming = e.Value;
            log.e("set SceneTiming :" + SceneTiming);
        }
    }
}