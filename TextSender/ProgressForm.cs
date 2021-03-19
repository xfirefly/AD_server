using System;
using System.Windows.Forms;

namespace ProgressTest
{
    /// <summary>
    /// Simple progress form.
    /// </summary>
    public partial class ProgressForm : Form
    {
        //BackgroundWorker worker;
        private int lastPercent;

        private string lastStatus;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();

            DefaultStatusText = "Please wait...";
            CancellingText = "Cancelling operation...";
            progressBar.Minimum = progressBar.Value = 0;

            //worker = new BackgroundWorker();
            //worker.WorkerReportsProgress = true;
            //worker.WorkerSupportsCancellation = true;
            //worker.DoWork += new System.ComponentModel.DoWorkEventHandler(worker_DoWork);
            //worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        public ProgressForm(int max)
                    : this()
        {
            progressBar.Maximum = max;
        }

        /// <summary>
        /// Text displayed once the Cancel button is clicked.
        /// </summary>
        public string CancellingText { get; set; }

        /// <summary>
        /// Default status text.
        /// </summary>
        public string DefaultStatusText { get; set; }

        /// <summary>
        /// Gets the progress bar so it is possible to customize it
        /// before displaying the form.
        /// Do not use it directly from the background worker function!
        /// </summary>
        public ProgressBar ProgressBar { get { return progressBar; } }
        /// <summary>
        /// Changes the progress bar value only.
        /// </summary>
        /// <param name="percent">New value for the progress bar.</param>
        public void SetProgress(int percent)
        {
            //do not update the progress bar if the value didn't change
            if (percent != lastPercent)
            {
                lastPercent = percent;
                progressBar.Value = percent;
                //worker.ReportProgress(percent);
            }

            Console.WriteLine("SetProgress  " + progressBar.Value + " " + progressBar.Maximum);
            if (progressBar.Value == progressBar.Maximum)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //notify the background worker we want to cancel
            //worker.CancelAsync();
            //disable the cancel button and change the status text
            buttonCancel.Enabled = false;
            labelStatus.Text = CancellingText;
            Close();
        }

        private void progressBar_Click(object sender, EventArgs e)
        {
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            //reset to defaults just in case the user wants to reuse the form
            buttonCancel.Enabled = true;
            progressBar.Value = progressBar.Minimum;
            labelStatus.Text = DefaultStatusText;
            lastStatus = DefaultStatusText;
            lastPercent = progressBar.Minimum;
            //start the background worker as soon as the form is loaded
            //worker.RunWorkerAsync(Argument);
        }
    }
}