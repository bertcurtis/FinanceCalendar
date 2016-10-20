using Finance_Calendar.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finance_Calendar
{
    public partial class Tutorial : Form
    {
        public Tutorial()
        {
            InitializeComponent();
            Images = new List<Image>();
            Instructions = new List<string>();
            Title = new List<string>();

        }
        public List<Image> Images { get; set; }
        public List<string> Instructions { get; set; }
        public List<string> Title { get; set; }

        public int MyInt = 0;

        private void Tutorial_Load(object sender, EventArgs e)
        {
            //Image image = Resources.StartingIn;
            //pictureBox1.Image = image;
            Images.Add(Resources.Start);
            Images.Add(Resources.Input);
            Images.Add(Resources.Buttons);
            Images.Add(Resources.SaveFile);
            Images.Add(Resources.LoadFile);
            Images.Add(Resources.Calculate);
            Images.Add(Resources.LockBox);

            Instructions.Add("This is where you begin! Put your current checking bank balance here (or whatever amount of money you want to work with). Over time this number will"
                + " change as your incoming income and outgoing bills are summed up. So think of this box as a window of what could have been if you hadn't spent all that money"
                + " on game boy games and ritz crackers. Simply subtract the money you had spent (excluding bills) since the last time you opened the app and it should match your bank.");
            Instructions.Add("Now begins the fun part. This calendar is different than the ones you have seen. The first text box on the top left will always be today's date. This is so you"
                + " can plan ahead. It's easy to figure out where your money went (that's what bank statements are for) but it's hard to visualize where and when your money will disappear."
                + " So, using a bank statement(or any other method) enter all of your 'Ins' and 'Outs' (paychecks and bills) on the day of the month that they occur (For example:"
                + " If you know that your power bill comes out on the 5th of every month, then put 50.00 on the 5th and label it in the notes how you choose!). If you have multiple bills/ sources of"
                + " income, no worries! Write it in this format: 125.00 + 10.00. Be sure to add the plus sign or it won't work! It's that easy! Note: If you want to save time, you don't have to re-enter the Ins and Outs"
                + " on days that occur twice on the calendar. Just save the file and then load it back up and your values will be shown accordingly!");
            Instructions.Add("Now that we have all of our ducks in a row, let's shoot 'em!Just kidding, let's start saving some files. As you can see there are 3 buttons here."
                + " The first one will save a file based on all of the numbers you inputed into the calendar. The second button will load a file."
                + " The last button will clear all the values you have entered but will not corrupt your save file.");
            Instructions.Add("Once you click save, it will ask you to choose a file name. This will save in a folder created in your 'My Documents' folder. If you are just saving a file that you've worked on,"
                + " the name of the file will appear and you can just click save and overwrite the file.");
            Instructions.Add("Loading a file is easy! Just select the file that you want to load, and click 'Open'. If you don't have a file then you won't be able to load anything in yet.");           
            Instructions.Add("Once you have all of your Ins and Outs entered, or a file loaded, let's go ahead and click that 'Calcualte' button. On whatever day you click the calendar will tell you exactly how much money"
                + " you're going to have on that day based on whatever you decide to include as recurring Ins and Outs (some folks might want to include gas money as a continual payment). Ain't that neat?"
                + " This is especially helpful in planning when you're going to make that extra credit card payment!");
            Instructions.Add("As you can see, there is a peculiar little checkbox at the bottom of every box labelled 'Lock Day of the Week'. This is a nifty feature for you paid-every-other-friday people."
                + " You see, clicking this box will give you the ability to save based on the day of the week that you save it under. For example, If I got paid every other Friday, I could just input"
                + " the amount I got paid in the In box for every other Friday, and then every time"
                + " I loaded the that file, It would show those numbers on the day of the week that I saved them under! Prett cool eh? Just remember that the program doesn't know how often you want to show"
                + " those numbers so you do have to fill out the form completely based on how frequent you want to see that number (i.e. you cant just put the number in for one Friday, you have to put it in"
                + " every other week until there is not another friday visible on the form.");

            Title.Add("Starting Amount");
            Title.Add("Adding Ins and Outs");
            Title.Add("Save, Load, and Clear");
            Title.Add("Saving a File");
            Title.Add("Loading a File");
            Title.Add("Calculating Amounts");
            Title.Add("Bonus Feature: The Lock Box");
            pictureBox1.Image = Images[0];
            textBox1.Text = Instructions[0];
            label1.Text = Title[0];
            label1.TextAlign = ContentAlignment.TopCenter;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MyInt++;
            if(MyInt == 6)
            {
                button1.Text = "Finish";
            }
            if(MyInt > 6)
            {               
                DialogResult dialog = MessageBox.Show("Tutorial Completed! You should now be a finance calendar master. If not, well that sucks! Sorry!", "Completed", MessageBoxButtons.OK);
                if(dialog == DialogResult.OK)
                {
                    Close();
                }
                MyInt = 0;
            }
            pictureBox1.Image = Images[MyInt];
            textBox1.Text = Instructions[MyInt];
            label1.Text = Title[MyInt];
            label1.TextAlign = ContentAlignment.TopCenter;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyInt--;
            if (MyInt < 0)
            {
                MyInt = 6;
            }
            pictureBox1.Image = Images[MyInt];
            textBox1.Text = Instructions[MyInt];
            label1.Text = Title[MyInt];
            label1.TextAlign = ContentAlignment.TopCenter;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
