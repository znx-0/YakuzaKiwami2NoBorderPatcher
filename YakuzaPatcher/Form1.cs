using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace YakuzaPatcher
{
    public partial class YakuzaKiwami2NoBarsPatcher : Form
    {
        OpenFileDialog openFile = new OpenFileDialog();

        String FileName;
        bool isOK = true;

        public YakuzaKiwami2NoBarsPatcher()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FileName))
            {
                label1.Text = "Error : Please choose ui.par file.";
                label1.ForeColor = Color.Red;
            }
            else
            {
                label1.Text = "Info : Patching ...";
                label1.ForeColor = Color.Blue;

                Patch();

                if (isOK)
                {
                    label1.Text = "Info : Patching Complete";
                    label1.ForeColor = Color.Blue;

                    label1.Text = "Info : Please Copy generated ui.par file to game folder (\\Yakuza Kiwami 2 \\ data)";
                    label1.ForeColor = Color.Blue;

                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (openFile.FileName.Contains("ui.par"))
                {
                    FileName = openFile.FileName;
                    label1.Text = "Info : "  + FileName;
                    label1.ForeColor = Color.Blue;
                }
                else
                {
                    label1.Text = "Error : Please choose ui.par file only.";
                    label1.ForeColor = Color.Red;
                }
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        public void Patch()
        {
            if(!String.IsNullOrEmpty(FileName))
            { 
                String command = "-d -f -s \"" + FileName + "\" nobars-v_1_4 ui.par";
                ProcessStartInfo procStartInfo = new ProcessStartInfo("xdelta3.exe", command);

                procStartInfo.RedirectStandardError = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;

                // wrap IDisposable into using (in order to release hProcess) 
                using (Process process = new Process())
                {
                    process.StartInfo = procStartInfo;
                    process.Start();

                    // Add this: wait until process does its work
                    process.WaitForExit();

                    // and only then read the result
                    string result = process.StandardError.ReadToEnd();
                    if(!String.IsNullOrEmpty(result))
                    {
                        isOK = false;
                        label1.Text = result;
                        label1.ForeColor = Color.Red;
                    }
                    else
                    {
                        isOK = true;
                    }
                }
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/zypnyx/YakuzaKiwami2NoBorderPatcher");

        }
    }
}
