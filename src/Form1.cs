using KeyenceUplinkEMU.emu;
using KeyenceUplinkEMU.entity;
using KeyenceUplinkEMU.utils;
using log4net;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KeyenceUplinkEMU
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Form1));
        private List<KVTcpServer> emuList = new List<KVTcpServer>();
        public Form1() {
            InitializeComponent();
            btn_newTag.Visible = false;
            AppCfg.LoadAck();
            KVTcpServer kv = new KVTcpServer();
            kv.SetWin(this);
            kv.onClient = OnClientEvent;
            InitContextMenuStrip();
        }
        bool kv_plc_connected = false;
        private void OnPlcConnectEvent(int state) {
            this.BeginInvoke(new Action(() => {
                //if (state == 1) {
                //    btn_plc_ctl.BackColor = Color.Gray;
                //    btn_plc_ctl.Text = "未启动";

                //    btn_plc_ctl.BackColor = Color.ForestGreen;
                //    btn_plc_ctl.ForeColor = Color.White;
                //    btn_plc_ctl.Text = "运行中";
                //    kv_plc_connected = true;
                //} else {
                //    app_btn_start.BackColor = Color.Red;
                //    app_btn_start.ForeColor = Color.White;
                //    app_btn_start.Text = "已停止";
                //    kv_plc_connected = false;
                //}
            }));
        }
        private void OnClientEvent(string client, bool add) {
            this.BeginInvoke(new Action(() => {
                if (add) {
                    app_cmb_clients.Items.Add(client);
                    app_cmb_clients.SelectedIndex = 0;

                } else {
                    app_cmb_clients.Items.Remove(client);
                }
                this.app_lb_clientCount.Text = app_cmb_clients.Items.Count.ToString();

            }));
        }


        private void Form1_Load(object sender, EventArgs e) {
            InitDevTree();
        }

        private void InitDevTree() {
            entity.AckEntity ackM = AppCfg.AckMap;
            if (ackM == null || ackM.device == null || ackM.device.Count < 1) {
                return;
            }

            TreeNode devRoot = tv_devlist.Nodes[0];

            devRoot.Tag = ackM.device;

            ackM.device.ForEach(device => {
                TreeNode devNode = new TreeNode();
                KVTcpServer kv = new KVTcpServer();
                kv.SetWin(this);
                // kv.connectionAction = OnPlcConnectEvent;
                kv.onClient = OnClientEvent;
                kv.device = device; ;
                emuList.Add(kv);
                devNode.Tag = device;
                if (!string.IsNullOrEmpty(device.name)) {
                    devNode.Text = device.name;
                } else {
                    devNode.Text = string.Format("{0}:{1}", device.ip, device.port);
                }
                devRoot.Nodes.Add(devNode);
            });

            if (tv_devlist.Nodes.Count > 1) {
                tv_devlist.SelectedImageIndex = 1;
            } else {
                tv_devlist.SelectedImageIndex = 0;
            }
            tv_devlist.ExpandAll();
            tv_devlist.Refresh();
        }
        ContextMenuStrip devCtxMenu;
        private void InitContextMenuStrip() {

            devCtxMenu = new ContextMenuStrip();


            ToolStripMenuItem prop = new ToolStripMenuItem("配置信息");
            ToolStripMenuItem add = new ToolStripMenuItem("添加设备");
            ToolStripMenuItem del = new ToolStripMenuItem("删除");

            prop.Click += new EventHandler(onCtxItemEditHandler);
            add.Click += new EventHandler(onCtxItemAddHandler);
            del.Click += new EventHandler(onCtxItemDelHandler);
            //ToolStripMenuItem tmiMoveRouteStation = new ToolStripMenuItem("更改位置");
            //tmiMoveRouteStation.Click += new EventHandler(tmiMoveRouteStation_Click);
            //ToolStripMenuItem tmiDeleRouteStation = new ToolStripMenuItem("删除飞行站点");
            //tmiDeleRouteStation.Click += new EventHandler(tmiDeleRouteStation_Click);


            devCtxMenu.Items.Add(prop);
            devCtxMenu.Items.Add(add);
            devCtxMenu.Items.Add(del);
            // tv_devlist.ContextMenuStrip = devCtxMenu;
            //devCtxMenu.Items.Add(tmiMoveRouteStation);
            //devCtxMenu.Items.Add(tmiDeleRouteStation);

        }

        public void Notify(string fmt, params object[] args) {
            this.BeginInvoke(NotifyHandler, fmt, args);
        }
        int maxLinds = 1000;
        public void NotifyHandler(string fmt, params object[] args) {
            string ts = DateTime.Now.ToString("F");
            //Glb_Msg.BackColor = color;
            //Glb_Msg.AckText = msg;
            string msg = string.Format(fmt, args);

            this.app_console.AppendText(string.Format("{0} {1}\r", ts, msg));
            this.app_console.ScrollToCaret();

            // 最多显示500行.
            if (this.app_console.Lines.Length > maxLinds) {
                string[] lines = this.app_console.Lines;
                string[] remain = lines.Skip(maxLinds / 2).Take(maxLinds / 2).ToArray();
                this.app_console.Lines = remain;
                //让文本框获取焦点  
                this.app_console.Focus();
                //设置光标的位置到文本尾  
                this.app_console.Select(this.app_console.TextLength - 1, 0);
                //滚动到控件光标处  
                this.app_console.ScrollToCaret();
            }
        }


        private void tv_devlist_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Tag == null || string.Equals("设备列表", e.Node.Text)) {
                return;
            }
            btn_newTag.Visible = true;
            btn_plc_ctl.Visible = true;
            btn_dev_chage.Visible = true;
            cb_protocol.SelectedIndex = 0;
            dgv_acklist.Rows.Clear();
            DeviceEntity dev = e.Node.Tag as entity.DeviceEntity;
            btn_plc_ctl.Tag = dev;
            KVTcpServer emu = GetEMUServer(dev.ip, dev.port);
            if (emu != null && emu.IsConnected) {
                btn_plc_ctl.Text = "已启动";
                btn_plc_ctl.BackColor = Color.ForestGreen;
                btn_plc_ctl.ForeColor = Color.White;

            } else {
                btn_plc_ctl.BackColor = Color.Gray;
                btn_plc_ctl.Text = "未启动";
            }
            tb_ip.Text = dev.ip;
            tb_port.Text = dev.port.ToString();
            dev.tags.ForEach(t => {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewCell addr = new DataGridViewTextBoxCell();
                addr.Value = t.addr;
                DataGridViewCell dtcbx = GetDataType(t.type);
                DataGridViewCell count = new DataGridViewTextBoxCell();
                count.Value = t.count;
                DataGridViewCell value = new DataGridViewTextBoxCell();
                value.Value = t.value;

                row.Tag = t;
                row.Cells.Add(addr);
                row.Cells.Add(dtcbx);
                row.Cells.Add(count);
                row.Cells.Add(value);
                dgv_acklist.Rows.Add(row);
            });

        }
        private DataGridViewComboBoxCell GetDataType(string dt) {
            DataGridViewComboBoxCell type = new DataGridViewComboBoxCell();

            type.Items.Add("int16");
            type.Items.Add("uint16");
            type.Items.Add("int32");
            type.Items.Add("uint32");
            type.Items.Add("string");
            type.Items.Add("bool");
            type.Items.Add("hex");
            switch (dt) {
                case "int16":
                    type.Value = "int16"; break;
                case "uint16":
                    type.Value = "uint16"; break;
                case "int32":
                    type.Value = "int32"; break;
                case "uint32":
                    type.Value = "uint32"; break;
                case "string":
                    type.Value = "string"; break;
                case "bool":
                    type.Value = "bool"; break;
                case "hex":
                    type.Value = "hex"; break;
            }
            return type;
        }
        private void lb_port_Click(object sender, EventArgs e) {

        }
        private KVTcpServer GetEMUServer(string ip, int port) {
            string c = string.Format("{0}:{1}", ip, port);
            return emuList.Find(s => {
                string emu = string.Format("{0}:{1}", s.device.ip, s.device.port);
                return string.Equals(emu, c, StringComparison.OrdinalIgnoreCase);
            })!;

        }
        private void btn_plc_ctl_Click(object sender, EventArgs e) {
            // DeviceEntity dev = e.Node.Tag as entity.DeviceEntity;
            DeviceEntity dev = btn_plc_ctl.Tag as DeviceEntity;
            if (dev == null) {
                return;
            }
            KVTcpServer emu = GetEMUServer(dev.ip, dev.port);
            if (emu == null) {
                return;
            }
            if (emu.IsConnected) {
                emu.Stop();
                btn_plc_ctl.BackColor = Color.Gray;
                btn_plc_ctl.Text = "未启动";
            } else {
                bool suc = emu.Start();
                if (suc) {
                    btn_plc_ctl.Text = "已启动";
                    btn_plc_ctl.BackColor = Color.ForestGreen;
                    btn_plc_ctl.ForeColor = Color.White;
                }
            }
        }


        private void dgv_acklist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            //TagEntity tag = new TagEntity();
            //tag.addr = addr;
            //tag.type = GetDataTypeFromCmd(addr_dt);
            //log.DebugFormat("新行已添加");
        }

        private void dgv_acklist_Validated(object sender, EventArgs e) {
            log.DebugFormat("经过验证");
        }

        private void dgv_acklist_EditModeChanged(object sender, EventArgs e) {

        }

        private void dgv_acklist_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            log.DebugFormat("属性值发生变化:{0}", e.ColumnIndex);
            int rowNum = e.RowIndex;
            int colNum = e.ColumnIndex;
            if (rowNum < 0 || colNum < 0) {
                return;
            }

            DataGridViewRow row = dgv_acklist.Rows[rowNum];
            if (row.Tag == null) {
                return;
            }
            TagEntity? tag = row.Tag as TagEntity;
            DataGridViewCell cell = row.Cells[colNum];
            object? v = cell.Value;            
            if (v==null) {
                Notify("不能为空");
                return;
            }
            switch (colNum) {
                case 0:
                    UpdateAddr(tag, v);
                    break;
                case 1:
                    UpdateType(tag, v);
                    break;
                case 2:
                    UpdateCount(tag, v);
                    break;
                case 3:
                    UpdateValue(tag, v, row.Cells[colNum]);
                    break;
            }            
        }
        private void UpdateValue(TagEntity tag, object v, DataGridViewCell cell) {
            string strV = (string)v;
            strV = strV.Trim();
            if (string.IsNullOrEmpty(strV)) {
                Notify("[数值]不能为空字符");
                return;
            }
            string[] vs = strV.Split(',');
            for(int i=0; i<vs.Length; i++) {
                string s = vs[i] = vs[i].Trim();
                if (string.IsNullOrEmpty(s)) continue;
                if (tag.type.Contains("int16")) {
                    vs[i] = s.PadLeft(4,'0');
                }else if (tag.type.Contains("int32")) {
                    vs[i] = s.PadLeft(8, '0');
                }
            }

            string formatedV = string.Join(",", vs);
            cell.Value = formatedV;
            tag.value = formatedV;
        }

        private void UpdateCount(TagEntity tag, object v) {
            try {
                int count = (int)v;
                if (count < 0) {
                    Notify("数量必须大于1!");
                    return;
                }
                tag.count = count;
            } catch(Exception e) {
                Notify("数量必须是数字,且要大于1");
                log.ErrorFormat("数据数量配置异常:{0},{1}",e.Message,e.StackTrace);   
            }
        }
        private void UpdateType(TagEntity tag,object v) {
            string type = (string)v;
            if (type.StartsWith("请选择")){
                Notify("请选择数据类型!");
                return;
            }
            tag.type = type;
        }
        private void UpdateAddr(TagEntity tag, object v) {
            string? addr = v as string;
            addr = addr.Trim();
            if (string.IsNullOrEmpty(addr)) {
                Notify("[地址]不能为空");
                return;
            }
            tag.addr = addr;
        }

        internal void OnWriteData(DeviceEntity dev, TagEntity tag) {
            if (btn_plc_ctl.Tag == dev) {
                DataGridViewRowCollection rows = dgv_acklist.Rows;
                bool isNew = true;
                foreach (DataGridViewRow row in rows) {
                    if (row.Tag == tag) {
                        isNew = false;
                        log.InfoFormat("addr={0},value={1}", tag.addr, tag.value);
                        row.Cells[3].Value = tag.value;
                    }
                }
                if (isNew) {
                    NewTag(dev, tag);
                }
            }
        }
        internal void NewTag(DeviceEntity dev, TagEntity tag) {
            this.BeginInvoke(new Action(() => {
                dev.tags.Add(tag);
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewCell addr = new DataGridViewTextBoxCell();
                addr.Value = tag.addr;
                DataGridViewCell dtcbx = GetDataType(tag.type);
                DataGridViewCell count = new DataGridViewTextBoxCell();
                count.Value = tag.count;
                DataGridViewCell value = new DataGridViewTextBoxCell();
                value.Value = tag.value;

                row.Tag = tag;
                row.Cells.Add(addr);
                row.Cells.Add(dtcbx);
                row.Cells.Add(count);
                row.Cells.Add(value);
                dgv_acklist.Rows.Add(row);
            }));
        }

        private void tsm_save_Click(object sender, EventArgs e) {
            bool suc = AppCfg.SaveAck();
            if (suc) {
                MessageBox.Show("当前状态已经保存在保存成功");
            }
        }

        private void tv_devlist_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button != MouseButtons.Right) return;
            if (e.Node.Parent == null || e.Node == null) return;
            tv_devlist.SelectedNode = e.Node;
            devCtxMenu.Tag = e.Node;
            devCtxMenu.Show(tv_devlist, e.X, e.Y);
            // devCtxMenu.Show();
        }
        private void onCtxItemEditHandler(object? sender, EventArgs e) {
            TreeNode clickNode = devCtxMenu.Tag as TreeNode;
            DeviceEntity dev = clickNode.Tag as DeviceEntity;
            log.DebugFormat("点击编辑,{0},{1}", clickNode.Text, dev.ip);
        }
        private void onCtxItemAddHandler(object? sender, EventArgs e) {
            log.DebugFormat("点击添加");
        }
        private void onCtxItemDelHandler(object? sender, EventArgs e) {
            // tv_devlist.GetNodeAt()
            log.DebugFormat("点击删除");
        }

        private void btn_dev_chage_Click(object sender, EventArgs e) {
            if (btn_plc_ctl.Tag == null) {
                MessageBox.Show("修改失败!");
            };
            try {
                DeviceEntity dev = btn_plc_ctl.Tag as DeviceEntity;
                dev.ip = tb_ip.Text;
                dev.port = int.Parse(tb_port.Text);
                MessageBox.Show("修改成功");
            } catch (Exception ex) {
                MessageBox.Show(string.Format("修改失败,监听地址必须为IP格式,或者0.0.0.0表示全部.端口必须为数字!"));
            }


        }

        private void btn_newTag_Click(object sender, EventArgs e) {
            DeviceEntity dev = btn_plc_ctl.Tag as entity.DeviceEntity;
            if (dev == null) {
                MessageBox.Show("请先选择一个设备,然后再添加新点位");
                return;
            }
            TagEntity tag = new TagEntity();
            tag.addr = "-";
            dev.tags.Add(tag);
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell addr = new DataGridViewTextBoxCell();
            addr.Value = tag.addr;
            DataGridViewCell dtcbx = GetDataType(tag.type);
            DataGridViewCell count = new DataGridViewTextBoxCell();
            count.Value = tag.count;
            DataGridViewCell value = new DataGridViewTextBoxCell();
            value.Value = tag.value;

            row.Tag = tag;
            row.Cells.Add(addr);
            row.Cells.Add(dtcbx);
            row.Cells.Add(count);
            row.Cells.Add(value);
            dgv_acklist.Rows.Add(row);
        }
    }
}