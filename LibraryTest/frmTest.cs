using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HelperMethods;
using HelperMethods.Classes;
using HelpersClasses.Commands.Jira;

// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedVariable
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace LibraryTest
{
    public class Alex
    {
        public int Idade { get; set; }

        public string Nome { get; set; }
    }

    public partial class FrmTest : Form
    {
        public FrmTest() =>
            InitializeComponent();

        private void Button2_Click(object sender, EventArgs e)
        {
            const float s = 1;
            const float b = .875f;

            PnlCor.BackColor = ColorMethods.GetColorFromAhsb(NumberMethods.GetRandomFloat(360), s, b);
            panel1.BackColor = ColorMethods.GetColorFromAhsb(NumberMethods.GetRandomFloat(360), s, b);
            //panel2.BackColor = ColorMethods.GetColorFromAhsb();
            //panel3.BackColor = ColorMethods.GeneratePastelColor()();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e) =>
            label1.Text = BytesMethods.GenerateString((long)numericUpDown1.Value, (int)numericUpDown2.Value);

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e) =>
            label1.Text = BytesMethods.GenerateString((long)numericUpDown1.Value, (int)numericUpDown2.Value);

        private void NumericUpDown3_ValueChanged(object sender, EventArgs e) =>
            label2.Text = BytesMethods.Convert(
                (double)numericUpDown3.Value,
                (FileSizeUnit)numericUpDown4.Value,
                (FileSizeUnit)numericUpDown5.Value).ToString(CultureInfo.InvariantCulture);

        private void TxtMaiuscula_TextChanged(object sender, EventArgs e) => LblMaiuscula.Text = StringMethods.MakeFirstCharWordUpperCase(TxtMaiuscula.Text);

        private void Button1_Click(object sender, EventArgs e)
        {
            //Downloader.GetFileSize("ftp://ftp.rdisoftware.com/kiosk/outgoing/releases/KIOSK_5.17.3-b2-HF17.zip",
            //"avaz", "minhaSenha27");
        }

        private void Button3_Click(object sender, EventArgs e) =>
            SystemMethods.CopyToFolder(@"C:\Users\avaz\Desktop\hey", ConflictAction.Overwrite, Directory.GetFileSystemEntries(@"C:\Users\avaz\Desktop\unzippedRelease").Where(path => !path.ToLower().EndsWith("_sample")).ToArray());

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) =>
            placeholderTextBox1.Enabled = checkBox1.Checked;

        private void Button4_Click(object sender, EventArgs e)
        {
            JiraCommands jira = new JiraCommands("https://jira.rdisoftware.com/jira/", "avaz", "minhaSenha27");
            Ticket ticket = jira.GetTicket("CSO-7206");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            //SystemMethods.Copy(@"C:\KSharp\DeployToNewPos.cmd", @"C:\KSharp", false);
            //File.Delete(@"C:\KSharp\PosData - Copy.zip");
        }
    }
}
