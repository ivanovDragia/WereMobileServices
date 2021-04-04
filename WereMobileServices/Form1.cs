using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WereMobileServices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ServiceInit();
        
        }

        WebServiceHost host = null;

        private void ServiceInit()
        {
            statusBox.ForeColor = Color.White;
            statusBox.Items.Add("Starting up...");
            try
            {
                statusBox.Items.Add("Loading  up...");

                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                host = new WebServiceHost(typeof(WereMobileRest),
                 new Uri("http://localhost:9090"));

                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IWereMobileRest), new WebHttpBinding(), "");

                ServiceDebugBehavior beh = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                beh.HttpHelpPageEnabled = true;

                host.Open();
            }
            catch (Exception e)
            {
                statusBox.Items.Add("Starting failed!");
                statusBox.Items.Add(e.Message);
            }

            statusBox.Items.Add("Server running!");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
        }
    }
}
