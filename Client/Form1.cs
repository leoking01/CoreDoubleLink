using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
//Download by http://www.codesc.net
namespace Client
{
    public partial class Form1 : Form,QQServiceReference.IQQSCallback 
    {

        public void Receive(string senderName, string message)
        {
            this.listBox2.Items.Add(senderName + " 说： " + message);
        }

        public void ReceiveWhisper(string senderName, string message)
        {
            this.listBox2.Items.Add(senderName + " 悄悄对您说： " + message);
        }

        public void UserEnter(string[] name)
        {
            this.listBox1.Items.Add(name[0]);
        }

        public void UserLeave(string name)
        {
            this.listBox1.Items.Remove(name[0]);
        }

        QQServiceReference.QQSClient clt = null;
        public Form1()
        {
            InitializeComponent();
            InstanceContext ins = new InstanceContext(this);
            clt = new QQServiceReference.QQSClient(ins);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ss = clt.join(this.textBox1.Text);
            if (ss[0] == "已经有此用户")
            {
                MessageBox.Show("已经有此用户！");
                return;
            }
            this.button1.Enabled = false;
            this.textBox1.Enabled = false;
            this.Text = "登陆成功";
            this.listBox1.Items.AddRange(ss);
            this.groupBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == "" || this.textBox3.Text == "")
            {
                MessageBox.Show("发送人或者信息不能为空");
                return;
            }
            clt.Whisper(textBox2.Text, textBox3.Text);
            this.listBox2.Items.Add("您悄悄对" + textBox2.Text + "说： " + textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clt.Speak(textBox4.Text);
            this.listBox2.Items.Add("您对所有人说： " + textBox3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clt.Leave();
            this.Dispose();
        }
    }
}
