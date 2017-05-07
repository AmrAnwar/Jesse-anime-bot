
#region lib
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech;
using System.Speech.Recognition;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics; // Process
using System.IO; // StreamWriter

#endregion

namespace WindowsFormsApplication15
{
    public partial class jesse : Form
    {

        #region Personal qualities

        string name = "test01";
        int age = 20;

        #endregion

        #region arguments
        bool flag_pic = false;
        List<string> text = new List<string>();
        int index = 0; 
        bool flag = true, flagg = true;
        string last;
        PromptBuilder pBuilder = new PromptBuilder();
        SpeechRecognitionEngine sRecognize = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechSynthesizer voice = new SpeechSynthesizer();
        #endregion
        public jesse()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region test
            //a.SelectVoiceByHints(VoiceGender.Male);
            //a.SpeakAsync("HELLO");
            //voice.SelectVoiceByHints(VoiceGender.Male,VoiceAge.Teen);
            //voice.SpeakAsync(textBox1.Text);

            //////////
            #endregion
            voice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Teen);
            voice.SpeakAsync(textBox1.Text);
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            pictureBox1.Visible = false;
            pictureBox2.Enabled = true;
            pictureBox2.Visible = true;

            #region dictonary words 
            Choices sList = new Choices();
            sList.Add(new string[] {"Microsoft","scope15","memory","test","My Document", "notepad","google search", "My Computer",
               "Who is","is", "Einstein", "youtube","facebook",
                "What is","nice to meet you", "your name is jesse","nice","what is your name",
                  "today", "i",  "fine", "exit form", "code", "so" });
            Grammar gr = new Grammar(new GrammarBuilder(sList));

            #endregion
            //DictationGrammar gr = new DictationGrammar();

            try
            {
                #region recording

                sRecognize.RequestRecognizerUpdate();

                sRecognize.LoadGrammar(gr);
                sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;

                sRecognize.SetInputToDefaultAudioDevice();
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);
                sRecognize.Recognize();
                #endregion
                //
            }

            catch
            {
                return;
            }


        }
        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            pictureBox3.Enabled = false;
            pictureBox3.Visible = false;
            pictureBox2.Enabled = true;
            pictureBox2.Visible = true;

            exit(e);
            string s = e.Result.Text;
            textBox2.Text = s;
            string[] words = s.Split(' ');
            #region test
            //if (flagg)
            //{

            //        last = s;
            //    flagg = false;

            //}

            //if (s != last)
            //    {
            //        flag = true;
            //        last = s;
            //    }
            //    else
            //{
            //    flag = false;
            //}
            #endregion
            foreach (string word in words)
                {
                #region check word input

                if (word == "what")
                    {
                        if (e.Result.Text == "what is your name")
                        {
                            voice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Teen);
                            string ss = string.Format("my name is {0}", name);
                            voice.SpeakAsync(ss);

                        }
                    }
                os_operation(e.Result.Text);

                if (word == "your")
                    {
                        if (e.Result.Text == "your name is jesse")
                        {
                            name = words[words.Length - 1];
                            voice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Teen);
                            string ss = string.Format("ok now my name is {0}", name);
                            voice.SpeakAsync(ss);

                        }
                    }
                    if (e.Result.Text == "nice to meet you")
                    {
                        voice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Teen);

                        string ss = "nice to meet you too";
                        voice.SpeakAsync(ss);
                    }
                #endregion
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sRecognize.RecognizeAsyncStop();
            
        }
        private void exit(SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "exit form")
            {
                Application.Exit();
            }
            else
            {
                textBox1.Text = textBox1.Text + " " + e.Result.Text.ToString();
            }
        }

        #region text-boxes
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            
            text.Add(name);
            string s = "is";
            if ( s != name )
            {

                try
                {
                    for (int i = text.Count - 2; i >= 0; i--)
                    {
                        #region scriping
                        if (text[i] == "What is" || text[i] == "Who is")
                        {
                            pictureBox3.Enabled = true;
                            pictureBox3.Visible = true;

                            pictureBox2.Enabled = false;
                            pictureBox2.Visible = false;

                            

                            textBox4.Text = name;
                            //
                            #region python code
                            String prg = string.Format(@"import sys
import wikipedia
z = ""abcdefghigklmnobqrstuvwxyz""
s = wikipedia.summary(""{0}"", sentences = 1)
s = s.split()
w = """"
for i in s :
    for j in i :
        if j.lower() in z:
                            w += j
    w += "" ""
print w", name);
                            #endregion
                            #region wikipedia part
                            StreamWriter sw = new StreamWriter("amt.py");
                            sw.Write(prg); // write this program to a file
                            sw.Close();
                            Process p = new Process(); // create process (i.e., the python program
                            p.StartInfo.FileName = "python.exe";
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.UseShellExecute = false; // make sure we can read the output from stdout
                            p.StartInfo.Arguments = "amt.py";// + a + " " + b; // start the python program with two parameters
                            p.Start(); // start the process (the python program)
                            StreamReader ss = p.StandardOutput;
                            String output = ss.ReadToEnd();
                            string[] r = output.Split(new char[] { ' ' }); // get the parameter
                            string web_text = (string.Join(" ", r));

                            textBox4.Text = web_text;
                            //
                            p.WaitForExit();

                            voice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Teen);
                            voice.SpeakAsync(web_text);

                            pictureBox3.Enabled = true;
                            pictureBox3.Visible = true;
                            pictureBox2.Enabled = false;
                            pictureBox2.Visible = false;

                            //
                            text = new List<string>();
                            break;
                            ////////////////////
                            #endregion
                        }
                        #endregion 

                        #region search
                        else if (text[i] == "google search")
                        {
                            textBox4.Text = name;
                            Process.Start("www.google.com.eg/#q=" + textBox4.Text);

                        }

                        #endregion

                        #region memory
                        else if (text[i] == "memory")
                        {
                            textBox4.Text = name;
                            
                            Form2 a = new Form2();
                            a.file_name = name;
                            a.Show();

                           
                        }

                        #endregion
                    }
                }
                catch
                {
                    textBox4.Text = text.Count.ToString();
                }
               

            }
            textBox3.Text = string.Join(" ", text);
        }
        private void os_operation( string s)
        {
            if (s == "My Computer")
            {
                Process.Start("::{20d04fe0-3aea-1069-a2d8-08002b30309d}");
            }
            if (s == "My Document")
            {
                Process.Start("::{450d8fba-ad25-11d0-98a8-0800361b1103}");
            }
            if (s == "notepad")
            {
                Process.Start("notepad.exe");
            }
            if (s == "shutdown")
            {
                Process.Start("shutdown", "/s /t 0");
            }
            if (s == "restart")
            {
                Process.Start("shutdown", "/r /t 0");
            }
        }

        private void textTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void nameLabel_Click(object sender, EventArgs e)
        {

        }

        private void textLabel_Click(object sender, EventArgs e)
        {

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            nameTextBox.Text = textBox2.Text;
            textTextBox.Text = textBox4.Text;
        }

        private void jesseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
