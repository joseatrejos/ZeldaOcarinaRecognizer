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
using System.Threading;
using System.Diagnostics;

namespace Entrada
{
    public partial class MainWindow : Window
    {
        WaveIn waveIn;
        Stopwatch cronometro;
        // TimeSpan tiempoAnterior;

        float frecuenciaFundamental = 0.0f;

        DispatcherTimer timer;

        string letraAnterior = "";
        string letraActual = "";

        bool aplicacion = true;         // Para usar los hilos siempre que la aplicación corre
        bool play = false;              // Para evitar que la aplicación crashee al pulsar detener
        bool videoPlaying = false;      // Para no limpiar el txt_Notas 2 veces si detienen el video antes de que termine

        public enum Display
        {
            Reconocer, Escuchando, Water, Forest, Epona, Spirit, Fire, Light, Zelda,
            Healing, Saria, Sun, Time, Storms
        };
        Display DisplayActual { get; set; }

        string epona = "w a d w a d ", saria = "s d a s d a ", zelda = "a w d a w d ", sun = "d s w d s w ",
            storms = "e s w e s w ", time = "d e s d e s ", healing = "a d s a d s ", spirit = "e s e d s e ",
            light = "w d w d a w ", forest = "e w a d a d ", fire = "s e s e d s d s ", water = "e s d d a ";

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;

            cronometro = new Stopwatch();
            LlenarComboDispositivos();

            // Hilo Videos
            ThreadStart VideoThreadStart = new ThreadStart(cicloHiloVideo);
            Thread VideoThread = new Thread(VideoThreadStart);
            VideoThread.Start();
        }

        // ================================= Thread =================================
        private void cicloHiloVideo()
        {
            while (aplicacion == true)
            {
                switch (DisplayActual)
                {
                    case Display.Epona:
                        Dispatcher.Invoke(epona_Recognized);
                        break;
                    case Display.Fire:
                        Dispatcher.Invoke(fire_Recognized);
                        break;
                    case Display.Forest:
                        Dispatcher.Invoke(forest_Recognized);
                        break;
                    case Display.Healing:
                        Dispatcher.Invoke(healing_Recognized);
                        break;
                    case Display.Light:
                        Dispatcher.Invoke(light_Recognized);
                        break;
                    case Display.Saria:
                        Dispatcher.Invoke(saria_Recognized);
                        break;
                    case Display.Spirit:
                        Dispatcher.Invoke(spirit_Recognized);
                        break;
                    case Display.Storms:
                        Dispatcher.Invoke(storms_Recognized);
                        break;
                    case Display.Sun:
                        Dispatcher.Invoke(sun_Recognized);
                        break;
                    case Display.Time:
                        Dispatcher.Invoke(time_Recognized);
                        break;
                    case Display.Water:
                        Dispatcher.Invoke(water_Recognized);
                        break;
                    case Display.Zelda:
                        Dispatcher.Invoke(zelda_Recognized);
                        break;
                    default:
                        break;
                }
            }
        }

        private void epona_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\epona.wmv", UriKind.Relative);
                video_Render();
                LtEpona.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void fire_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\fire.wmv", UriKind.Relative);
                video_Render();
                LtFuego.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void forest_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\forest.wmv", UriKind.Relative);
                video_Render();
                LtBosque.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void healing_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\healing.wmv", UriKind.Relative);
                video_Render();
                LtSanacion.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void light_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\light.wmv", UriKind.Relative);
                video_Render();
                LtLuz.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void saria_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\saria.wmv", UriKind.Relative);
                video_Render();
                LtSaria.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void spirit_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\spirit.wmv", UriKind.Relative);
                video_Render();
                LtEspiritu.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void storms_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\storms.wmv", UriKind.Relative);
                video_Render();
                LtTormentas.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void sun_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\sun.wmv", UriKind.Relative);
                video_Render();
                LtSol.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void time_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\time.wmv", UriKind.Relative);
                video_Render();
                LtTiempo.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void water_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\water.wmv", UriKind.Relative);
                video_Render();
                LtAgua.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        private void zelda_Recognized()
        {
            if (videoPlaying == false)
            {
                ocultar_Todo();
                // Video Source Change & Playback
                videoPlaying = true;
                Videos.Source = new Uri(@"Video\zelda.wmv", UriKind.Relative);
                video_Render();
                LtNana.Visibility = Visibility.Visible;
                btn_Omitir.Visibility = Visibility.Visible;
                DisplayActual = Display.Escuchando;
            }
        }

