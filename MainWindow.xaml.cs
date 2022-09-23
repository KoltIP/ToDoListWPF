using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ToDoListWPF.Models;
using ToDoListWPF.Services;

namespace ToDoListWPF
{ 
    public partial class MainWindow : Window
    {
        private readonly string Path = $"{Environment.CurrentDirectory}\\toDoDataList.json";
        private BindingList<ToDoModel> _toDoDataList;
        private FileIOService _fileIOService;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            _fileIOService = new FileIOService(Path);
            try
            {
                _toDoDataList = _fileIOService.LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }


            dgToDoList.ItemsSource = _toDoDataList;
            _toDoDataList.ListChanged += _todoDataList_ListChanged;
        }

        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }

            }
        }

    }
}
