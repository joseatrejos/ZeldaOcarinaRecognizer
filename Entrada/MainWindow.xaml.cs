using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.Dsp;
using System.Windows.Threading;
using System.Diagnostics;

namespace Entrada
{
    public partial class MainWindow : Window
    {
        WaveIn waveIn;
        Stopwatch cronometro;
        float frecuenciaFundamental = 0.0f;
        DispatcherTimer timer;
        string letraAnterior = "";
        string letraActual = "";
        bool play = false;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Tick += Timer_Tick;

            cronometro = new Stopwatch();

            LlenarComboDispositivos();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (letraActual != "" && letraActual == letraAnterior)
            {
                // Evaluar si ya pasó un segundo
                if (cronometro.ElapsedMilliseconds >= 50)
                {
                    lbl_Nota.AppendText(letraActual + "");
                    letraActual = "";
                    cronometro.Restart();
                    if(lbl_Nota.Text.Length >= 4)
                    {
                        string eo = lbl_Nota.Text.Substring(lbl_Nota.Text.Length - 4, 4);
                        if (eo == "E O ")
                            lbl_EO.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                cronometro.Restart();
            }
        }

        public void LlenarComboDispositivos()
        {
            for(int i = 0; i < WaveIn.DeviceCount; i++)
            {
                // La variable tipo WaveInCapabilities almacena los dispositivos en la iteración [i] hasta guardar todos los aparatos de entrada
                WaveInCapabilities capacidades = WaveIn.GetCapabilities(i);

                // Ahora agregamos a los "items" de nuestro combo box el Nombre del dispositivo almacenado en capacidades[i]
                cmb_Dispositivos.Items.Add(capacidades.ProductName);
            }
            cmb_Dispositivos.SelectedIndex = 0;
        }

        private void btn_Iniciar_Click(object sender, RoutedEventArgs e)
        {
            play = true;

            btn_Reconocer.Visibility = Visibility.Hidden;
            LtEscuchando.Visibility = Visibility.Visible;
            Notas.Visibility = Visibility.Visible;

            timer.Start();

            waveIn = new WaveIn();

            // Formato de Audio (Sample Rate, Bit Depth, Channels)
            waveIn.WaveFormat = new WaveFormat(44100, 16, 1);

            // Buffer
            waveIn.BufferMilliseconds = 250;

            // ¿Qué hacer cuando hay muestras disponibles?
            waveIn.DataAvailable += WaveIn_DataAvailable;

            waveIn.StartRecording();
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesGrabados = e.BytesRecorded;
            float acumulador = 0.0f;

            double numeroDeMuestras = bytesGrabados / 2;
            int exponente = 1;
            int numeroDeMuestrasComplejas = 0;
            int bitsMaximos = 0;

            do
            {
                bitsMaximos = (int)Math.Pow(2, exponente);
                exponente++;
            } while (bitsMaximos < numeroDeMuestras);

            numeroDeMuestrasComplejas = bitsMaximos / 2;
            exponente -= 2;

            Complex[] señalCompleja = new Complex[numeroDeMuestrasComplejas];


            // Aquí se transforman 2 bytes separados en una muestra de 16 bits
            for (int i = 0; i < bytesGrabados; i += 2)
            {                                           // Este "OR" revisa ambos parámetros para ver si se cumple la condición
                short muestra = (short)(buffer[i + 1] << 8 | buffer[i]);

                /* 1. Se toma el segundo byte y anteponemos 8 ceros al principio
                   2. Se hace un OR (matemática discreta) con el primer byte, al cual automáticamente se le llenan los 8 ceros 
                   del principio con los últimos 8 valores del segundo byte */

                float muestra32bits = (float)(muestra / 32768.0f);
                acumulador += Math.Abs(muestra32bits);

                if (i / 2 < numeroDeMuestrasComplejas)
                {
                    señalCompleja[i / 2].X = muestra32bits;
                }
            }

            /* Puesto que cada muestra son 2 bytes, divides entre 2 el número de bytes grabados para dividir lo acumulado entre
               el número de muestras (para obtener el promedio) */
            float promedio = acumulador / (bytesGrabados / 2.0f);
            sld_Microfono.Value = (double)promedio;

            if (promedio > 0)
            {
                FastFourierTransform.FFT(true, exponente, señalCompleja);
                float[] valoresAbsolutos = new float[señalCompleja.Length];
                for (int i = 0; i < señalCompleja.Length; i++)
                {                                               // Para obtener el valor absoluto de un número complejo se necesita sacar la raíz cuadrada
                    valoresAbsolutos[i] = (float)Math.Sqrt(     // de la suma de los cuadrados tanto de la parte imaginaria como de la parte real   |a + bi|
                        (señalCompleja[i].X * señalCompleja[i].X) + (señalCompleja[i].Y * señalCompleja[i].Y));
                }

                // El arreglo valoresAbsolutos se vuelve una lista indexada (posición de cada valor del arreglo) y jalamos el valor más alto
                int indiceSeñalConMasPresencia = valoresAbsolutos.ToList().IndexOf(valoresAbsolutos.Max());

                frecuenciaFundamental = (float)indiceSeñalConMasPresencia * (float)waveIn.WaveFormat.SampleRate / (float)valoresAbsolutos.Length;

                letraAnterior = letraActual;

                if (frecuenciaFundamental >= 1170 && frecuenciaFundamental <= 1195)
                {
                    letraActual = "Arriba ";
                }
                else if (frecuenciaFundamental >= 965 && frecuenciaFundamental <= 1005)
                {
                    letraActual = "Izquierda ";
                }
                else if (frecuenciaFundamental >= 865 && frecuenciaFundamental <= 895)
                {
                    letraActual = "Derecha ";
                }
                else if (frecuenciaFundamental >= 685 && frecuenciaFundamental <= 710)
                {
                    letraActual = "Abajo ";
                }
                else if (frecuenciaFundamental >= 575 && frecuenciaFundamental <= 600)
                {
                    letraActual = "A ";
                }
                else
                    letraActual = "";

                lbl_Frecuencia.Text = frecuenciaFundamental.ToString("f") + " Hz";
            }
        }

        private void btn_Detener_Click(object sender, RoutedEventArgs e)
        {
            if (play == true)
            {
                waveIn.StopRecording();
            }
            btn_Reconocer.Visibility = Visibility.Visible;
            LtEscuchando.Visibility = Visibility.Hidden;
            LtFuego.Visibility = Visibility.Hidden;
            LtAgua.Visibility = Visibility.Hidden;
            LtEpona.Visibility = Visibility.Hidden;
            LtSaria.Visibility = Visibility.Hidden;
            Notas.Visibility = Visibility.Hidden;
            lbl_Nota.Clear();
        }
    }
}
