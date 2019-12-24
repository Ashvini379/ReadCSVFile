using Microsoft.Win32;
using ReadCSVFile.Commands;
using ReadCSVFile.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCSVFile.ViewModels
{
   public class MainWindowViewModel:ViewModelBase,IDisposable
    {
        private BackgroundWorker _worker;

        private DataTable dataTable;

        public DataTable DataTable
        {
            get { return dataTable; }
            set { dataTable = value; OnPropertyChanged("DataTable"); }
        }
        public DelegateCommand ShowDataCommand { get; set; }
        public MainWindowViewModel()
        {
            ShowDataCommand = new DelegateCommand(ShowData);
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += _worker_DoWork;
            _worker.ProgressChanged += _worker_ProgressChanged;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

        }
        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                 _progress = value;
            }
        }

         
        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(FileName != "")
            {
                DataTable = readcsv.readCSV(FileName);
            }
            
        }

        private object gridColumn;

        public object GridColumn
        {
            get { return gridColumn; }
            set { gridColumn = value;OnPropertyChanged("GridColumn"); }
        }

        private object selectedRow;

        public object SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
                GridColumn = new object();
                var ids = DataTable.AsEnumerable().Select(r => r.Field<object>(0)).ToList().Distinct();
                
                GridColumn = ids;
            }
        }
        private ImportCSV readcsv = new ImportCSV();
        private string FileName = "";
        private object selectedFeature;

        public object SelectedFeature
        {
            get { return selectedFeature; }
            set {
                selectedFeature = value;
                OnPropertyChanged("SelectedFeature");

            }
        }
        private  void ShowData()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
             
            if (fileDialog.ShowDialog() == true)
            {
                FileName =  fileDialog.FileName;
                _worker.RunWorkerAsync();
            }
        }

        public void Dispose()
        {
            _worker.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
