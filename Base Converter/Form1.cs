using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base_Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            if (UInt32.TryParse(textBox2.Text, out uint IBase) == false)
            {
                label3.Text = "Input Base: Cannot parse as number";
                return;
            }
            else if ((IBase > 1 && IBase <= 36) == false)
            {
                label3.Text = "Input Base: Must be in range from 2 and 36";
                return;
            }

            if (UInt32.TryParse(textBox4.Text, out uint OBase) == false)
            {
                label3.Text = "Output Base: Cannot parse as number";
                return;
            }
            else if ((OBase > 1 && OBase <= 36) == false)
            {
                label3.Text = "Output Base: Must be in range from 2 and 36";
                return;
            }

            //Get value
            String SValue = textBox1.Text;
            if (String.IsNullOrWhiteSpace(SValue))
            {
                label3.Text = "Input Value: Not available";
                return;
            }
            UInt64 Value = 0;
            if (IBase <= 10)
            {
                for (int i = 0; i < SValue.Length; i++)
                {
                    uint Digit;
                    if (SValue[i] >= '0' && SValue[i] < '0' + IBase) Digit = (uint)(SValue[i] - '0');
                    else
                    {
                        label3.Text = "Input Value: Invalid character - " + SValue[i];
                        return;
                    }
                    Value *= IBase;
                    Value += Digit;
                }
            }
            else
            {
                for (int i = 0; i < SValue.Length; i++)
                {
                    uint Digit;
                    if (SValue[i] >= '0' && SValue[i] <= '9') Digit = (uint)(SValue[i] - '0');
                    else if (SValue[i] >= 'a' && SValue[i] < 'a' + IBase - 10) Digit = 10 + (uint)(SValue[i] - 'a');
                    else if (SValue[i] >= 'A' && SValue[i] < 'A' + IBase - 10) Digit = 10 + (uint)(SValue[i] - 'A');
                    else
                    {
                        label3.Text = "Input Value: Invalid character - " + SValue[i];
                        return;
                    }
                    Value *= IBase;
                    Value += Digit;
                }
            }

            // Convert
            List<Char> ODigits = new();            
            UInt64 CValue = Value;
            while (CValue > 0)
            {
                UInt64 ODigit = CValue % OBase;
                if (ODigit < 10) ODigits.Add((char)('0' + ODigit));
                else ODigits.Add((char)('A' + ODigit - 10));
                CValue /= OBase;
            }
            ODigits.Reverse();

            textBox3.Text = new String(ODigits.ToArray());
        }
    }
}
