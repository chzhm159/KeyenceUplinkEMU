namespace KeyenceUplinkEMU
{
    partial class DevProperty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            groupBox1 = new GroupBox();
            prop_bt_close = new Button();
            label2 = new Label();
            prop_tb_ip = new TextBox();
            prop_cbx_protocol = new ComboBox();
            label3 = new Label();
            prop_tb_port = new TextBox();
            label4 = new Label();
            prop_bt_save = new Button();
            prop_lb_msg = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(prop_lb_msg);
            groupBox1.Controls.Add(prop_cbx_protocol);
            groupBox1.Controls.Add(prop_bt_save);
            groupBox1.Controls.Add(prop_bt_close);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(prop_tb_port);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(prop_tb_ip);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(319, 322);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "设备配置";
            // 
            // prop_bt_close
            // 
            prop_bt_close.Location = new Point(22, 224);
            prop_bt_close.Margin = new Padding(4, 4, 4, 4);
            prop_bt_close.Name = "prop_bt_close";
            prop_bt_close.Size = new Size(96, 27);
            prop_bt_close.TabIndex = 3;
            prop_bt_close.Text = "关闭";
            prop_bt_close.UseVisualStyleBackColor = true;
            prop_bt_close.Click += prop_bt_close_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 100);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(56, 20);
            label2.TabIndex = 1;
            label2.Text = "IP 地址";
            // 
            // prop_tb_ip
            // 
            prop_tb_ip.Location = new Point(130, 97);
            prop_tb_ip.Margin = new Padding(4, 4, 4, 4);
            prop_tb_ip.Name = "prop_tb_ip";
            prop_tb_ip.Size = new Size(154, 26);
            prop_tb_ip.TabIndex = 2;
            // 
            // prop_cbx_protocol
            // 
            prop_cbx_protocol.DropDownStyle = ComboBoxStyle.DropDownList;
            prop_cbx_protocol.FormattingEnabled = true;
            prop_cbx_protocol.Items.AddRange(new object[] { "uplink(基恩士)" });
            prop_cbx_protocol.Location = new Point(130, 42);
            prop_cbx_protocol.Margin = new Padding(4, 4, 4, 4);
            prop_cbx_protocol.Name = "prop_cbx_protocol";
            prop_cbx_protocol.Size = new Size(154, 28);
            prop_cbx_protocol.TabIndex = 4;
            prop_cbx_protocol.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.Location = new Point(22, 45);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 1;
            label3.Text = "通信协议";
            // 
            // prop_tb_port
            // 
            prop_tb_port.Location = new Point(130, 157);
            prop_tb_port.Margin = new Padding(4, 4, 4, 4);
            prop_tb_port.Name = "prop_tb_port";
            prop_tb_port.Size = new Size(154, 26);
            prop_tb_port.TabIndex = 2;
            prop_tb_port.TextChanged += textBox3_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 160);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 1;
            label4.Text = "端口";
            // 
            // prop_bt_save
            // 
            prop_bt_save.Location = new Point(188, 224);
            prop_bt_save.Margin = new Padding(4);
            prop_bt_save.Name = "prop_bt_save";
            prop_bt_save.Size = new Size(96, 27);
            prop_bt_save.TabIndex = 3;
            prop_bt_save.Text = "保存";
            prop_bt_save.UseVisualStyleBackColor = true;
            prop_bt_save.Click += prop_bt_save_Click;
            // 
            // prop_lb_msg
            // 
            prop_lb_msg.Dock = DockStyle.Bottom;
            prop_lb_msg.Location = new Point(4, 266);
            prop_lb_msg.Name = "prop_lb_msg";
            prop_lb_msg.Size = new Size(311, 52);
            prop_lb_msg.TabIndex = 5;
            prop_lb_msg.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DevProperty
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 322);
            Controls.Add(groupBox1);
            Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 4, 4, 4);
            Name = "DevProperty";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button prop_bt_close;
        private ComboBox prop_cbx_protocol;
        private Label label2;
        private TextBox prop_tb_ip;
        private Label label3;
        private Label label4;
        private TextBox prop_tb_port;
        private Button prop_bt_save;
        private Label prop_lb_msg;
    }
}