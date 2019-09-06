using AnomalyDetectionWinForms.Models;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnomalyDetectionWinForms.Views
{
    public partial class RS485Form : Form
    {
        internal static RS485Form New(string filename)
        {
            var form = new RS485Form();
            form.Init(filename);
            form.Text = filename;
            return form;
        }

        private void Init(string filename)
        {
        }

        public RS485Form()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Control)
            {
                if (Clipboard.ContainsText())
                {
                    var config = JsonConvert.DeserializeObject<DeviceDetailsViewModel>(Clipboard.GetText());
                    Task.Factory.StartNew(async () => {
                        await Run(config, text => {
                            listBox1.Invoke(new Action(() => listBox1.Items.Add(text)));
                        });
                    });
                }
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        async Task Run(DeviceDetailsViewModel config, Action<string> log)
        {
            var client = DeviceClient.CreateFromConnectionString(config.ConnectionString);

            var port = new SerialPort(config.ComPortName, 57600, Parity.None, 8, StopBits.One);
            port.DataReceived += async (s, e) => {
                var buffer = new byte[port.BytesToRead];
                var read = port.Read(buffer, 0, buffer.Length);
                var id = buffer[0];
                var cmd = buffer[1];
                var cmdlen = buffer[2];
                log($"{read} bytes from {id}: cmd={cmd} len={cmdlen}");

                var sensor = config.Sensors.Single(xx => xx.CommandId + 0x80 == cmd);
                var readValue = System.BitConverter.ToSingle(buffer, 3);
                log($"READ {sensor.Name}={readValue}");
                await Send(client, sensor, readValue);

                //switch (cmd)
                //{
                //    case 0x80 + 0x01:
                //        var readTemperatureValue = System.BitConverter.ToSingle(buffer, 3);
                //        log($"READ REMPERATURE={readTemperatureValue}");
                //        await client.Send("t", "temperature", readTemperatureValue);
                //        break;
                //        break;
                //    case 0x80 + 0x02:
                //        var readHumidityValue = System.BitConverter.ToSingle(buffer, 3);
                //        log($"READ HUMIDITY={readHumidityValue}");
                //        await client.Send("h", "humidity", readHumidityValue);
                //        break;
                //    case 0x80 + 0x04:
                //        var readFlexValue = System.BitConverter.ToSingle(buffer, 3);
                //        log($"READ FLEX={readFlexValue}");
                //        await client.Send("fl", "flex", readFlexValue);
                //        break;
                //    case 0x80 + 0x08:
                //        var readForceValue = System.BitConverter.ToSingle(buffer, 3);
                //        log($"READ FORCE={readForceValue}");
                //        await client.Send("fr", "force", readForceValue);
                //        break;
                //}
            };
            port.Open();

            var requestT1 = new byte[] { 1, 1, 0 };
            var requestH1 = new byte[] { 1, 2, 0 };
            var requestFL1 = new byte[] { 1, 4, 0 };
            var requestFR1 = new byte[] { 1, 8, 0 };

            try
            {
                while (true)
                {
                    //log("Sending T1");
                    //port.Write(requestT1, 0, requestT1.Length);
                    //await Task.Delay(5000);

                    //log("Sending H1");
                    //port.Write(requestH1, 0, requestH1.Length);
                    //await Task.Delay(5000);

                    //log("Sending FL1");
                    //port.Write(requestFL1, 0, requestFL1.Length);
                    //await Task.Delay(5000);

                    //log("Sending FR1");
                    //port.Write(requestFR1, 0, requestFR1.Length);
                    //await Task.Delay(5000);

                    foreach (var sensor in config.Sensors)
                    {
                        log($"Sending {sensor.Name}");
                        port.Write(new byte[] { config.SerialId, sensor.CommandId, 0 }, 0, 3);
                        await Task.Delay(5000);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                port.Close();
            }
        }

        async Task Send(DeviceClient client, SensorRefViewModel viewModel, float value)
        {
            var bytes = Encoding.UTF8.GetBytes($"{value}");
            var eventMessage = new Microsoft.Azure.Devices.Client.Message(bytes);
            eventMessage.Properties.Add("DigitalTwins-Telemetry", "1.0");
            eventMessage.Properties.Add("DigitalTwins-SensorHardwareId", $"{viewModel.HardwareId}");
            eventMessage.Properties.Add("CreationTimeUtc", DateTime.UtcNow.ToString("o", System.Threading.Thread.CurrentThread.CurrentCulture));
            eventMessage.Properties.Add("x-ms-client-request-id", Guid.NewGuid().ToString());
            eventMessage.Properties.Add($"x-type", viewModel.Name);
            eventMessage.Properties.Add($"x-value", $"{value}");

            await client.SendEventAsync(eventMessage);
        }
    }
}
