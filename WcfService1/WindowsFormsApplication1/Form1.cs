using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.ServiceReference1;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool fromB = false;
        bool toB = false;
        bool timeB = false;
        public Form1()
        {
            InitializeComponent();

            maskedTextBox1.Mask = "0000-00-00 00:00";
            maskedTextBox2.Mask = "0000-00-00 00:00";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
      
            try
            {
                listBox1.Items.Clear();
            textBox11.Text = " ";
            Service1Client client = new Service1Client();
            string time = " ";
            string from =" ";
            string to = " ";
            string[] a = null;
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                if (textBox1.Text.Equals("A") || textBox1.Text.Equals("B") || textBox1.Text.Equals("C") || textBox1.Text.Equals("D"))
                {
                    from = textBox1.Text;
                    fromB = true;
                }else
                {
                    textBox11.Text += "Brak miasta" + textBox1.Text + " ";
                }
            }

            if(!string.IsNullOrEmpty(textBox3.Text))
            {
                if (textBox3.Text.Equals("A") || textBox3.Text.Equals("B") || textBox3.Text.Equals("C") || textBox3.Text.Equals("D"))
                {
                    to = textBox3.Text;
                    toB = true;
                }
                else
                {
                    textBox11.Text += " Brak miasta" + textBox3.Text + " ";
                }
            }

            if (!string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                    var splitedLine = maskedTextBox1.Text.Split(' ');
                    string timess = splitedLine[1];
                    var splitedLine2 = timess.Split(':');
                    time = splitedLine2[0];
                    if (!string.IsNullOrEmpty(time))
                    {
                        timeB = true;
                    }
                    else
                    {
                        timeB = false;
                    }
                }
                if (!string.IsNullOrEmpty(maskedTextBox2.Text))
                {
                    var splitedLine = maskedTextBox1.Text.Split(' ');
                    string timess = splitedLine[1];
                    var splitedLine2 = timess.Split(':');
                    time = splitedLine2[0];
                    if (!string.IsNullOrEmpty(time))
                    {
                        timeB = true;
                    }else
                    {
                        timeB = false;
                    }
                }

                if (fromB == true && toB == true && timeB == true)
            {
                try
                {
                    a = client.getTripWithTime(from, to, time, Convert.ToDateTime(maskedTextBox1.Text), Convert.ToDateTime(maskedTextBox2.Text));
                }catch(Exception ex)
                {
                    textBox11.Text = ex.ToString();
                }
                    try
                    {
                        foreach (string row in a)
                        {
                            listBox1.Items.Add(row);

                        }
                    }catch(NullReferenceException eexce)
                    {
                        textBox11.Text = "Pusta lista";
                    }
                    
            }
            else if (fromB == true && toB == true && timeB == false)
            {
                a = client.getTrip(from, to);
          
            foreach (string row in a)
                {
                    listBox1.Items.Add(row);

                }

                textBox11.Text = " ";
            }
            else { 
               textBox11.Text += "Wprowadź poprawne dane";
                listBox1.Items.Clear();
            }
            fromB = false;
            toB = false;
            timeB = false;
            }
            catch (FaultException fault)
            {
                MessageBox.Show(fault.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.ServiceModel.CommunicationException)
            {

                MessageBox.Show("Check your internet connection ", "Internet connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

/* // 05 / 01 / 2009 14:57:32.8
  //textBox2-godzina
  //textBox4 - minuty
  //textBox5 - dzien
  //textBox6-miesiac
  //textBox7 - rok
  string dzien = textBox5.Text;
  string miesiac = textBox6.Text;
  string rok = textBox7.Text;
  string godzina = textBox2.Text;
  string minuty = textBox4.Text;
  string sekundy = "00";
  //string data = dzien + "/" + miesiac + "/" + rok + " " + godzina + ":" + minuty + ":" + sekundy + ".8";
  DateTime dateValue;
  DateTime.TryParse(dzien, out dateValue);
  //Console.WriteLine("  Converted '{0}' to {1} ({2}).", data,
                 //   dateValue, dateValue.Kind);
  DateTime dat = new DateTime(2017, 5, 12, 12, 32, 30);
  //DateTime.ParseExact(data, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
  */
