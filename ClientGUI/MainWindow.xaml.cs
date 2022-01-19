using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string _addressBar = "";
        public string AddressBar
        {
            get => _addressBar;
            set
            {
                _addressBar = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Explore(@"\", @"\");
        }

        private async Task Explore(string path, string dirName)
        {
            var directory = await ClientGUILibrary.GRPCClient.ExplorePathAsync(path, dirName);
            AddressBar = directory.Path;
            Items.Clear();
            foreach (var file in directory.DirectoryNames)
            {
                Items.Add(new Item() { Name = file });
            }
            foreach (var file in directory.FileNames)
            {
                Items.Add(new Item() { Name = file, IsDirectory = false });
            }
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedIndex >= 0)
            {
                await Explore(AddressBar, Items[listView.SelectedIndex].Name);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Explore(@"\", @"\");
        }
    }
}
