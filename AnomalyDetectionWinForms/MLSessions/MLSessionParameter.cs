using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyDetectionWinForms.MLSessions
{
    public class MLSessionParameter
    {
        public int Value { get; set; }
        public RangeType Range { get; set; }
        public string Label { get; set; }
    }
}
