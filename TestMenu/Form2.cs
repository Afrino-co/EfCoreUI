using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMenu
{
    public partial class Form2 : Form
    {
        Form1 _form1;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;

        }

        public async Task DisplayMessage(string message)
        {
            await Task.Run(() =>
            {
                // نمایش خروجی به کاربر در ترد گرافیکی
                textBox2.BeginInvoke((MethodInvoker)delegate
                {
                    this.ForeColor = Color.White;
                    textBox2.AppendText("  " + message + Environment.NewLine);


                });
            });
            await Task.Run(() =>
            {
                // نمایش خروجی به کاربر در ترد گرافیکی
                textBox2.BeginInvoke((MethodInvoker)delegate
                {

                    this.ForeColor = Color.Lime;
                    textBox2.AppendText(Environment.NewLine + ">");
                });
            });
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _form1.button2.Enabled = false;
            _form1.button1.Enabled = false;
            _form1.button3.Enabled = false;
            _form1.ControlBox = false;

        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _form1.button2.Enabled = true;
            _form1.button1.Enabled = true;
            _form1.button3.Enabled = true;
            _form1.ControlBox = true;

        }
    }
}
