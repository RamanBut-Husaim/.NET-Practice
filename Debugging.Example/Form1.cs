// Decompiled with JetBrains decompiler
// Type: CrackMe.Form1
// Assembly: CrackMe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E929FEB7-F3DD-449F-99CD-83285EA760B5
// Assembly location: C:\Development\de4dot\de4dot-v3-1\CrackMe-cleaned.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace CrackMe
{
    public class Form1 : Form
    {
        private string[] string_0;
        private IContainer icontainer_0;
        private Button bt_check;
        private TextBox tb_key;
        private Label label1;
        private Label lb_status;

        public Form1()
        {
            this.InitializeComponent();
        }

        private class Class0
        {
            public byte[] byte_0;
            public int[] int_0;

            public int method_0(byte value, int index)
            {
                return value ^ this.byte_0[index];
            }

            public int method_1(int value, int index)
            {
                return value - this.int_0[index];
            }
        }

        private void bt_check_Click(object sender, EventArgs e)
        {
            int num1 = 0;
            while (true)
            {
                switch (num1)
                {
                    case 1:
                        goto label_7;
                    case 2:
                        goto label_8;
                }

                int num2;
                if (string.IsNullOrEmpty(this.tb_key.Text))
                {
                    num2 = 2;
                    num1 = num2;
                }
                else
                {
                    this.string_0 = this.tb_key.Text.Split('-');
                    num2 = 1;
                    num1 = num2;
                }
            }

            label_7:
            this.lb_status.Text = this.method_0(this.string_0) ? "Correct key" : "Wrong key";
            this.lb_status.Visible = true;
            return;
            label_8:
            this.lb_status.Text = "Key cannot be empty";
        }

        private bool method_0(string[] string2)
        {
            label_9:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            Form1.Class0 class0 = new Form1.Class0();
            NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
            int num1 = 0;
            int num2 = num1;
            while (true)
            {
                switch (num2)
                {
                    case 0:
                        if (networkInterface == null)
                        {
                            num1 = 2;
                            num2 = num1;
                            continue;
                        }

                        num1 = 1;
                        num2 = num1;
                        continue;
                    case 1:
                        goto label_10;
                    case 2:
                        num1 = 3;
                        num2 = num1;
                        continue;
                    case 3:
                        goto label_11;
                    default:
                        goto label_9;
                }
            }

            label_10:
            byte[] addressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();
            // ISSUE: reference to a compiler-generated field
            class0.byte_0 = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());
            // ISSUE: reference to a compiler-generated method
            Func<byte, int, int> selector1 = class0.method_0;
            int[] numArray = addressBytes.Select(selector1).Select((int0 =>
            {
                if (int0 <= 999)
                {
                    return int0*10;
                }

                return int0;
            })).ToArray();

            // ISSUE: reference to a compiler-generated field
            class0.int_0 = string2.Select(int.Parse).ToArray();
            // ISSUE: reference to a compiler-generated method
            Func<int, int, int> selector2 = class0.method_1;
            return numArray.Select(selector2).All(int0 => int0 == 0);
            label_11:
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            int num1 = 2;
            while (true)
            {
                int num2;
                switch (num1)
                {
                    case 0:
                        num2 = 4;
                        num1 = num2;
                        continue;
                    case 1:
                        goto label_11;
                    case 3:
                        this.icontainer_0.Dispose();
                        num2 = 1;
                        num1 = num2;
                        continue;
                    case 4:
                        if (this.icontainer_0 != null)
                        {
                            num2 = 3;
                            num1 = num2;
                            continue;
                        }
                        goto label_11;
                    default:
                        if (disposing)
                        {
                            num2 = 0;
                            num1 = num2;
                            continue;
                        }

                        goto label_11;
                }
            }
            label_11:
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            switch (true)
            {
                default:
                    this.bt_check = new Button();
                    this.tb_key = new TextBox();
                    this.label1 = new Label();
                    this.lb_status = new Label();
                    this.SuspendLayout();
                    this.bt_check.Location = new Point(268, 51);
                    this.bt_check.Name = "bt_check";
                    this.bt_check.Size = new Size(75, 23);
                    this.bt_check.TabIndex = 0;
                    this.bt_check.Text = "Check";
                    this.bt_check.UseVisualStyleBackColor = true;
                    this.bt_check.Click += new EventHandler(this.bt_check_Click);
                    this.tb_key.Location = new Point(35, 25);
                    this.tb_key.Name = "tb_key";
                    this.tb_key.Size = new Size(308, 20);
                    this.tb_key.TabIndex = 1;
                    this.label1.AutoSize = true;
                    this.label1.Location = new Point(32, 9);
                    this.label1.Name = "label1";
                    this.label1.Size = new Size(107, 13);
                    this.label1.TabIndex = 2;
                    this.label1.Text = "Please, enter the key";
                    this.lb_status.AutoSize = true;
                    this.lb_status.Location = new Point(35, 52);
                    this.lb_status.Name = "lb_status";
                    this.lb_status.Size = new Size(35, 13);
                    this.lb_status.TabIndex = 3;
                    this.lb_status.Text = "label2";
                    this.lb_status.Visible = false;
                    this.AutoScaleDimensions = new SizeF(6f, 13f);
                    this.AutoScaleMode = AutoScaleMode.Font;
                    this.ClientSize = new Size(369, 86);
                    this.Controls.Add((Control) this.lb_status);
                    this.Controls.Add((Control) this.label1);
                    this.Controls.Add((Control) this.tb_key);
                    this.Controls.Add((Control) this.bt_check);
                    this.Name = "Form1";
                    this.Text = "Crack me";
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    break;
            }
        }
    }
}
