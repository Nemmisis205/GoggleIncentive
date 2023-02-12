using GI.Model.Models;
using GI.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.Toolkit;

namespace GI.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UnitOfWork UOW { get; set; }
        public ConnectionFactory factory { get; set; }
        public ObservableCollection<Timeslip> Timeslips { get; set; }
        public ObservableCollection<Operator> Operators { get; set; }
        public ObservableCollection<Goggle> Goggles { get; set; }

        public MainWindow()
        {
            //Creates Unit of Work
            factory = new ConnectionFactory();
            UOW = new UnitOfWork(factory);

            InitializeComponent();

            //Creates Collections, then pulls information from database
            Timeslips = new ObservableCollection<Timeslip>();
            Operators = new ObservableCollection<Operator>();
            Goggles = new ObservableCollection<Goggle>();
            RefreshLists();

        }

        private void RefreshLists()
        {
            RefreshTimeslips();
            RefreshOperators();
            RefreshGoggles();
        }

        private void RefreshTimeslips()
        {
            Timeslips.Clear();
            foreach (Timeslip i in new ObservableCollection<Timeslip>(UOW.Timeslips.GetAll()))
            {
                Timeslips.Add(i);
            }
        }

        private void RefreshOperators()
        {
            Operators.Clear();
            foreach (Operator i in new ObservableCollection<Operator>(UOW.Operators.GetAll()))
            {
                Operators.Add(i);
            }
        }

        private void RefreshGoggles()
        {
            Goggles.Clear();
            foreach (Goggle i in new ObservableCollection<Goggle>(UOW.Goggles.GetAll()))
            {
                Goggles.Add(i);
            }
        }

        private void TimeslipSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddGoggleSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var newGoggle = new Goggle(AddGoggleNameTextBox.Text, int.Parse(AddGoggleQuoteTextBox.Text), int.Parse(AddGogglePieceTextBox.Text));

            UOW.Goggles.Add(newGoggle);
        }

        private void AddOperatorSubmitButton_Click(Object sender, RoutedEventArgs e)
        {
            var newOperator = new Operator(AddOperatorTextBox.Text);

            UOW.Operators.Add(newOperator);
        }
    }
}
