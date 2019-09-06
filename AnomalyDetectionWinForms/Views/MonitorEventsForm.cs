using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms.Views
{
    public partial class MonitorEventsForm : Form
    {
        internal static MonitorEventsForm New(string filename)
        {
            var form = new MonitorEventsForm();
            form.Init(filename);
            form.Text = filename;
            return form;
        }

        private void Init(string filename)
        {
        }

        public MonitorEventsForm()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R && e.Control)
            {
                Task.Factory.StartNew(async () =>
                {
                    await Run(text =>
                    {
                        listBox1.Invoke(new Action(() =>
                        {
                            listBox1.Items.Add(text);
                        }));

                    });
                });
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        async Task Run(Action<string> log)
        {
            var list = new List<Task>();
            var client = EventHubClient.CreateFromConnectionString("Endpoint=sb://iothub-ns-mpdigitalt-1659081-a90896d2f5.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=0O/RFT1boqPCVLWY6z7fyeyjeFW8F0N/uVPOsxfwylo=;EntityPath=mpdigitaltwins101");
            var info = await client.GetRuntimeInformationAsync();
            foreach (var pi in info.PartitionIds)
            {
                var r = client.CreateReceiver("$Default", pi, EventPosition.FromStart());
                list.Add(Task.Factory.StartNew(async x =>
                {
                    var rr = x as PartitionReceiver;
                    while (true)
                    {
                        var evts = await rr.ReceiveAsync(1);
                        log($"Partition {rr.PartitionId}");
                        foreach (var evt in evts)
                        {
                            foreach (var p in evt.Properties)
                            {
                                log($"{p.Key}={p.Value}");
                            }
                            //evt.SystemProperties.Offset;
                        }
                    }
                }, r));
            }

            await Task.Delay(Timeout.Infinite);
        }
    }
}
