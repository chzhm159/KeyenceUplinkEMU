namespace KeyenceUplinkEMU
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            TreeNode treeNode7 = new TreeNode("设备列表");
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            app_console = new RichTextBox();
            app_tabs = new TabControl();
            tab_page_1 = new TabPage();
            app_spc_main = new SplitContainer();
            tv_devlist = new TreeView();
            pn_tags = new Panel();
            dgv_acklist = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewComboBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            pn_tags_bottom = new Panel();
            btn_newTag = new Button();
            pn_tags_top = new Panel();
            btn_dev_chage = new Button();
            btn_plc_ctl = new Button();
            tb_ip = new TextBox();
            lb_ip = new Label();
            tb_port = new TextBox();
            lb_port = new Label();
            panel3 = new Panel();
            label2 = new Label();
            app_lb_clientCount = new Label();
            app_cmb_clients = new ComboBox();
            menuStrip1 = new MenuStrip();
            文件ToolStripMenuItem = new ToolStripMenuItem();
            tsm_save = new ToolStripMenuItem();
            lb_protocol = new Label();
            cb_protocol = new ComboBox();
            app_tabs.SuspendLayout();
            tab_page_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)app_spc_main).BeginInit();
            app_spc_main.Panel1.SuspendLayout();
            app_spc_main.Panel2.SuspendLayout();
            app_spc_main.SuspendLayout();
            pn_tags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_acklist).BeginInit();
            pn_tags_bottom.SuspendLayout();
            pn_tags_top.SuspendLayout();
            panel3.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // app_console
            // 
            app_console.Dock = DockStyle.Bottom;
            app_console.Location = new Point(0, 556);
            app_console.Margin = new Padding(4);
            app_console.Name = "app_console";
            app_console.Size = new Size(1175, 142);
            app_console.TabIndex = 2;
            app_console.Text = "";
            // 
            // app_tabs
            // 
            app_tabs.Controls.Add(tab_page_1);
            app_tabs.Dock = DockStyle.Fill;
            app_tabs.Location = new Point(0, 28);
            app_tabs.Margin = new Padding(4);
            app_tabs.Name = "app_tabs";
            app_tabs.SelectedIndex = 0;
            app_tabs.Size = new Size(1175, 528);
            app_tabs.TabIndex = 5;
            // 
            // tab_page_1
            // 
            tab_page_1.Controls.Add(app_spc_main);
            tab_page_1.Controls.Add(panel3);
            tab_page_1.Location = new Point(4, 29);
            tab_page_1.Margin = new Padding(4);
            tab_page_1.Name = "tab_page_1";
            tab_page_1.Padding = new Padding(4);
            tab_page_1.Size = new Size(1167, 495);
            tab_page_1.TabIndex = 0;
            tab_page_1.Text = "配置";
            tab_page_1.UseVisualStyleBackColor = true;
            // 
            // app_spc_main
            // 
            app_spc_main.BorderStyle = BorderStyle.FixedSingle;
            app_spc_main.Dock = DockStyle.Fill;
            app_spc_main.Location = new Point(4, 4);
            app_spc_main.Name = "app_spc_main";
            // 
            // app_spc_main.Panel1
            // 
            app_spc_main.Panel1.Controls.Add(tv_devlist);
            // 
            // app_spc_main.Panel2
            // 
            app_spc_main.Panel2.Controls.Add(pn_tags);
            app_spc_main.Panel2.Controls.Add(pn_tags_bottom);
            app_spc_main.Panel2.Controls.Add(pn_tags_top);
            app_spc_main.Panel2.Padding = new Padding(3);
            app_spc_main.Size = new Size(1159, 451);
            app_spc_main.SplitterDistance = 200;
            app_spc_main.TabIndex = 7;
            // 
            // tv_devlist
            // 
            tv_devlist.Dock = DockStyle.Fill;
            tv_devlist.Location = new Point(0, 0);
            tv_devlist.Name = "tv_devlist";
            treeNode7.Name = "dev_root";
            treeNode7.Text = "设备列表";
            tv_devlist.Nodes.AddRange(new TreeNode[] { treeNode7 });
            tv_devlist.Size = new Size(198, 449);
            tv_devlist.TabIndex = 0;
            tv_devlist.AfterSelect += tv_devlist_AfterSelect;
            tv_devlist.NodeMouseClick += tv_devlist_NodeMouseClick;
            // 
            // pn_tags
            // 
            pn_tags.Controls.Add(dgv_acklist);
            pn_tags.Dock = DockStyle.Fill;
            pn_tags.Location = new Point(3, 47);
            pn_tags.Name = "pn_tags";
            pn_tags.Size = new Size(947, 357);
            pn_tags.TabIndex = 0;
            // 
            // dgv_acklist
            // 
            dgv_acklist.AllowUserToAddRows = false;
            dgv_acklist.AllowUserToOrderColumns = true;
            dgv_acklist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_acklist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_acklist.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4 });
            dgv_acklist.Dock = DockStyle.Fill;
            dgv_acklist.Location = new Point(0, 0);
            dgv_acklist.Name = "dgv_acklist";
            dgv_acklist.RowTemplate.Height = 25;
            dgv_acklist.Size = new Size(947, 357);
            dgv_acklist.TabIndex = 0;
            dgv_acklist.CellValueChanged += dgv_acklist_CellValueChanged;
            dgv_acklist.RowsAdded += dgv_acklist_RowsAdded;
            dgv_acklist.Validated += dgv_acklist_Validated;
            // 
            // Column1
            // 
            Column1.FillWeight = 20F;
            Column1.HeaderText = "地址";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            dataGridViewCellStyle7.NullValue = "请选择...";
            Column2.DefaultCellStyle = dataGridViewCellStyle7;
            Column2.FillWeight = 20F;
            Column2.HeaderText = "类型";
            Column2.Items.AddRange(new object[] { "int16", "uint16", "string", "int32", "uint32", "bool", "hex" });
            Column2.Name = "Column2";
            Column2.Resizable = DataGridViewTriState.True;
            Column2.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            Column3.FillWeight = 20F;
            Column3.HeaderText = "数量";
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.HeaderText = "数值";
            Column4.Name = "Column4";
            // 
            // pn_tags_bottom
            // 
            pn_tags_bottom.Controls.Add(btn_dev_chage);
            pn_tags_bottom.Controls.Add(cb_protocol);
            pn_tags_bottom.Controls.Add(btn_newTag);
            pn_tags_bottom.Controls.Add(tb_ip);
            pn_tags_bottom.Controls.Add(lb_protocol);
            pn_tags_bottom.Controls.Add(lb_ip);
            pn_tags_bottom.Controls.Add(tb_port);
            pn_tags_bottom.Controls.Add(lb_port);
            pn_tags_bottom.Dock = DockStyle.Bottom;
            pn_tags_bottom.Location = new Point(3, 404);
            pn_tags_bottom.Name = "pn_tags_bottom";
            pn_tags_bottom.Padding = new Padding(3);
            pn_tags_bottom.Size = new Size(947, 42);
            pn_tags_bottom.TabIndex = 2;
            // 
            // btn_newTag
            // 
            btn_newTag.Dock = DockStyle.Right;
            btn_newTag.Location = new Point(838, 3);
            btn_newTag.Name = "btn_newTag";
            btn_newTag.Size = new Size(106, 36);
            btn_newTag.TabIndex = 0;
            btn_newTag.Text = "添加新点位";
            btn_newTag.UseVisualStyleBackColor = true;
            btn_newTag.Click += btn_newTag_Click;
            // 
            // pn_tags_top
            // 
            pn_tags_top.Controls.Add(btn_plc_ctl);
            pn_tags_top.Dock = DockStyle.Top;
            pn_tags_top.Location = new Point(3, 3);
            pn_tags_top.Name = "pn_tags_top";
            pn_tags_top.Padding = new Padding(4);
            pn_tags_top.Size = new Size(947, 44);
            pn_tags_top.TabIndex = 1;
            // 
            // btn_dev_chage
            // 
            btn_dev_chage.Location = new Point(589, 6);
            btn_dev_chage.Name = "btn_dev_chage";
            btn_dev_chage.Size = new Size(75, 30);
            btn_dev_chage.TabIndex = 3;
            btn_dev_chage.Text = "修改";
            btn_dev_chage.UseVisualStyleBackColor = true;
            btn_dev_chage.Visible = false;
            btn_dev_chage.Click += btn_dev_chage_Click;
            // 
            // btn_plc_ctl
            // 
            btn_plc_ctl.BackColor = Color.Gray;
            btn_plc_ctl.Location = new Point(7, 7);
            btn_plc_ctl.Name = "btn_plc_ctl";
            btn_plc_ctl.Size = new Size(75, 30);
            btn_plc_ctl.TabIndex = 2;
            btn_plc_ctl.Text = "未启动";
            btn_plc_ctl.UseVisualStyleBackColor = false;
            btn_plc_ctl.Visible = false;
            btn_plc_ctl.Click += btn_plc_ctl_Click;
            // 
            // tb_ip
            // 
            tb_ip.Font = new Font("Microsoft YaHei UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            tb_ip.Location = new Point(305, 6);
            tb_ip.Multiline = true;
            tb_ip.Name = "tb_ip";
            tb_ip.Size = new Size(140, 28);
            tb_ip.TabIndex = 1;
            tb_ip.Text = "/";
            // 
            // lb_ip
            // 
            lb_ip.Location = new Point(213, 6);
            lb_ip.Name = "lb_ip";
            lb_ip.Size = new Size(85, 28);
            lb_ip.TabIndex = 0;
            lb_ip.Text = "监听地址:";
            lb_ip.TextAlign = ContentAlignment.MiddleLeft;
            lb_ip.Click += lb_port_Click;
            // 
            // tb_port
            // 
            tb_port.Font = new Font("Microsoft YaHei UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            tb_port.Location = new Point(511, 6);
            tb_port.Multiline = true;
            tb_port.Name = "tb_port";
            tb_port.Size = new Size(72, 28);
            tb_port.TabIndex = 1;
            tb_port.Text = "/";
            // 
            // lb_port
            // 
            lb_port.Location = new Point(451, 6);
            lb_port.Name = "lb_port";
            lb_port.Size = new Size(54, 28);
            lb_port.TabIndex = 0;
            lb_port.Text = "端口:";
            lb_port.TextAlign = ContentAlignment.MiddleLeft;
            lb_port.Click += lb_port_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(label2);
            panel3.Controls.Add(app_lb_clientCount);
            panel3.Controls.Add(app_cmb_clients);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(4, 455);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(4);
            panel3.Size = new Size(1159, 36);
            panel3.TabIndex = 6;
            // 
            // label2
            // 
            label2.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(5, 4);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(66, 28);
            label2.TabIndex = 2;
            label2.Text = "客户端:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // app_lb_clientCount
            // 
            app_lb_clientCount.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            app_lb_clientCount.Location = new Point(340, 4);
            app_lb_clientCount.Margin = new Padding(4, 0, 4, 0);
            app_lb_clientCount.Name = "app_lb_clientCount";
            app_lb_clientCount.Size = new Size(58, 25);
            app_lb_clientCount.TabIndex = 3;
            app_lb_clientCount.Text = "0";
            app_lb_clientCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // app_cmb_clients
            // 
            app_cmb_clients.DropDownStyle = ComboBoxStyle.DropDownList;
            app_cmb_clients.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            app_cmb_clients.FormattingEnabled = true;
            app_cmb_clients.Location = new Point(79, 4);
            app_cmb_clients.Margin = new Padding(4);
            app_cmb_clients.Name = "app_cmb_clients";
            app_cmb_clients.Size = new Size(253, 29);
            app_cmb_clients.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip1.GripStyle = ToolStripGripStyle.Visible;
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1175, 28);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            文件ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsm_save });
            文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            文件ToolStripMenuItem.Size = new Size(51, 24);
            文件ToolStripMenuItem.Text = "文件";
            // 
            // tsm_save
            // 
            tsm_save.Name = "tsm_save";
            tsm_save.Size = new Size(108, 24);
            tsm_save.Text = "保存";
            tsm_save.Click += tsm_save_Click;
            // 
            // lb_protocol
            // 
            lb_protocol.Location = new Point(6, 6);
            lb_protocol.Name = "lb_protocol";
            lb_protocol.Size = new Size(54, 28);
            lb_protocol.TabIndex = 0;
            lb_protocol.Text = "端口:";
            lb_protocol.TextAlign = ContentAlignment.MiddleLeft;
            lb_protocol.Click += lb_port_Click;
            // 
            // cb_protocol
            // 
            cb_protocol.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_protocol.FormattingEnabled = true;
            cb_protocol.Items.AddRange(new object[] { "Uplink(基恩士)" });
            cb_protocol.Location = new Point(66, 7);
            cb_protocol.Name = "cb_protocol";
            cb_protocol.Size = new Size(140, 28);
            cb_protocol.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1175, 698);
            Controls.Add(app_tabs);
            Controls.Add(app_console);
            Controls.Add(menuStrip1);
            Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "PLC-EMU";
            Load += Form1_Load;
            app_tabs.ResumeLayout(false);
            tab_page_1.ResumeLayout(false);
            app_spc_main.Panel1.ResumeLayout(false);
            app_spc_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)app_spc_main).EndInit();
            app_spc_main.ResumeLayout(false);
            pn_tags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_acklist).EndInit();
            pn_tags_bottom.ResumeLayout(false);
            pn_tags_bottom.PerformLayout();
            pn_tags_top.ResumeLayout(false);
            panel3.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox app_console;
        private TabControl app_tabs;
        private TabPage tab_page_1;
        private Panel panel3;
        private ComboBox app_cmb_clients;
        private Label label2;
        private Label app_lb_clientCount;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件ToolStripMenuItem;
        private SplitContainer app_spc_main;
        private TreeView tv_devlist;
        private Panel pn_tags;
        private Panel pn_tags_top;
        private Label lb_port;
        private TextBox tb_port;
        private Button btn_plc_ctl;
        private Panel pn_tags_bottom;
        private DataGridView dgv_acklist;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private ToolStripMenuItem tsm_save;
        private TextBox tb_ip;
        private Label lb_ip;
        private Button btn_dev_chage;
        private Button btn_newTag;
        private Label lb_protocol;
        private ComboBox cb_protocol;
    }
}