using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using DataImporter.Repository;
using System.Diagnostics;
using DataImporter.Config;
using log4net;
using log4net.Config;


namespace DataImporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string PathName { get; set; }
        public string Extension { get; set; }
        public DateTime DateFrom { get { return DateTime.Today.AddDays(-500); } }

        BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
        //bool isTrue = true;
        

        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.PathName = txtPath.Text;
            this.Extension = txtExtension.Text;
            this.bw.DoWork += Bw_DoWork;
            this.bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();
        }
                

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send,
              new Action(() => stopThreads()));
        }

        private void stopThreads()
        {
            Dispatcher.InvokeShutdown();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //LoopInfinite();
            ReadFiles();
        }

        private void ReadFiles()
        {
            if (Directory.Exists(PathName))
            {
                new ReadFilesRepo(Extension, PathName, DateFrom);
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        int i = 1;
        private void LoopInfinite()
        {
            while (true)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, GenerateNewItem());
            }
        }

        private Action GenerateNewItem()
        {
            return new Action(() => this.lboxInfo.Items.Insert(0, new ListBoxItem { Content = "New item" + (++i) }));
        }




    }
}
