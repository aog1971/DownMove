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

namespace DownMove
{
    public partial class Form1 : Form
    {
        private string urlDefaul = ""; //"http://www.sample-videos.com/video/mp4/360/big_buck_bunny_360p_1mb.mp4";
        private string rutaHD = "C:\\00\\video-" + DateTime.Now.Ticks + ".mp4";
        private string nameFile = DateTime.Now.Ticks + ".mp4";
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = urlDefaul;
            comboBox1.SelectedItem = "mp4";
             nameFile += "mp4";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //var client = new WebClient();
                //var uri = textBox1.Text;
                //Uri address = new Uri(uri);
                //client.DownloadFileAsync(address, rutaHD );
                loading(true);
                BtnDownload_Click();
            }
            catch(Exception ex) {
                loading(false);
                MessageBox.Show(ex.Message);
            }
            
        }


        private void BtnDownload_Click()
        {
            progressBar1.Value = 0;
            rutaHD = getRuta() ;// "C:\\00\\video-" + DateTime.Now.Ticks + ".mp4";
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(new System.Uri(textBox1.Text),
                rutaHD);
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage <= 10 ? 10 : e.ProgressPercentage;

            if(progressBar1.Value >= 100)
            {
                loading(false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void btnOpenDialogo_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            setFile();
        }

        private void setFile()
        {
            var fileNameFull = txtFile.Text;
            var subName = checkBox1.Checked ? DateTime.Now.Ticks.ToString() :"";
            var tipo = comboBox1.Text;
            var ruta = txtRuta.Text;
             //ruta = ruta.LastIndexOf(@"\") == ruta.Length -1 ? ruta : ruta + @"\";
            if (checkBox1.Checked)
            {
                fileNameFull = $"{fileNameFull}-{subName}.{tipo}";
            }
            else
            {
                fileNameFull = $"{fileNameFull}.{tipo}";
            }
            var rutaMasFile = System.IO.Path.Combine(ruta, fileNameFull);
            lbRutaArchivo.Text =  string.Format("{0}{1}", ruta, fileNameFull);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFile();

        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            setFile();
        }
        private string getRuta()
        {
            return lbRutaArchivo.Text;
        }

        public void loading(bool isShow)
        {
            var value = isShow ? "Descargando, espere..." : "";
            lblLoading.Text = value;

        }
    }
}
