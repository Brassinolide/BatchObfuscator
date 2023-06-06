using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BatchProtect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string SelectFile(){
            string path = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "bat file (*.bat)|*.bat";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
            }

            return path;
        }

        private string ShowSaveFileDialog()
        {
            string path = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "bat file (*.bat)|*.bat";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                path = sfd.FileName.ToString();
            }

            return path;
        }

        private string filepath;
        private string outfilepath;
        private void button1_Click(object sender, EventArgs e)
        {
            filepath = SelectFile();
            if (filepath == "") return;
            outfilepath = filepath.Substring(0, filepath.LastIndexOf(".bat")) + "-obf.bat";
            textBox2.Text = outfilepath;
            textBox1.Text = filepath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filepath))
            {
                MessageBox.Show("No file found");
                return;
            }

            string text = File.ReadAllText(filepath);

            string pattern = @"(-----BEGIN CERTIFICATE-----.*?-----END CERTIFICATE-----)";

            Match match = Regex.Match(text, pattern, RegexOptions.Singleline);
            string cert = match.Groups[1].Value;

            text = Regex.Replace(text, pattern, "");

            text = Obfuscator.TrimSpace(text);
            text = Obfuscator.RemoveCommentary(text);

            Tuple<string, string> data = Obfuscator.SubstringEncode(text);
            text = data.Item2;  //Item2为混淆后的内容
            text = data.Item1 + text;   //Item1为key

            text = Obfuscator.ControlFlow(text);

            //text = Obfuscator.RandomVariableName(Obfuscator.RandomSubroutineName(text));

            text = text + "\r\n" + cert;

            File.WriteAllText(outfilepath, text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            outfilepath = ShowSaveFileDialog();
            textBox2.Text = outfilepath;
        }
    }
}
