using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppClima
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string llave = "ad240024eb256d9a648372b6fce20c63";
        private void button1_Click(object sender, EventArgs e)
        {
            bool verificado = verificar();
            if (verificado == false)
            {
                MessageBox.Show("Tienes que llenar todos los formularios.");
            }
            else
            {
                getClima();
            }
        }

        void getClima()
        {
            double temperatura;
            using (WebClient web = new WebClient())
            {
                try
                {
                    string link = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", txtCityName.Text, llave);
                    var json = web.DownloadString(link);

                    Clima.raiz info = JsonConvert.DeserializeObject<Clima.raiz>(json);

                    labcondicion.Text = info.weather[0].main;
                    labdetalles.Text = info.weather[0].description;
                    labsunrise.Text = convertDateTime(info.sys.sunrise).ToShortTimeString();
                    labsunset.Text = convertDateTime(info.sys.sunset).ToShortTimeString();
                    labspeed.Text = info.wind.speed.ToString();
                    labPressure.Text = info.main.pressure.ToString();
                    temperatura = info.main.temp;
                    temperatura = temperatura - 273.15;
                    labtemp.Text = temperatura.ToString() + "Cº";
                    labHum.Text = info.main.humidity.ToString() + "%";
                  

                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
        DateTime convertDateTime(long sec) {
            DateTime dia = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            dia = dia.AddSeconds(sec).ToLocalTime();
            return dia;
                }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool verificar()
        {
            if (String.IsNullOrEmpty(txtCityName.Text))
            {

                return false;
            }
            return true;
        }

        private void txtCityName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("No se puede numeros");
            }
        }
    } 
}