        // ======================= Reconocimiento de Canciones ======================
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (letraActual != "" && letraActual == letraAnterior)
            {
                // Evaluar si ya pasó un segundo
                if (cronometro.ElapsedMilliseconds >= 100 && videoPlaying == false)
                {
                    lbl_Nota.AppendText(letraActual + "");
                    letraActual = "";
                    cronometro.Restart();
                    if (lbl_Nota.Text.Length >= 12)
                    {
                        string cancion = lbl_Nota.Text.Substring(lbl_Nota.Text.Length - 12, 12);
                        if (cancion == epona)
                        {
                            DisplayActual = Display.Epona;
                        }
                        else if (cancion == zelda)
                        {
                            DisplayActual = Display.Zelda;
                        }
                        else if (cancion == saria)
                        {
                            DisplayActual = Display.Saria;
                        }
                        else if (cancion == sun)
                        {
                            DisplayActual = Display.Sun;
                        }
                        else if (cancion == storms)
                        {
                            DisplayActual = Display.Storms;
                        }
                        else if (cancion == time)
                        {
                            DisplayActual = Display.Time;
                        }
                        else if (cancion == light)
                        {
                            DisplayActual = Display.Light;
                        }
                        else if (cancion == spirit)
                        {
                            DisplayActual = Display.Spirit;
                        }
                        else if (cancion == healing)
                        {
                            DisplayActual = Display.Healing;
                        }
                        else if (cancion == forest)
                        {
                            DisplayActual = Display.Forest;
                        }
                    }
                    if (lbl_Nota.Text.Length >= 16)
                    {
                        string cancion = lbl_Nota.Text.Substring(lbl_Nota.Text.Length - 16, 16);
                        if (cancion == fire)
                        {
                            DisplayActual = Display.Fire;
                        }
                    }
                    if (lbl_Nota.Text.Length >= 10)
                    {
                        string cancion = lbl_Nota.Text.Substring(lbl_Nota.Text.Length - 10, 10);
                        if (cancion == water)
                        {
                            DisplayActual = Display.Water;
                        }
                    }
                }
            }
            else
            {
                cronometro.Restart();
            }
        }

        // =================  Audio Engineering & Frequency Analysis =================
        public void LlenarComboDispositivos()
        {
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                // La variable tipo WaveInCapabilities almacena los dispositivos en la iteración [i] hasta guardar todos los aparatos de entrada
                WaveInCapabilities capacidades = WaveIn.GetCapabilities(i);

                // Ahora agregamos a los "items" de nuestro combo box el Nombre del dispositivo almacenado en capacidades[i]
                cmb_Dispositivos.Items.Add(capacidades.ProductName);
            }
            cmb_Dispositivos.SelectedIndex = 0;
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
                    if (!videoPlaying)
                        BtnArriba.Visibility = Visibility.Visible;
                    else
                        BtnArriba.Visibility = Visibility.Hidden;
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
                    if (!videoPlaying)
                        BtnIzquierda.Visibility = Visibility.Visible;
                    else
                        BtnIzquierda.Visibility = Visibility.Hidden;
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
                    if (!videoPlaying)
                        BtnDerecha.Visibility = Visibility.Visible;
                    else
                        BtnDerecha.Visibility = Visibility.Hidden;
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
                    if (!videoPlaying)
                        BtnAbajo.Visibility = Visibility.Visible;
                    else
                        BtnAbajo.Visibility = Visibility.Hidden;
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
                    if (!videoPlaying)
                        BtnA.Visibility = Visibility.Visible;
                    else
                        BtnA.Visibility = Visibility.Hidden;
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

        // ==================== Botones Iniciar, Detener y Omitir ====================
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

        private void btn_Detener_Click(object sender, RoutedEventArgs e)
        {
            if (play == true)
            {
                waveIn.StopRecording();
                videoPlaying = false;
                Videos.IsMuted = true;
                ocultar_Todo();

                btn_Reconocer.Visibility = Visibility.Visible;
                btn_Ocarina.Visibility = Visibility.Visible;
            }

            lbl_Nota.Text = "";
            lbl_Frecuencia.Text = "0 Hz";
        }

        private void Btn_Omitir_Click(object sender, RoutedEventArgs e)
        {
            videoPlaying = false;
            Videos.Visibility = Visibility.Hidden;
            Videos.IsMuted = true;
            ocultar_Letreros();
            Ocarina.Visibility = Visibility.Visible;
            Notas.Visibility = Visibility.Visible;
            LtEscuchando.Visibility = Visibility.Visible;
            btn_Omitir.Visibility = Visibility.Hidden;
            lbl_Nota.Text = "";
            lbl_Frecuencia.Text = "0 Hz";
        }

        // ========================== Hide & Show Functions ==========================
        private void ocultar_Todo()
        {
            // Videos
            Videos.Visibility = Visibility.Hidden;
            Videos.IsMuted = true;

            // Letreros, Notas y Ocarina
            LtEscuchando.Visibility = Visibility.Hidden;
            btn_Reconocer.Visibility = Visibility.Hidden;
            Ocarina.Visibility = Visibility.Hidden;
            btn_Ocarina.Visibility = Visibility.Hidden;
            Notas.Visibility = Visibility.Hidden;
            btn_Omitir.Visibility = Visibility.Hidden;

            BtnA.Visibility = Visibility.Hidden;
            BtnArriba.Visibility = Visibility.Hidden;
            BtnAbajo.Visibility = Visibility.Hidden;
            BtnDerecha.Visibility = Visibility.Hidden;
            BtnIzquierda.Visibility = Visibility.Hidden;

            // Letreros Canciones
            if (LtEpona.Visibility == Visibility.Visible)
            {
                LtEpona.Visibility = Visibility.Hidden;
            }
            else if (LtFuego.Visibility == Visibility.Visible)
            {
                LtFuego.Visibility = Visibility.Hidden;
            }
            else if (LtAgua.Visibility == Visibility.Visible)
            {
                LtAgua.Visibility = Visibility.Hidden;
            }
            else if (LtSaria.Visibility == Visibility.Visible)
            {
                LtSaria.Visibility = Visibility.Hidden;
            }
            else if (LtSol.Visibility == Visibility.Visible)
            {
                LtSol.Visibility = Visibility.Hidden;
            }
            else if (LtTormentas.Visibility == Visibility.Visible)
            {
                LtTormentas.Visibility = Visibility.Hidden;
            }
            else if (LtTiempo.Visibility == Visibility.Visible)
            {
                LtTiempo.Visibility = Visibility.Hidden;
            }
            else if (LtSanacion.Visibility == Visibility.Visible)
            {
                LtSanacion.Visibility = Visibility.Hidden;
            }
            else if (LtNana.Visibility == Visibility.Visible)
            {
                LtNana.Visibility = Visibility.Hidden;
            }
            else if (LtLuz.Visibility == Visibility.Visible)
            {
                LtLuz.Visibility = Visibility.Hidden;
            }
            else if (LtEspiritu.Visibility == Visibility.Visible)
            {
                LtEspiritu.Visibility = Visibility.Hidden;
            }
            else if (LtBosque.Visibility == Visibility.Visible)
            {
                LtBosque.Visibility = Visibility.Hidden;
            }
        }

        private void ocultar_Letreros()
        {
            if (LtEpona.Visibility == Visibility.Visible)
            {
                LtEpona.Visibility = Visibility.Hidden;
            }
            else if (LtFuego.Visibility == Visibility.Visible)
            {
                LtFuego.Visibility = Visibility.Hidden;
            }
            else if (LtAgua.Visibility == Visibility.Visible)
            {
                LtAgua.Visibility = Visibility.Hidden;
            }
            else if (LtSaria.Visibility == Visibility.Visible)
            {
                LtSaria.Visibility = Visibility.Hidden;
            }
            else if (LtSol.Visibility == Visibility.Visible)
            {
                LtSol.Visibility = Visibility.Hidden;
            }
            else if (LtTormentas.Visibility == Visibility.Visible)
            {
                LtTormentas.Visibility = Visibility.Hidden;
            }
            else if (LtTiempo.Visibility == Visibility.Visible)
            {
                LtTiempo.Visibility = Visibility.Hidden;
            }
            else if (LtSanacion.Visibility == Visibility.Visible)
            {
                LtSanacion.Visibility = Visibility.Hidden;
            }
            else if (LtNana.Visibility == Visibility.Visible)
            {
                LtNana.Visibility = Visibility.Hidden;
            }
            else if (LtLuz.Visibility == Visibility.Visible)
            {
                LtLuz.Visibility = Visibility.Hidden;
            }
            else if (LtEspiritu.Visibility == Visibility.Visible)
            {
                LtEspiritu.Visibility = Visibility.Hidden;
            }
            else if (LtBosque.Visibility == Visibility.Visible)
            {
                LtBosque.Visibility = Visibility.Hidden;
            }
        }

        private void video_Render()
        {
            Videos.Play();
            Videos.IsMuted = false;
            Videos.Visibility = Visibility.Visible;
        }

        // ===============  Función ejecutada al finalizar los videos ===============
        private void Vid_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (videoPlaying == true)
            {
                videoPlaying = false;
                ocultar_Todo();
                Ocarina.Visibility = Visibility.Visible;
                Notas.Visibility = Visibility.Visible;
                LtEscuchando.Visibility = Visibility.Visible;
                lbl_Nota.Text = "";
            }
        }

        // ================ Function to stop threads when app closes ================
        private void Window_Closed(object sender, EventArgs e)
        {
            aplicacion = false;
        }
    }
}