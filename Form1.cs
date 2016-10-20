using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using NCalc;

namespace Finance_Calendar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // I add the starting label in the constructor so it will respond to the datetime that I assign to it (the month)      
            Starting = new TextBox();      
            Starting.AcceptsReturn = true;
            Starting.Location = new System.Drawing.Point(15, 57);
            Starting.Name = "Starting";
            helpProvider1.SetHelpNavigator(Starting, System.Windows.Forms.HelpNavigator.Index);
            helpProvider1.SetHelpString(Starting, "Input the amount of you money you have today!");
            helpProvider1.SetShowHelp(Starting, true);
            Starting.Size = new System.Drawing.Size(136, 20);
            Starting.TabIndex = 210;
            this.Controls.Add(Starting);


            DayInfos = new List<DayInfo>();
            CheckedInfos = new List<CheckedInfo>();
            Checked = new List<bool>();
            PostSaveInfos = new List<PostSaveRePopulateInfo>();
            InfoDays = new List<Information>();

            // This bool list is from a bunch of textboxes and is vital in how the save file is created
            for (int i = 0; i < 42; i++)
            {
                Checked.Add(false);
            }

            // The following arrays are declared as such so I can use them in loops throughout the code
            Ins = new TextBox[] {
            add1,
            add2,
            add3,
            add4,
            add5,
            add6,
            add7,
            add8,
            add9,
            add10,
            add11,
            add12,
            add13,
            add14,
            add15,
            add16,
            add17,
            add18,
            add19,
            add20,
            add21,
            add22,
            add23,
            add24,
            add25,
            add26,
            add27,
            add28,
            add29,
            add30,
            add31,
            add32,
            add33,
            add34,
            add35,
            add36,
            add37,
            add38,
            add39,
            add40,
            add41,
            add42,
              };
            Outs = new TextBox[] {
            subtract1,
            subtract2,
            subtract3,
            subtract4,
            subtract5,
            subtract6,
            subtract7,
            subtract8,
            subtract9,
            subtract10,
            subtract11,
            subtract12,
            subtract13,
            subtract14,
            subtract15,
            subtract16,
            subtract17,
            subtract18,
            subtract19,
            subtract20,
            subtract21,
            subtract22,
            subtract23,
            subtract24,
            subtract25,
            subtract26,
            subtract27,
            subtract28,
            subtract29,
            subtract30,
            subtract31,
            subtract32,
            subtract33,
            subtract34,
            subtract35,
            subtract36,
            subtract37,
            subtract38,
            subtract39,
            subtract40,
            subtract41,
            subtract42,
              };

            Notes = new TextBox[] {
            textBox1,
            textBox2,
            textBox3,
            textBox4,
            textBox5,
            textBox6,
            textBox7,
            textBox8,
            textBox9,
            textBox10,
            textBox11,
            textBox12,
            textBox13,
            textBox14,
            textBox15,
            textBox16,
            textBox17,
            textBox18,
            textBox19,
            textBox20,
            textBox21,
            textBox22,
            textBox23,
            textBox24,
            textBox25,
            textBox26,
            textBox27,
            textBox28,
            textBox29,
            textBox30,
            textBox31,
            textBox32,
            textBox33,
            textBox34,
            textBox35,
            textBox36,
            textBox37,
            textBox38,
            textBox39,
            textBox40,
            textBox41,
            textBox42,
              };

            DateLabels = new Label[]
            {
            label1,
            label2,
            label3,
            label4,
            label5,
            label6,
            label7,
            label8,
            label9,
            label10,
            label11,
            label12,
            label13,
            label14,
            label15,
            label16,
            label17,
            label18,
            label19,
            label20,
            label21,
            label22,
            label23,
            label24,
            label25,
            label26,
            label27,
            label28,
            label29,
            label30,
            label31,
            label32,
            label33,
            label101,
            label102,
            label103,
            label104,
            label105,
            label106,
            label107,
            label108,
            label109,
            };
            CheckBoxes = new CheckBox[]
            {
            checkBox1,
            checkBox2,
            checkBox3,
            checkBox4,
            checkBox5,
            checkBox6,
            checkBox7,
            checkBox8,
            checkBox9,
            checkBox10,
            checkBox11,
            checkBox12,
            checkBox13,
            checkBox14,
            checkBox15,
            checkBox16,
            checkBox17,
            checkBox18,
            checkBox19,
            checkBox20,
            checkBox21,
            checkBox22,
            checkBox23,
            checkBox24,
            checkBox25,
            checkBox26,
            checkBox27,
            checkBox28,
            checkBox29,
            checkBox30,
            checkBox31,
            checkBox32,
            checkBox33,
            checkBox34,
            checkBox35,
            checkBox36,
            checkBox37,
            checkBox38,
            checkBox39,
            checkBox40,
            checkBox41,
            checkBox42
            };

            // An error handler to trigger if certain criteria is not met immediately
            for (int i = 0; i < 42; i++)
            {
                Ins[i].TextChanged += new EventHandler(UniqueHandler);
                Outs[i].TextChanged += new EventHandler(UniqueHandler);
            }
        }
        private char[] alpha = " ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public TextBox[] Notes { get; set; }
        public static TextBox[] Ins { get; set; }
        public static TextBox[] Outs { get; set; }
        public CheckBox[] CheckBoxes { get; set; }
        public Label[] DateLabels { get; set; }
        public static List<CheckedInfo> CheckedInfos { get; set; }
        public static List<PostSaveRePopulateInfo> PostSaveInfos { get; set; }
        public static List<DayInfo> DayInfos { get; set; }
        public static List<bool> Checked { get; set; }
        public static List<Information> InfoDays { get; set; }
        public decimal InitialInput = 0;
        public decimal FinalOutput = 0;
        public static DayInfo start = new DayInfo();
        public static TextBox Starting = new TextBox();

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            label50.Text = DateTime.Now.ToString("MMMM");
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 2000;
            toolTip1.ReshowDelay = 1000;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.saveAll, "Saves file based on the day of month that it is saved and any number that has it's checkbox clicked for the day of the week. For example, if you have '100' on the 5th, then when you load the file"
                + " back up again, it will display '100' on the 5th. If you have a checkbox clicked, it will save the day of the week and then when you load it up again, it will show on that day of the week.");
            toolTip1.SetToolTip(this.loadAll, "This will ask you to load a file that has all of your previously saved information on it.");

            // The following methods and loop set the form up in a way that is the path to the least amount of user errors. Also adding elements to the objects I 
            // use in my save files
            AddToDayInfos();
            AddToCheckedInfos();
            MakeNewMonthBold();
            for (int i = 0; i < Ins.Length; i++)
            {
                SetTextBoxValues("0", "0", "0", "", i);
            }
            AddToPostSavedInfos();
            FinalOutput = 0;
        }
        // The next 42 button clicks use the calculate method in such a way that depending on the button that you click, the Output.Text label will show you 
        //the sum of all values contained in the textboxes before it. This is so the user can see how much money they'd have on that day
        private void save1_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(0);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save2_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(1);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save3_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(2);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save4_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(3);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }

        }

        private void save5_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(4);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch
            {
                Output.Text = "Invalid Entry";
            }
        }

        private void save6_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(5);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save7_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(6);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save8_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(7);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save9_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(8);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save10_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(9);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save11_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(10);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save12_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(11);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save13_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(12);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save14_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(13);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save15_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(14);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save16_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(15);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save17_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(16);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save18_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(17);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save19_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(18);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save20_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(19);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save21_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(20);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save22_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(21);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save23_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(22);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save24_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(23);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save25_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(24);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save26_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(25);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save27_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(26);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save28_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(27);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save29_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(28);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save30_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(29);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save31_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(30);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save32_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(31);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save33_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(32);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save34_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(33);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save35_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(34);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save36_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(35);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save37_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(36);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save38_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(37);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save39_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(38);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save40_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(39);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save41_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(40);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void save42_Click(object sender, EventArgs e)
        {
            try
            {
                FinalOutput = Calculate(41);
                Output.Text = FinalOutput.ToString();
                Red();
            }
            catch { Output.Text = "Invalid Entry"; }
        }

        private void UniqueHandler(object sender, EventArgs e)
        {
            TextBox source = (sender as TextBox);
            for (int i = 0; i < Ins.Length; i++)
            {
                if (ExcludeLetters(i) == true)
                {
                    errorProvider1.SetError(Ins[i], String.Empty);
                }
                else
                {
                    errorProvider1.SetError(Ins[i], "Input valid number");
                }
            }
        }

        public void Red()
        {
            if (decimal.Parse(Output.Text) < 0)
            {
                Output.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Output.ForeColor = System.Drawing.Color.Green;
            }
        }
        // This method is used when a file is loaded, and 2 numbers exist on the same day. It puts them in the correct format
        private string conCat(string a, string b)
        {
            if (a == "" || a == "0")
            {
                a = b;
                return a;
            }
            else if (b == "" || b == "0")
            {
                return a;
            }
            else
            {
                a = a + "+" + b;
                return a;
            }
        }
        // Putting the datetime into the format I want, will be set a label in the form load.
        public string Days(int add)
        {
            if (DateTime.Now.AddDays(add).Day == 1 && DateTime.Now.AddDays(add).Month > DateTime.Now.Month)
            {
                return DateTime.Now.AddDays(add).ToString("MMMM") + " 1st";
            }
            else
            {
                string dayOfWeek = DateTime.Now.AddDays(add).DayOfWeek.ToString();
                string whiteSpace;

                if (dayOfWeek.ToUpper() == "monday".ToUpper())
                    whiteSpace = "                         ";
                else if (dayOfWeek.ToUpper() == "tuesday".ToUpper())
                    whiteSpace = "                        ";
                else if (dayOfWeek.ToUpper() == "wednesday".ToUpper())
                    whiteSpace = "                  ";
                else if (dayOfWeek.ToUpper() == "thursday".ToUpper())
                    whiteSpace = "                       ";
                else if (dayOfWeek.ToUpper() == "friday".ToUpper())
                    whiteSpace = "                           ";
                else if (dayOfWeek.ToUpper() == "saturday".ToUpper())
                    whiteSpace = "                        ";
                else
                {
                    whiteSpace = "                          ";
                }
                string date = dayOfWeek + whiteSpace + DateTime.Now.AddDays(add).Day;
                return date;
            }
        }
        // The two following methods ad properties into an object. The first contains properties based off of the day of the month. The second is based off of the 
        // day of the week. This allows the user to save based on the "nth" of the month and the "---day" of the week.
        private void AddToDayInfos()
        {
            for (int i = 0; i < Ins.Length; i++)
            {
                DayInfos.Add(new DayInfo()
                {
                    In = Ins[i],
                    Out = Outs[i],
                    Note = Notes[i],
                    SaveDate = DateTime.Now.AddDays(i)
                });
            }
        }
        private void AddToCheckedInfos()
        {
            for (int i = 0; i < Ins.Length; i++)
            {
                CheckedInfos.Add(new CheckedInfo()
                {
                    InChecked = Ins[i],
                    OutChecked = Outs[i],
                    NoteCheck = Notes[i],
                    SaveDateChecked = DateTime.Now.AddDays(i)
                });
            }
        }
        // This method exists as a reset. The variables in the object are set here as well in case the user decides to cancel or needs to go back
        private void AddToPostSavedInfos()
        {
            if (PostSaveInfos != null)
            {
                PostSaveInfos.Clear();
            }
            for (int i = 0; i < Ins.Length; i++)
            {
                PostSaveInfos.Add(new PostSaveRePopulateInfo()
                {
                    InPost = Ins[i].Text,
                    OutPost = Outs[i].Text,
                    PostNotes = Notes[i].Text

                });
            }
        }
        private void MakeNewMonthBold()
        {
            for (int i = 0; i < 42; i++)
            {
                DateLabels[i].Text = Days(i);

                if (DateTime.Now.AddDays(i).Day == 1 && DateTime.Now.AddDays(i).Month > DateTime.Now.Month)
                {
                    DateLabels[i].Font = new System.Drawing.Font("Lucida Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void SetTextBoxValues(string starting, string ins, string outs, string notes, int i)
        {
            Starting.Text = starting;
            Ins[i].Text = ins;
            Outs[i].Text = outs;
            Notes[i].Text = notes;
        }
        private bool ExcludeLetters(int index)
        {
            for (int j = 0; j < alpha.Length; j++)
            {
                if (string.IsNullOrEmpty(Ins[index].Text) || string.IsNullOrEmpty(Outs[index].Text) || Ins[index].Text.ToUpper().Contains(alpha[j]) || Outs[index].Text.ToUpper().Contains(alpha[j]))
                {
                    return false;
                }
            }

            return true;

        }

        // This is static because it is used in other forms. The expression is a way for me to do math in textboxes and be able to evaluate that and return a value
        // which is especially helpful when I have multiple numbers on the same day. The user simple adds "1 + 1" to the textbox and this evaluates that and 
        // returns "2"
        public static decimal Calculate(int a)
        {
            decimal fin = 0;
            for (int i = 0; i < a + 1; i++)
            {
                try
                {
                    Expression InEx = new Expression(Ins[i].Text);
                    Expression OutEx = new Expression(Outs[i].Text);
                    fin = fin + decimal.Parse(InEx.Evaluate().ToString()) - decimal.Parse(OutEx.Evaluate().ToString());
                }
                catch
                {

                }

            }
            return decimal.Parse(Starting.Text) + fin;
        }

        // This will check the checkboxes on the form when a file is loaded where the checkbox was clicked when it was saved
        private bool CheckBool(List<Information> a, int i)
        {
            if (a[i].InCheckInt.ToString() == "0" && a[i].OutCheckedInt.ToString() == "0" && a[i].CheckedNotes == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // These two mehtods are used to check if the user has made any changes. They are used during events and if returned as true will prompt a save dialog
        private bool ChangesMade(List<PostSaveRePopulateInfo> list, DayInfo item, int a)
        {
            if (list[a].InPost != Ins[a].Text || list[a].OutPost != Outs[a].Text || list[a].PostNotes != Notes[a].Text)
            {
                return true;
            }
            else if (item.StartingValue.ToString() != Starting.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool LookForChanges()
        {
            for (int q = 0; q < 42; q++)
            {
                if (ChangesMade(PostSaveInfos, start, q) == true)
                {
                    DialogResult diag = MessageBox.Show("Would you like to save your changes?", "Before you continue..", MessageBoxButtons.YesNoCancel);
                    if (diag == DialogResult.Yes)
                    {
                        // Call method to save file...
                        Form2 form = new Form2();
                        form.StartPosition = FormStartPosition.CenterParent;
                        SaveNewFile();
                    }
                    else if (diag == DialogResult.No)
                    {
                        AddToPostSavedInfos();
                        //AddToDayInfos();
                        //for (int i = 0; i < 42; i++)
                        //{
                        //    SetTextBoxValues(start.ToString(), DayInfos[i].In.ToString(), DayInfos[i].Out.ToString(), DayInfos[i].Note.Text, i);
                        //}
                    }
                    else
                    {
                        return true;
                    }

                }
                break;
            }
            return false;
        }

        //This method will set the arrays back to their original values after setting them to zero based on checkbox behavior
        private void ReSetDays()
        {
            for (int i = 0; i < 42; i++)
            {
                Ins[i].Text = PostSaveInfos[i].InPost.ToString();
                Outs[i].Text = PostSaveInfos[i].OutPost.ToString();
                Notes[i].Text = PostSaveInfos[i].PostNotes.ToString();
            }
        }
        //This method will call the "Save" method from the DataLayer class. This method takes the values you have put into your 
        // DayInfos and CheckedInfos objects and the Starting value save them into an XML file.
        private void SaveNewFile()
        {
            Form2 form = new Form2();
            if (SaveFile.SaveFileName != null)
            {
                form.TextBox1.Text = SaveFile.SaveFileName;
            }
            form.StartPosition = FormStartPosition.CenterParent;


            start.StartingValue = decimal.Parse(Starting.Text);
            start.StartingDate = DateTime.Now;
            //AddToPostSavedInfos();
            //SetCheckedList();
            DataLayer data = new DataLayer();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                SaveFile.SaveFileName = form.TextBox1.Text;
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Calendar Finance XML\\" + SaveFile.SaveFileName + ".xml"))
                {
                    DialogResult dialogResult = MessageBox.Show("Would you like to overwrite current save file?", "Save File Exists", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        AddToPostSavedInfos();
                        data.Save(DayInfos, Starting.Text, Checked, SaveFile.SaveFileName);
                        //data.SaveStarting(start);                       
                        MessageBox.Show("Your file has been saved.", "File Saved", MessageBoxButtons.OK);
                        ReSetDays();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("File not saved.", "No File Saved", MessageBoxButtons.OK);

                    }
                }
                else
                {
                    AddToPostSavedInfos();
                    data.Save(DayInfos, Starting.Text, Checked, SaveFile.SaveFileName);
                    //data.SaveStarting(start);
                    MessageBox.Show("Your file has been saved.", "File Saved", MessageBoxButtons.OK);
                    ReSetDays();
                }

            }

        }
        //This click event takes you through a series of logic that checks to see if you currently have a file loaded, asks you to name your file and also 
        //tells you if the name you have selected already exists and then saves that XML file to a folder located in your MyDocuments. I wasn't aware that a           
        // save file dialog existed in the framework at this time, but I think I like this better anyways

        private void saveAll_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.StartPosition = FormStartPosition.CenterParent;
            SaveNewFile();
        }
        private void loadAll_Click(object sender, EventArgs e)
        {
            if(LookForChanges() == true)
            {
                return;
            }
            ClearForm(true);
            DataLayer data = new DataLayer();
            InfoDays = data.Load();
            if (InfoDays == null)
            {
                ReSetDays();
            }
            //The following logic is for the Checkednfo list. Because the list is meant to reflect the day of week that you saved it against as well as the frequency which the value occurs on
            //that day of week, I had to check use the day of year property. The logic below looks at the saved DayofYear value of today and matches it with the DayofYear value saved in the
            //CheckedInfos list. It then will loop through the list 42 times to populate every textbox on the form starting with the element of the CheckedInfos list that matches the the first
            //value in the form. For example. If the DayOfYear value of today is 150 and the 20th element of the CheckedInfos list has the DayOfYear value of 150 then the list will start at the
            //20th value and continue up to 41, go to 0 and then continue to 19 until all of the elements of the list are put in their place. I have also accounted for the situation where there
            //is not matching DayOfYear element in the CheckedInfos array and if the DayOfYear value of today is lower than any in the saved CheckedInfos list (like during any day in January).               
            try
            {
                for (int i = 0; i < 42; i++)
                {
                    if (i == 42)
                    {
                        int diff = CheckedInfos[0].SaveDateChecked.DayOfYear - InfoDays[41].CheckedDate.DayOfYear;
                        if (CheckedInfos[0].SaveDateChecked.DayOfYear < InfoDays[41].CheckedDate.DayOfYear)
                        {
                            diff = CheckedInfos[0].SaveDateChecked.DayOfYear;
                        }
                        int finalDiff = diff % 42;
                        for (int p = 0; p < 42; p++)
                        {
                            finalDiff = diff + p;
                            if (diff >= 42)
                            {
                                if (finalDiff >= 42)
                                {
                                    finalDiff = finalDiff - 42;
                                }
                            }
                            CheckedInfos[p].InChecked.Text = InfoDays[finalDiff].InCheckInt.ToString();
                            CheckedInfos[p].OutChecked.Text = InfoDays[finalDiff].OutCheckedInt.ToString();
                            CheckedInfos[p].NoteCheck.Text = InfoDays[finalDiff].CheckedNotes;

                            if (CheckBool(InfoDays, finalDiff) == true)
                            {
                                ((CheckBox)CheckBoxes[p]).Checked = true;
                            }


                        }
                        break;
                    }

                    if (CheckedInfos[0].SaveDateChecked.DayOfYear == InfoDays[i].CheckedDate.DayOfYear)
                    {
                        for (int j = 0; j < 42; j++)
                        {
                            int k = j + i;
                            if (k >= 42)
                            {
                                k = k - 42;
                            }
                            CheckedInfos[j].InChecked.Text = InfoDays[k].InCheckInt.ToString();
                            CheckedInfos[j].OutChecked.Text = InfoDays[k].OutCheckedInt.ToString();
                            CheckedInfos[j].NoteCheck.Text = InfoDays[k].CheckedNotes;
                            if (CheckBool(InfoDays, k) == true)
                            {
                                ((CheckBox)CheckBoxes[j]).Checked = true;
                            }
                        }
                    }
                }


                //The following foreach and lamda statement populate the form with the DayInfos list based off of the day of month and add or subtract that value to the existing value in the 
                //textbox where the value is being placed.

                foreach (DayInfo item in DayInfos)
                {
                    Information info = InfoDays.First(x => x.DayOfMonth.Day.Equals(item.SaveDate.Day));
                    item.In.Text = conCat(item.In.Text, info.Ins);
                    item.Out.Text = conCat(item.Out.Text, info.Outs);
                    item.Note.Text = conCat(item.Note.Text, info.Notes);
                }
            }
            catch
            {
                MessageBox.Show("No file was loaded or the file contained incorrect data.", "File Not Loaded", MessageBoxButtons.OK);
            }
            //This will display the name of the file in the form header whenever a file is loaded.
            if (SaveFile.SaveFileName != null)
            {
                string form = SaveFile.SaveFileName;

                string x = form.Substring(form.LastIndexOf("\\") + 1);
                this.Text = "Loaded File: " + x;
            }
            Starting.Text = data.FinalStarting().ToString();
            AddToPostSavedInfos();
        }


        //Sets everthing back to default
        private void ClearForm(bool b)
        {
            if (b == true)
            {
                FinalOutput = 0;
                Starting.Text = "0";
                Output.Text = "0";

                for (int i = 0; i < 42; i++)
                {
                    Ins[i].Text = "0";
                    Outs[i].Text = "0";
                    Notes[i].Text = "";
                }

                Output.ForeColor = System.Drawing.Color.Green;
                this.Text = "Calendar Finance";

                SaveFile.SaveFileName = null;

                foreach (Control c in this.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = false;
                    }
                }
            }
        }
                    
        private void clearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("If you continue, all data will be cleared from the form. Are you sure?", "Clear All", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ClearForm(true);
            }
            else
            {
                MessageBox.Show("Form not cleared.", "No Action", MessageBoxButtons.OK);
            }    
        }

        //Sets the bool list of checked to true based on the index of which button you click (third checkbox is index 2). This is used in the SaveNewFile 
        //method to tell the save method which values to save or which ones to ignore.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Checked[0] = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Checked[1] = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Checked[2] = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Checked[3] = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Checked[4] = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Checked[5] = true;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Checked[6] = true;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Checked[7] = true;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Checked[8] = true;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Checked[9] = true;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            Checked[10] = true;
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            Checked[11] = true;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            Checked[12] = true;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            Checked[13] = true;
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            Checked[14] = true;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            Checked[15] = true;
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            Checked[16] = true;
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            Checked[17] = true;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            Checked[18] = true;
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            Checked[19] = true;
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            Checked[20] = true;
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            Checked[21] = true;

        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            Checked[22] = true;

        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            Checked[23] = true;    
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            Checked[24] = true;
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            Checked[25] = true;
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            Checked[26] = true;
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            Checked[27] = true;
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            Checked[28] = true;
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            Checked[29] = true;
        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            Checked[30] = true;
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            Checked[31] = true;
        }

        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            Checked[32] = true;
        }

        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            Checked[33] = true;
        }

        private void checkBox35_CheckedChanged(object sender, EventArgs e)
        {
            Checked[34] = true;
        }

        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
            Checked[35] = true;
        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {
            Checked[36] = true;
        }

        private void checkBox38_CheckedChanged(object sender, EventArgs e)
        {
            Checked[37] = true;
        }

        private void checkBox39_CheckedChanged(object sender, EventArgs e)
        {
            Checked[38] = true;
        }

        private void checkBox40_CheckedChanged(object sender, EventArgs e)
        {
            Checked[39] = true;
        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {
            Checked[40] = true;
        }

        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {
            Checked[41] = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < 42; i++)
            {
                if (ChangesMade(PostSaveInfos, start, i) == true)
                {
                    DialogResult diag = MessageBox.Show("Do you want to save your changes?", "Before you go..",
                    MessageBoxButtons.YesNoCancel);
                    if (diag == DialogResult.Yes)
                    {
                        // Call method to save file...
                        Form2 form = new Form2();
                        form.StartPosition = FormStartPosition.CenterParent;
                        SaveNewFile();
                        e.Cancel = false;
                        break;                      
                    }
                    else if (diag == DialogResult.No)
                    {
                        e.Cancel = false;
                        break;
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                else
                {
                    e.Cancel = false;
                    break;
                }           
            }            
        }

        private void SnapShot_Click(object sender, EventArgs e)
        {
            AddToPostSavedInfos();
            SixWeekSnapshot form = new SixWeekSnapshot();
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show();
        }

        private void Tutorial_Click(object sender, EventArgs e)
        {
            Tutorial tutorial = new Tutorial();
            tutorial.ShowDialog(this);
        }
    }
}
