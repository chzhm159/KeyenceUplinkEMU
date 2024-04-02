using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyenceUplinkEMU.entity
{
    public class AckEntity
    {
        public List<DeviceEntity> device { get; set; }
    }
    public class DeviceEntity {
        public string name { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string protocol { get; set; }
        public List<TagEntity> tags { get; set; }
    }
    public class TagEntity
    {
        public string addr { get; set; }
        public string type { get; set; }
        public int count { get; set; }
        public string value { get; set; }
    }
}
