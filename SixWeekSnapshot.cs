using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSupergoo.ABCpdf;
using System.IO;

namespace Finance_Calendar
{
    public partial class SixWeekSnapshot : Form
    {
        public SixWeekSnapshot()
        {
            InitializeComponent();
            this.Font = null;
            tableLayoutPanel1.Font = null;
        }

        private void SixWeekSnapshot_Load(object sender, EventArgs e)
        {
            AddTextBoxesAndLabels();
        }
        private Color color { get; set; }
        private System.Drawing.Font NewFont(FontStyle fs)
        {
            return new System.Drawing.Font("Lucida Sans", 10F, fs, GraphicsUnit.Point, ((byte)(0)));
        }
        /// <summary>
        /// This exists to assist my design which will show the date with its correct suffix
        /// </summary>

        private string Suffix(int i)
        {
            DateTime date = DateTime.Now;
            string dt = date.AddDays(i).ToString("MMMM") + " " + date.AddDays(i).Day.ToString();
            if (date.AddDays(i).Day.ToString() == "11" || date.AddDays(i).Day.ToString() == "12")
            {
                return dt + "th";
            }
            if (date.AddDays(i).Day.ToString().EndsWith("1"))
            {
                return dt + "st";
            }
            else if (date.AddDays(i).Day.ToString().EndsWith("2"))
            {
                return dt + "nd";
            }
            else if (date.AddDays(i).Day.ToString().EndsWith("3"))
            {
                return dt + "rd";
            }
            else
            {
                return dt + "th";
            }
        }

        private string Calc(int i)
        {
            return Form1.Calculate(i).ToString();
        }
        /// <summary>
        /// This uses my append text method extension to show in the panel with the desinged format. I use a panel and load the members into the panel because
        /// creating that many textboxes again would be tedious and unecessary, so loading them using a for loop makes more sense.
        /// </summary>
        private void AddTextBoxesAndLabels()
        {
            if (tableLayoutPanel1 != null)
            {
                tableLayoutPanel1.Controls.Clear();
            }
            for (int i = 0; i < 7; i++)
            {
                Label label = new Label();
                label.Text = DateTime.Now.AddDays(i).DayOfWeek.ToString();
                label.Dock = DockStyle.Fill;
                label.Anchor = AnchorStyles.Top;
                label.Anchor = AnchorStyles.Bottom;
                label.Font = NewFont(FontStyle.Bold);
                tableLayoutPanel1.Controls.Add(label);
            }
            for (int i = 0; i < 42; i++)
            {
                if (decimal.Parse(Calc(i)) < 0)
                {
                    color = Color.Red;
                }
                else
                {
                    color = Color.Green;
                }
                RichTextBox textBox = new RichTextBox();
                textBox.AppendText(Suffix(i), Color.Black, NewFont(FontStyle.Italic));
                textBox.AppendText(Environment.NewLine);
                textBox.AppendText("$ ", Color.DarkSlateBlue, NewFont(FontStyle.Regular));
                textBox.AppendText(Calc(i), color, NewFont(FontStyle.Bold));
                textBox.AppendText(Environment.NewLine);
                textBox.AppendText(Environment.NewLine);
                textBox.AppendText("In: ", Color.Black, NewFont(FontStyle.Bold));
                textBox.AppendText(Form1.PostSaveInfos[i].InPost, Color.Green, NewFont(FontStyle.Regular));
                textBox.AppendText(Environment.NewLine);
                textBox.AppendText("Out: ", Color.Black, NewFont(FontStyle.Bold));
                textBox.AppendText(Form1.PostSaveInfos[i].OutPost, Color.DarkOrange, NewFont(FontStyle.Regular));
                textBox.AppendText(Environment.NewLine);
                textBox.AppendText(Form1.PostSaveInfos[i].PostNotes, Color.Black, NewFont(FontStyle.Italic));
                textBox.ReadOnly = true;
                textBox.Multiline = true;
                textBox.Anchor = AnchorStyles.Top;
                textBox.Anchor = AnchorStyles.Bottom;
                textBox.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(textBox);
            }
        }
    }
    /// <summary>
    /// This extension allows me to modify more than just the text, giving me a way to have a more attractive design
    /// </summary>
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, System.Drawing.Font font)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionFont = font;
            box.AppendText(text);
        }
    }
}
