using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnomalyDetectionWinForms.Models
{
    public class DeviceDetailsViewModel : RefViewModel
    {
        public Guid? ParentId { get; set; }
        public IEnumerable<SensorRefViewModel> Sensors { get; set; }
        public string ConnectionString { get; set; }
        public string ComPortName { get; set; }
        public byte SerialId { get; set; }
    }
       
    public class SensorRefViewModel : RefViewModel
    {
        public string HardwareId { get; set; }
        public byte CommandId { get; set; }
    }
}
