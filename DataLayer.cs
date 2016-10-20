using NCalc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Finance_Calendar
{
    public class DataLayer
    {
        public DataLayer()
        {
        }
        //This is the class that opens up the file dialog and allows you to load your XML file. It also has the save function for the DayInfos list.
        public static string Path { get; set; }

        // This is written in a way that sets the save file in the easiest returned format based on my 2 separate objects that contain properties based on 
        //different datetime values. Basically it checks if there are any values attached to the properties and then sets them accordingly
        public void Save(List<DayInfo> myList, string start, List<bool> myBool, string fileName)
        {
            List<Information> InsAndOuts = new List<Information>();

            for (int i = 0; i < 42; i++)
            {
                
                if (myBool[i] == true)
                {                
                    InsAndOuts.Add(new Information()
                    {
                        InCheckInt = myList[i].In.Text,
                        OutCheckedInt = myList[i].Out.Text,
                        CheckedNotes = myList[i].Note.Text,
                        CheckedDate = myList[i].SaveDate,
                        Ins = "0",
                        Outs = "0",
                        Notes = "",
                        DayOfMonth = myList[i].SaveDate
                    });
                }
                else
                {
                    InsAndOuts.Add(new Information()
                    {
                        InCheckInt = "0", 
                        OutCheckedInt = "0", 
                        CheckedNotes = "", 
                        CheckedDate = myList[i].SaveDate,
                        Ins = myList[i].In.Text,
                        Outs = myList[i].Out.Text,
                        Notes = myList[i].Note.Text,
                        DayOfMonth = myList[i].SaveDate
                    });
                }
            }
            InsAndOuts.Add(new Information()
            {
                StartingValue = start,
                StartingDate = DateTime.Now
            });

            try
            {
                SaveAll.SaveData(InsAndOuts, fileName + ".xml");
            }
            catch 
            {
                MessageBox.Show("Unable to save the data in the form. Please try again", "Cannot Save", MessageBoxButtons.OK);
            }
        }

        public List<Information> Load()
        {
            Stream myStream = null;
            OpenFileDialog diag = new OpenFileDialog();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Calendar Finance XML";

            diag.Title = "Select the Calendar File";
            diag.Filter = "XML files|*.xml";
            diag.InitialDirectory = path; 

            if (diag.ShowDialog() == DialogResult.OK)
            {
                SaveFile.SaveFileName = diag.SafeFileName.ToString().TrimEnd(new char[] { '.', 'x', 'm', 'l' });
                Path = diag.FileName.ToString();
                            
                if ((myStream = diag.OpenFile()) != null)
                {
                    using (myStream)
                    {
                        if (File.Exists(diag.FileName))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(List<Information>));
                            FileStream read = new FileStream(diag.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                            List<Information> listInfo = (List<Information>)xs.Deserialize(read);

                            read.Close();
                            diag.Dispose();

                            return listInfo;
                        }
                        else
                        {
                            MessageBox.Show("File does not exist");
                            return null;
                        }                        
                    }
                }
                else
                {
                    diag.Dispose();
                    return null;
                }
            }
            else
            {                
                diag.Dispose();
                return null;
            }           
        }

        // This is a poorly written method that will probably go away. It exists to add up all of the values in the past and give you the sum. 
        public decimal FinalStarting()
        {
            decimal fin = 0;

            try
            {
                //Calculates the values that have occured in days that have passed and returns the final number.
                XmlSerializer xs = new XmlSerializer(typeof(List<Information>));

                FileStream readInsandOuts = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);

                List<Information> listInfo = (List<Information>)xs.Deserialize(readInsandOuts);

                readInsandOuts.Close();

                fin = int.Parse(listInfo[42].StartingValue);


                foreach (Information item in listInfo.Where(x => x.DayOfMonth >= listInfo[42].StartingDate && x.DayOfMonth <= DateTime.Now))
                {
                    Expression InEx = new Expression(item.Ins);
                    Expression OutEx = new Expression(item.Outs);
                    fin += decimal.Parse(InEx.Evaluate().ToString());
                    fin -= decimal.Parse(OutEx.Evaluate().ToString());
                }
                foreach (Information item in listInfo.Where(x => x.CheckedDate >= listInfo[42].StartingDate && x.DayOfMonth <= DateTime.Now))
                {
                    Expression InExCheck = new Expression(item.InCheckInt);
                    Expression OutExCheck = new Expression(item.OutCheckedInt);
                    fin += decimal.Parse(InExCheck.Evaluate().ToString());
                    fin -= decimal.Parse(OutExCheck.Evaluate().ToString());
                }
                return fin;
            }
            catch
            {
                MessageBox.Show("Could not load the starting value. Try re-entering the value and saving the file and try again." ,"Error Loading Start Value", MessageBoxButtons.OK);
                return 0;
            }
        }
    }
}
