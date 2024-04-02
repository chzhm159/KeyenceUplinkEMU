using BeetleX;
using BeetleX.EventArgs;
using KeyenceUplinkEMU.entity;
using KeyenceUplinkEMU.utils;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KeyenceUplinkEMU.emu
{
    internal class KVTcpServer : ServerHandlerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KVTcpServer));
        private static IServer server;
        public  bool IsConnected { get; private set; } = false;
        private Form1 win;

        public Action<int> connectionAction;
        public Action<string,bool> onClient;

        public DeviceEntity device { get; set; }

        public void SetWin(Form1 win) {
            this.win = win;
        }
        public bool Start() {
            if (IsConnected || device==null ) {
                return IsConnected;
            }
            try {
                string ip = "0.0.0.0";
                if (!string.IsNullOrEmpty(device.ip)) {
                    ip = device.ip;
                }

                int port = 8501;
                if (!string.IsNullOrEmpty(device.ip)) {
                    port= device.port; 
                }

                server = SocketFactory.CreateTcpServer<KVTcpServer>();
                server.Options.DefaultListen.Port = port;
                server.Options.DefaultListen.Host = ip;
                server.Options.SessionTimeOut = -1;
                server.Options.BufferSize = 1024 * 10;
                server.Handler = this;
                IsConnected = server.Open();
                
            } catch (Exception ex) {
                IsConnected = false;
            }
            if (connectionAction != null) {
                if (IsConnected) {
                    connectionAction(1);
                } else {
                    connectionAction(2);
                }
            }
            return IsConnected;
        }
        public void Stop() {
            try {
                IsConnected = false;
                server.Dispose();
            } catch (Exception e) {
                log.ErrorFormat("关闭异常: {0},{1}", e.Message, e);
            }
            if (connectionAction != null) {
                if (IsConnected) {
                    connectionAction(1);
                } else {
                    connectionAction(2);
                }
            }
        }
        public override void Connected(IServer server, ConnectedEventArgs e) {
            log.ErrorFormat("链接建立成功!: 客户端={0}\r\n", e.Session.ID);
            Notify("链接建立成功!: 客户端={0}\r\n", e.Session.ID);
            
            e.Session.Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            LingerOption myOpts = new LingerOption(true, 0);
            e.Session.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, myOpts);
            base.Connected(server, e);
            if(onClient!=null) {
                string client = string.Format("[{0}]{1}:{2}", e.Session.ID,e.Session.Host,e.Session.Port);
                onClient(client,true);
            }
        }
        public override void Disconnect(IServer server, SessionEventArgs e) {
            log.ErrorFormat("链接断开: 客户端={0}\r\n", e.Session.ID);
            Notify("链接断开: 客户端={0}\r", e.Session.ID);
            IsConnected = false;

            base.Disconnect(server, e);
            if (onClient != null) {
                string client = string.Format("[{0}]{1}:{2}", e.Session.ID, e.Session.Host, e.Session.Port);
                onClient(client, false);
            }
        }

        public override void SessionReceive(IServer server, SessionReceiveEventArgs e) {            
            try {
                OnDataMy(server, e);
                base.SessionReceive(server, e);
                if (e != null && e.Session != null && e.Session.Stream != null) {
                    e.Session.Stream.Flush();
                }

            } catch (Exception ex) {
                log.ErrorFormat("框架通信异常: error={0},{1}", ex.Message, ex.StackTrace);
            }

        }
        private void OnDataMy(IServer server, SessionReceiveEventArgs e) {
            BeetleX.Buffers.PipeStream pipestream = e.Stream.ToPipeStream();
            long len = pipestream.Length;
            string data = pipestream.ReadString((int)pipestream.Length);
            
            log.InfoFormat("收到数据:{0}", string.Join("\r\n", data));
            Notify("收到数据{0}", data);
            if (string.Equals("CR", data) || string.Equals("CR\r", data)) {
                pipestream.WriteLine("OK");
                pipestream.Flush();
                return;
            }
            List<string> cmds = ReadLine2(data);
            cmds.ForEach(c => {
                try {
                    if (!string.IsNullOrEmpty(c) && (data.StartsWith("WRS") || data.StartsWith("WR"))) {
                        UpdateData(c, e.Session);
                        pipestream.WriteLine("OK");
                        pipestream.Flush();
                        return;
                    }
                    // string ack = AppCfg.AckMsg(msg);
                    //BytesHandler ackBytes = new BytesHandler(ack + "\r\n", Encoding.UTF8);
                    BytesHandler ack = ReadData(c);

                    //BytesHandler ackBytes = new BytesHandler(ack + "\r\n", Encoding.UTF8);
                    e.Session.Send(ack);
                } catch (Exception ex) {
                    int id = -1;
                    log.ErrorFormat("数据处理异常: message={2},error={0},{1}", ex.Message, ex.StackTrace, c);
                }
            });
        }
        private string ReadData(string cmd) {
            // RD DM100
            // RDS DM100 1
            bool noAck = (device == null || device.tags == null || device.tags.Count < 1);
            string[] cpos = cmd.Split(" ");
            string ack ="";
            if (noAck && !string.IsNullOrEmpty(cpos[0]) && string.Equals("RD", cpos[0])) {
                return "0000";
            } else if (noAck && !string.IsNullOrEmpty(cpos[0]) && string.Equals("RDS", cpos[0])) {                
                int count = int.Parse(cpos[2]);                
                for(int i = 0;i < count; i++) {
                    ack += "0000 ";
                }
                ack = ack.Trim();
                return ack;
            }
            string addr_dt = cpos[1];
            string addr = addr_dt.Split(".")[0];
            // int count = int.Parse(cpos[2]);
            TagEntity? tag = device.tags.Find(t => {
                return string.Equals(t.addr, addr, StringComparison.OrdinalIgnoreCase);
            });
            bool tagNotExists = (tag == null);
            if  (tagNotExists) {
                tag = new TagEntity();
                tag.addr = addr;
                tag.type = GetDataTypeFromCmd(addr_dt);                
                if (!string.IsNullOrEmpty(cpos[0]) && string.Equals("RD", cpos[0])) {
                    tag.value= "0000";
                } else if (!string.IsNullOrEmpty(cpos[0]) && string.Equals("RDS", cpos[0])) {
                    int count = int.Parse(cpos[2]);
                    for (int i = 0; i < count; i++) {
                        ack += "0000 ";
                    }
                    ack = ack.Trim();
                    tag.value = ack;                    
                }
                this.win.NewTag(device, tag);
                device.tags.Add(tag);
            }
            return tag.value;
        }
        private void UpdateData(string cmd,ISession sess) {
            // WR DM100.U 100
            // WRS DM100.U 3 1 2 3
            if(device==null || device.tags==null || device.tags.Count < 1) {
                return;
            }
            string[] cpos = cmd.Split(" ");
            string addr_dt = cpos[1];
            string addr = addr_dt.Split(".")[0];
            
            TagEntity? tag = device.tags.Find(t => {
                return string.Equals(t.addr, addr, StringComparison.OrdinalIgnoreCase);
            });
            bool tagNotExists = (tag == null);
            if (tagNotExists) {
                tag = new TagEntity();
                tag.addr = addr;
                tag.type = GetDataTypeFromCmd(addr_dt) ;                
            }
            if (string.Equals("WRS", cpos[0], StringComparison.OrdinalIgnoreCase)) {
                int count = int.Parse(cpos[2]);
                if (tagNotExists) tag.count = count;
                List<string> ds = cpos.Skip(3).TakeLast(count).ToList();
                tag.value =string.Join(",", ds);
            } else if (string.Equals("WR", cpos[0], StringComparison.OrdinalIgnoreCase)) {
                string data = cpos[2];
                tag.value = data;
            }
            if (this.win != null) {                
                string client = string.Format("{0}:{1}", sess.Host, sess.Port);                
                this.win.OnWriteData(device, tag);
            }
        }
        private string GetDataTypeFromCmd(string addr) {            
            if (string.IsNullOrEmpty(addr)) {
                return "uint16";
            }
            if (addr.StartsWith("R",StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("B", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("MR", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("LR", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("CR", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("VB", StringComparison.OrdinalIgnoreCase)) {
                return "bool";
            } else if (addr.StartsWith("DM", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("EM", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("FM", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("ZF", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("W", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("TM", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("Z", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("CM", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("VM", StringComparison.OrdinalIgnoreCase)) {
                return "uint16";
            } else if (addr.StartsWith("TC", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("TS", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("CC", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("CS", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("AT", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("T", StringComparison.OrdinalIgnoreCase) ||
                addr.StartsWith("C", StringComparison.OrdinalIgnoreCase)) { 
                return "uint32";
            }else{
                return "uint16";
        }
        }
        private void Notify(string fmt, params object[] args) {
            if (this.win != null) {
                this.win.Notify(fmt, args);
            }
        }
        private string _cache = "";
        private string cmdCache = "";
        private List<string> ReadLine2(string data) {
            List<string> lines = new List<string>();
            if (String.IsNullOrEmpty(data)) {
                return lines;
            }
            cmdCache = cmdCache + data;
            int firstEndIdx = cmdCache.IndexOf("\r");
            if (firstEndIdx < 0) {
                // 没有结束符,表示为半包                
                return lines;
            } else {
                // string[] cmds = tmp.Split("\r");
                int dl = cmdCache.Length, start = 0,len = 0;
                // 存在结束符,开始遍历查找
                for (int i = 0; i < dl; i++) {
                    char c = cmdCache[i];
                    if (c == '\r') {
                        // 找到结束符
                        len = i - start + 1;
                        string pack = cmdCache.Substring(start, len);
                        pack = pack.Replace("\r","").Replace("\n", "").Trim();
                        lines.Add(pack);
                        start = i + 1;
                    }
                }
                cmdCache = cmdCache.Remove(0, start);
                if(cmdCache.Length==1 && cmdCache[0] == '\n') {
                    cmdCache = "";
                }
                return lines;
            }
        }
            private List<string> ReadLine(string data) {
            List<string> lines = new List<string>();
            if (String.IsNullOrEmpty(data)) {
                return lines;
            }

            // 先将缓存中内容拼接当前数据
            string tmp = _cache + data;
            int firstEndIdx = tmp.IndexOf("\r");
            if (firstEndIdx < 0) {
                // 没有结束符,表示为半包
                _cache = _cache + data;
                return lines;
            } else {
                int dl = tmp.Length;
                int start = 0;
                int len = 0;
                // 存在结束符,开始遍历查找
                for (int i = 0; i < dl; i++) {
                    char c = tmp[i];
                    int next = i + 1;
                    if (c == '\r' && next < dl && tmp[next] == '\n') {
                        // 找到结束符
                        len = next - start + 1;
                        string pack = tmp.Substring(start, len);
                        lines.Add(pack);
                        start = next + 1;
                    }
                }
                if (start < dl) {
                    // 如果结束符小于数据长度,表示还有剩余
                    _cache = tmp.Substring(start, (dl - start));
                } else if (start == dl) {
                    _cache = string.Empty;
                }
                return lines;
            }

        }
    }
}
