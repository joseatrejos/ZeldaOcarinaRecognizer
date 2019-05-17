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
        string silencio = "";
        bool play = false;
        string epona = "w a d w a d ", saria = "s d a s d a ", zelda = "a w d a w d ", sun = "d s w d s w ", storms = "e s w e s w ", 
            time = "d e s d e s ", healing = "a d s a d s ", spirit = "e s e d s e ", light = "w d w d a w ", forest ="e w a d a d ",
            // Otra cantidad de notas
            fuego = "s e s e d s d s ", agua = "e s d d a ";

        private void Vid_MediaEnded(object sender, RoutedEventArgs e)
        {
            ocultar_Todo();
            Ocarina.Visibility = Visibility.Visible;
            Notas.Visibility = Visibility.Visible;
            LtEscuchando.Visibility = Visibility.Visible;
            lbl_Nota.Text = "";
        }

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Tick += Timer_Tick;

            cronometro = new Stopwatch();
            MediaElement vidEpona = new MediaElement();
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
                    if (lbl_Nota.Text.Length >= 12)
                    {
                        string cancion = lbl_Nota.Text.Substring(lbl_Nota.Text.Length - 12, 12);
                        if (cancion == epona)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\epona.wmv", UriKind.Relative);
                            Videos.Play();
                            Videos.Visibility = Visibility.Visible;

                            // Letrero
                            LtEpona.Visibility = Visibility.Visible;
                        }
                        else if (cancion == zelda)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\zelda.wmv", UriKind.Relative);
                            Videos.Play();
                            
                            Videos.Visibility = Visibility.Visible;
                            LtNana.Visibility = Visibility.Visible;
                        }
                        else if (cancion == saria)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\saria.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtSaria.Visibility = Visibility.Visible;
                        }
                        else if (cancion == sun)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\sun.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtSol.Visibility = Visibility.Visible;
                        }
                        else if (cancion == storms)
                        {

                            ocultar_Todo();
                            // Video Playback
                            Videos.Source = new Uri(@"Video\storms.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtTormentas.Visibility = Visibility.Visible;
                        }
                        else if (cancion == time)
                        {
                            ocultar_Todo();
                            // Video Playback
                            Videos.Source = new Uri(@"Video\time.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtTiempo.Visibility = Visibility.Visible;

                        }
                        else if (cancion == light)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\light.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtLuz.Visibility = Visibility.Visible;
                        }
                        else if (cancion == spirit)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\spirit.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtEspiritu.Visibility = Visibility.Visible;
                        }
                        else if (cancion == healing)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\healing.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtSanacion.Visibility = Visibility.Visible;

                        }
                        else if (cancion == forest)
                        {
                            // Video Playback
                            ocultar_Todo();
                            Videos.Source = new Uri(@"Video\forest.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtBosque.Visibility = Visibility.Visible;
                        }
                    }
                    if (lbl_Nota.Text.Length >= 16)
                    {
                        string cancion = lbl_Nota.Text.Substring(lbl_Nota.Text.Length - 16, 16);
                        if (cancion == fuego)
                        {
                            ocultar_Todo();
                            // Video Playback
                            Videos.Source = new Uri(@"Video\Fire.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtFuego.Visibility = Visibility.Visible;
                        }
                    }
                    if (lbl_Nota.Text.Length >= 10)
                    {
                        string cancion = lbl_Nota.Text.Substring(lbl_Nota.Text.Length - 10, 10);
                        if (cancion == agua)
                        {
                            ocultar_Todo();
                            // Video Playback
                            Videos.Source = new Uri(@"Video\Water.wmv", UriKind.Relative);
                            Videos.Play();

                            Videos.Visibility = Visibility.Visible;
                            LtAgua.Visibility = Visibility.Visible;
                        }
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
            btn_Ocarina.Visibility = Visibility.Hidden;
            Ocarina.Visibility = Visibility.Visible;
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
            play = true;
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

                if (frecuenciaFundamental >= 1160 && frecuenciaFundamental <= 1192)
                {
                    letraActual = "w ";
                    BtnArriba.Visibility = Visibility.Visible;
                    Ocarina.Visibility = Visibility.Hidden;
                    btn_Ocarina.Visibility = Visibility.Hidden;
                    BtnA.Visibility = Visibility.Hidden;
                    BtnAbajo.Visibility = Visibility.Hidden;
                    BtnDerecha.Visibility = Visibility.Hidden;
                    BtnIzquierda.Visibility = Visibility.Hidden;
                }
                else if (frecuenciaFundamental >= 976 && frecuenciaFundamental <= 998)
                {
                    letraActual = "a ";
                    BtnIzquierda.Visibility = Visibility.Visible;
                    BtnArriba.Visibility = Visibility.Hidden;
                    Ocarina.Visibility = Visibility.Hidden;
                    btn_Ocarina.Visibility = Visibility.Hidden;
                    BtnA.Visibility = Visibility.Hidden;
                    BtnAbajo.Visibility = Visibility.Hidden;
                    BtnDerecha.Visibility = Visibility.Hidden;
                }
                else if (frecuenciaFundamental >= 872 && frecuenciaFundamental <= 891)
                {
                    letraActual = "d ";
                    BtnDerecha.Visibility = Visibility.Visible;
                    BtnArriba.Visibility = Visibility.Hidden;
                    Ocarina.Visibility = Visibility.Hidden;
                    btn_Ocarina.Visibility = Visibility.Hidden;
                    BtnA.Visibility = Visibility.Hidden;
                    BtnAbajo.Visibility = Visibility.Hidden;
                    BtnIzquierda.Visibility = Visibility.Hidden;
                }
                else if (frecuenciaFundamental >= 693 && frecuenciaFundamental <= 713)
                {
                    letraActual = "s ";
                    BtnAbajo.Visibility = Visibility.Visible;
                    BtnArriba.Visibility = Visibility.Hidden;
                    Ocarina.Visibility = Visibility.Hidden;
                    btn_Ocarina.Visibility = Visibility.Hidden;
                    BtnA.Visibility = Visibility.Hidden;
                    BtnDerecha.Visibility = Visibility.Hidden;
                    BtnIzquierda.Visibility = Visibility.Hidden;
                }
                else if (frecuenciaFundamental >= 583 && frecuenciaFundamental <= 595)
                {
                    letraActual = "e ";
                    BtnA.Visibility = Visibility.Visible;
                    BtnArriba.Visibility = Visibility.Hidden;
                    Ocarina.Visibility = Visibility.Hidden;
                    btn_Ocarina.Visibility = Visibility.Hidden;
                    BtnAbajo.Visibility = Visibility.Hidden;
                    BtnDerecha.Visibility = Visibility.Hidden;
                    BtnIzquierda.Visibility = Visibility.Hidden;
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
                Videos.IsMuted = true;
            }
            ocultar_Todo();

            btn_Reconocer.Visibility = Visibility.Visible;
            btn_Ocarina.Visibility = Visibility.Visible;

            lbl_Nota.Text = "";
            lbl_Frecuencia.Text = "0 Hz";
        }

        private void ocultar_Todo()
        {
            Videos.Visibility = Visibility.Hidden;     // Videos
            LtEscuchando.Visibility = Visibility.Hidden;    // Letreros, Notas y Ocarina
            btn_Reconocer.Visibility = Visibility.Hidden;
            Ocarina.Visibility = Visibility.Hidden;
            btn_Ocarina.Visibility = Visibility.Hidden;
            Notas.Visibility = Visibility.Hidden;
            BtnA.Visibility = Visibility.Hidden;            // Botones
            BtnArriba.Visibility = Visibility.Hidden;
            BtnAbajo.Visibility = Visibility.Hidden;
            BtnDerecha.Visibility = Visibility.Hidden;
            BtnIzquierda.Visibility = Visibility.Hidden;
            LtEpona.Visibility = Visibility.Hidden;         // Letreros Canciones
            LtFuego.Visibility = Visibility.Hidden;
            LtAgua.Visibility = Visibility.Hidden;
            LtSaria.Visibility = Visibility.Hidden;
            LtSol.Visibility = Visibility.Hidden;
            LtTormentas.Visibility = Visibility.Hidden;
            LtTiempo.Visibility = Visibility.Hidden;
            LtSanacion.Visibility = Visibility.Hidden;
            LtNana.Visibility = Visibility.Hidden;
            LtLuz.Visibility = Visibility.Hidden;
            LtEspiritu.Visibility = Visibility.Hidden;
            LtBosque.Visibility = Visibility.Hidden;

        }
    }
}
