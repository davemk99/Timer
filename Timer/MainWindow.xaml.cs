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
using System.Speech.Synthesis;
using MediaPlayer;
using WMPLib;
using System.Timers;


namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        System.Windows.Threading.DispatcherTimer timer;
        SpeechSynthesizer speech;
        int a;
        

       

        public MainWindow()
        {
            InitializeComponent();

            timer= new System.Windows.Threading.DispatcherTimer();
            speech = new SpeechSynthesizer();
           
            speech.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
            speech.Volume = 100;
            btnClear.IsEnabled = false;
            timer.Tick += new EventHandler(timer_Tick); 
            timer.Interval = new TimeSpan(0, 0, 1);
               timer.IsEnabled = false;
             a = 0;
            
        }
        private void timers()
        {
            timer.IsEnabled = true;
         timer.Start();
          
        }
        private void speecher()
        {
            int f = a+1;
            if (f < 5)
            {
                speech.Speak(f.ToString());
            }
        }
          

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtSeconds.Text != "")
            {
                a += int.Parse(txtSeconds.Text);
            }
            if (txtMinutes.Text != "")
            {
                a += int.Parse(txtMinutes.Text)*60;
            }
            if (txtHours.Text != "")
            {
                a += int.Parse(txtHours.Text) * 3600;
}
            if (a > 0)
            {

                timers();
            
              
              

                
                btnSet.IsEnabled = false;
                txtHours.Visibility = Visibility.Hidden;
                txtMinutes.Visibility = Visibility.Hidden;
                txtSeconds.Visibility = Visibility.Hidden;
                btnClear.IsEnabled = true;


            }
            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            
            
            if(a>0){


                int hours;
                int seconds;
                int minutes;
                hours = a / 3600;
                minutes = (a - (hours * 3600)) / 60;
                seconds = a - (hours * 3600) - (minutes * 60);
              

                lblTime.Content = hours.ToString()+":"+minutes.ToString()+":"+seconds.ToString() ;
                  
                
                    

                    a--;
                    speecher();
                  
                
                     
                
                

               
                

                
        }
               
            
            else
            {
                WindowsMediaPlayer player = new WindowsMediaPlayer();
                player.URL = "Message 1.mp3";
                player.controls.play();
                timer.Stop();
                timer.IsEnabled = false;
                lblTime.Content = "Time is ended";
                btnSet.IsEnabled = true;
                txtHours.Visibility = Visibility.Visible;
                txtMinutes.Visibility = Visibility.Visible;
                txtSeconds.Visibility = Visibility.Visible;


            }


            
        }

        private void txtSeconds_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtMinutes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timer.IsEnabled = false;
            btnSet.IsEnabled = true;
            txtHours.Visibility = Visibility.Visible;
            txtMinutes.Visibility = Visibility.Visible;
            txtSeconds.Visibility = Visibility.Visible;
            btnClear.IsEnabled = false;
          
           
           
            lblTime.Content = "";
            txtSeconds.Text = "";
            txtMinutes.Text = "";
            txtHours.Text = "";
            a = 0;

        }

      

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = WindowState.Minimized;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }
    }
}
