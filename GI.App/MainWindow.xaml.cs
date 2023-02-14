using GI.Model.Models;
using GI.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<TimeslipView> TimeslipsView { get; set; }

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
            TimeslipsView = new ObservableCollection<TimeslipView>();
            RefreshLists();

            OperatorTextBox.ItemsSource = Operators;
            GoggleTextBox.ItemsSource = Goggles;
            TimeslipListBox.ItemsSource = Timeslips;

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
                i.OperatorName = UOW.Operators.GetById(i.OperatorId).Name;
                i.GoggleName = UOW.Goggles.GetById(i.GoggleId).Name;
                DateTimeOffset dateHold = DateTimeOffset.FromUnixTimeSeconds(i.StartTime);
                DateTimeOffset endTimeHold = DateTimeOffset.FromUnixTimeSeconds(i.EndTime);
                i.DateString = dateHold.ToString("MM/dd/yy");
                i.StartTimeString = dateHold.ToString("hh:mm tt");
                i.EndTimeString = endTimeHold.ToString("hh:mm tt");
                i.HoursRanString = Math.Round((double)i.HoursRan / 3600, 2).ToString();
                if (i.IncentiveAchieved == 0)
                {
                    i.IncentiveString = "No";
                }
                else if(i.IncentiveAchieved == 1)
                {
                    i.IncentiveString = "Yes";
                }
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
            var newTimeslip = new Timeslip();
            Operator chosenOperator = OperatorTextBox.SelectedItem as Operator;
            Goggle chosenGoggle = GoggleTextBox.SelectedItem as Goggle;
            DateTimeOffset startTimeGrabber = (DateTimeOffset)StartDTPicker.Value;
            DateTimeOffset endTimeGrabber = (DateTimeOffset)EndDTPicker.Value;

            newTimeslip.OperatorId = chosenOperator.Id;
            newTimeslip.GoggleId = chosenGoggle.Id;
            newTimeslip.StartTime = startTimeGrabber.ToUnixTimeSeconds();
            newTimeslip.EndTime = endTimeGrabber.ToUnixTimeSeconds();
            newTimeslip.StartCounter = int.Parse(BeginningCounterTextBox.Text);
            newTimeslip.EndCounter = int.Parse(EndingCounterTextBox.Text);
            newTimeslip.StartBoxCount = int.Parse(BeginningBoxCount.Text);
            newTimeslip.StartPieceCount = int.Parse(BeginningPieceCount.Text);
            newTimeslip.EndBoxCount = int.Parse(EndingBoxCount.Text);
            newTimeslip.EndPieceCount = int.Parse(EndingPieceCount.Text);
            newTimeslip.HoursRan = newTimeslip.EndTime - newTimeslip.StartTime;
            newTimeslip.CycleCount = newTimeslip.EndCounter - newTimeslip.StartCounter;
            newTimeslip.CyclesPerHour = Math.Round((double)((newTimeslip.CycleCount) / (newTimeslip.HoursRan / 3600)), 2);
            newTimeslip.GoodParts = ((newTimeslip.EndBoxCount * chosenGoggle.PerBox) + newTimeslip.EndPieceCount) - ((newTimeslip.StartBoxCount * chosenGoggle.PerBox) + newTimeslip.StartPieceCount);
            newTimeslip.Scrap = newTimeslip.CycleCount - newTimeslip.GoodParts;
            newTimeslip.ScrapPercent = Math.Round((double)(newTimeslip.Scrap / newTimeslip.CycleCount) * 100, 2);
            newTimeslip.Efficiency = Math.Round((newTimeslip.CyclesPerHour / chosenGoggle.QuotedCycle) * 100, 2);
            if (newTimeslip.Efficiency >= 80)
            {
                newTimeslip.IncentiveAchieved = 1;
            }
            else
            {
                newTimeslip.IncentiveAchieved = 0;
            }

            UOW.Timeslips.Add(newTimeslip);
            RefreshTimeslips();

        }

        private void AddGoggleSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var newGoggle = new Goggle(AddGoggleNameTextBox.Text, int.Parse(AddGoggleQuoteTextBox.Text), int.Parse(AddGogglePieceTextBox.Text));

            UOW.Goggles.Add(newGoggle);
            RefreshGoggles();
            GoggleTextBox.ItemsSource = Goggles;
            AddGoggleNameTextBox.Clear();
            AddGoggleQuoteTextBox.Clear();
            AddGogglePieceTextBox.Clear();
        }

        private void AddOperatorSubmitButton_Click(Object sender, RoutedEventArgs e)
        {
            var newOperator = new Operator(AddOperatorTextBox.Text);

            UOW.Operators.Add(newOperator);
            RefreshOperators();
            OperatorTextBox.ItemsSource = Operators;
            AddOperatorTextBox.Clear();

        }

        //private void AddTimeslipConfirmation_Click(object sender, RoutedEventArgs e)
        //{
        //    bool valdated = false;

        //}

        private void TimeslipView_Click(object sender, RoutedEventArgs e)
        {
            EditTimeslipDialogWindow dlg = new EditTimeslipDialogWindow();
            Button clickedButton = sender as Button;
            Timeslip timeslipButton = clickedButton.DataContext as Timeslip;
            dlg.DataContext = timeslipButton;
            dlg.Show();
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            WatermarkTextBox txtBox = sender as WatermarkTextBox;
            txtBox.SelectAll();
        }
    }
}
