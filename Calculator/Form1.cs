using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Calculator
{
    public partial class theForm : Form
    {
        string labelLine = " 0";
        int labelSize = 13;

        public theForm()
        {
            InitializeComponent();
            updateDisplay();
            KeyPreview = true;
        }

        private void updateDisplay()
        {
            outputLabel.Text = labelLine.Substring(labelSize < labelLine.Length ? labelLine.Length - 1 - labelSize : 0);
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            if (labelLine == " 0")
                labelLine = labelLine.Substring(0, labelLine.Length - 1);
            if (labelLine != " ∞")
                labelLine += (sender as Button).Text;
            updateDisplay();
        }

        private void onSizeChg(object sender, EventArgs e)
        {
            labelSize = 7 + (ClientSize.Width - 217) / 26;
            updateDisplay();
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (char.IsDigit(labelLine[labelLine.Length - 1]) 
                && !labelLine.Substring(labelLine.LastIndexOf(' ')).Contains(','))
                labelLine += (sender as Button).Text;
            updateDisplay();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            if (labelLine[labelLine.Length - 1] == ',')
                labelLine = labelLine.Substring(0, labelLine.Length - 1);
            if (char.IsDigit(labelLine[labelLine.Length - 1]))
                labelLine +=' '+(sender as Button).Text+' ';
            updateDisplay();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            if (labelLine[labelLine.Length - 1] == ' ')
                labelLine = labelLine.Substring(0, labelLine.Length - 3);
            string buffr = labelResult(RWarsawNotation(labelLine)).ToString();
            labelLine = ' ' + (buffr == "NaN" ? " Error" : buffr);
            Clipboard.SetText(labelLine);
            updateDisplay();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (labelLine != " 0")
            {
                if (labelLine[labelLine.Length - 1] == ' ')
                    labelLine = labelLine.Substring(0, labelLine.LastIndexOf(' ') - 2);
                else
                    labelLine = labelLine.Substring(0, labelLine.LastIndexOf(' ') + 1);
                if (labelLine == " ")
                    labelLine += '0';
            }
            updateDisplay();
        }


        private string RWarsawNotation(string input)
        {
            string newInput = " ";
            Stack<string> items = new Stack<string>();
            int spacesCount = countSpace(input);
            input += ' ';
            for (int i = 0; i < spacesCount; i++)
            {
                string item = input.Substring(1, input.IndexOf(' ', 1) - 1);
                input = input.Substring(input.IndexOf(' ', 1));
                if (char.IsDigit(item[0]))
                    newInput += item + ' ';
                else
                {
                    while (items.Count > 0
                            && ((new[] { "/", "*" }.Contains(items.Peek())
                            && new[] { "/", "*", "+", "-" }.Contains(item))
                                || new[] { "+", "-" }.Contains(item))
                           )
                        newInput += items.Pop() + ' ';
                    items.Push(item);
                }
            }
            while (items.Count >= 1)
                newInput += items.Pop() + ' ';
            return newInput;
        }

        private double labelResult(string inLine)
        {
            int spaceNum = countSpace(inLine);
            Stack<double> items = new Stack<double>();
            for (int i = 0; i < spaceNum - 1; i++)
            {
                string item = inLine.Substring(1, inLine.IndexOf(' ', 1) - 1);
                inLine = inLine.Substring(inLine.IndexOf(' ', 1));
                if (!new[] { "+", "-", "*", "/" }.Contains(item))
                    items.Push(Convert.ToDouble(item));
                else
                {
                    double rightOperand = items.Pop(), leftOperand = items.Pop();
                    switch (item)
                    {
                        case "+":
                            items.Push(leftOperand + rightOperand);
                            break;
                        case "-":
                            items.Push(leftOperand - rightOperand);
                            break;
                        case "*":
                            items.Push(leftOperand * rightOperand);
                            break;
                        case "/":
                            items.Push(leftOperand / rightOperand);
                            break;
                    }
                }
            }
            return items.Pop();
        }

        private int countSpace(string input)
        {
            int spaceCount = 0;
            foreach (char simb in input)
                if (simb == ' ') spaceCount++;
            return spaceCount;
        }

        private void RButton_Click(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                case Keys.D0:
                    buttonZero.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad1:
                case Keys.D1:
                    buttonOne.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    buttonTwo.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad3:
                case Keys.D3:
                    buttonThree.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad4:
                case Keys.D4:
                    buttonFour.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad5:
                case Keys.D5:
                    buttonFive.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad6:
                case Keys.D6:
                    buttonSix.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad7:
                case Keys.D7:
                    buttonSeven.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad8:
                case Keys.D8:
                    buttonEight.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.NumPad9:
                case Keys.D9:
                    buttonNine.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Back:
                    buttonClear.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    buttonDot.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Oemplus:
                case Keys.Add:
                    buttonPlus.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.OemMinus:
                case Keys.Subtract:
                    buttonMinus.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Multiply:
                    buttonMultiply.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Divide:
                case Keys.OemQuestion:
                    buttonDivide.PerformClick();
                    e.Handled = true;
                    break;
            }
        }

    }
}
