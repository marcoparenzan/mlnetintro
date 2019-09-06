using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDetectionWinForms.Models
{
    public abstract class RefViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BlobRefViewModel> Blobs { get; set; }
    }
    public class BlobRefViewModel : RefViewModel { }

}
