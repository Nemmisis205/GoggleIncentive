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
using GI.App.Validation;

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
            EditTimeslipOperatorSearch.ItemsSource = Operators;
            GoggleTextBox.ItemsSource = Goggles;
            EditGoggleTextBox.ItemsSource = Goggles;
            TimeslipListBox.ItemsSource = Timeslips;

        }

        //Refeshes ObservableCollections
        private void RefreshLists()
        {
            RefreshOperators();
            RefreshGoggles();
            RefreshTimeslips();
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
                dateHold = dateHold.ToOffset(new TimeSpan(-7, 0, 0));
                DateTimeOffset endTimeHold = DateTimeOffset.FromUnixTimeSeconds(i.EndTime);
                endTimeHold = endTimeHold.ToOffset(new TimeSpan(-7, 0, 0));
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

        //Adds Timeslip info to database if validation is passed.
        private void TimeslipSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var newTimeslip = new Timeslip();

            var timeslipValidation = new TimeslipValidation(UOW)
            {
                Operator = OperatorTextBox.Text,
                Goggle = GoggleTextBox.Text,
                StartCounter = BeginningCounterTextBox.Text,
                EndCounter = EndingCounterTextBox.Text,
                StartBox = BeginningBoxCount.Text,
                StartPiece = BeginningPieceCount.Text,
                EndBox = EndingBoxCount.Text,
                EndPiece = EndingPieceCount.Text
            };
            if (timeslipValidation.Error == null)
            {
                Operator chosenOperator = OperatorTextBox.SelectedItem as Operator;
                Goggle chosenGoggle = GoggleTextBox.SelectedItem as Goggle;
                DateTimeOffset startTimeGrabber = (DateTimeOffset)StartDTPicker.Value;
                DateTimeOffset endTimeGrabber = (DateTimeOffset)EndDTPicker.Value;
                newTimeslip.OperatorId = chosenOperator.Id;
                newTimeslip.GoggleId = chosenGoggle.Id;
                newTimeslip.StartTime = startTimeGrabber.ToUnixTimeSeconds() - 25200;
                newTimeslip.EndTime = endTimeGrabber.ToUnixTimeSeconds() - 25200;
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
                double scrapHold = newTimeslip.Scrap;
                newTimeslip.ScrapPercent = Math.Round((scrapHold / newTimeslip.CyclesPerHour) * 100, 2);
                newTimeslip.Efficiency = Math.Round((newTimeslip.CyclesPerHour / chosenGoggle.QuotedCycle) * 100, 2);
                if (newTimeslip.Efficiency >= 80)
                {
                    newTimeslip.IncentiveAchieved = 1;
                }
                else
                {
                    newTimeslip.IncentiveAchieved = 0;
                }

                if (UOW.Timeslips.Add(newTimeslip) == 1)
                {
                    AddTimeslips_ClearAllFields();
                    RefreshTimeslips();
                    OperatorTextBox.Focus();
                    System.Windows.MessageBox.Show("Timeslip Added", "Success", MessageBoxButton.OK);
                }
                else
                {
                    System.Windows.MessageBox.Show("Something went wrong. Timeslip not added.", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                System.Windows.MessageBox.Show(timeslipValidation.Error);
            }


        }

        //Add Goggle information to database if validation is passed.
        private void AddGoggleSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var goggleValidation = new GoggleValidation(UOW)
            {
                Quote = AddGoggleQuoteTextBox.Text,
                PerBox = AddGogglePieceTextBox.Text,
                BoxesPerPallet = AddGogglePalletTextBox.Text
            };

            if (goggleValidation.Error == null)
            {
                var newGoggle = new Goggle(AddGoggleNameTextBox.Text, int.Parse(AddGoggleQuoteTextBox.Text), int.Parse(AddGogglePieceTextBox.Text), int.Parse(AddGogglePalletTextBox.Text));
                UOW.Goggles.Add(newGoggle);
                RefreshGoggles();
                GoggleTextBox.ItemsSource = Goggles;
                AddGoggleNameTextBox.Clear();
                AddGoggleQuoteTextBox.Clear();
                AddGogglePieceTextBox.Clear();
                System.Windows.MessageBox.Show("Goggle successfully added!");
            }
            else
            {
                System.Windows.MessageBox.Show(goggleValidation.Error);
            }
        }

        //Add Operator to database.
        private void AddOperatorSubmitButton_Click(Object sender, RoutedEventArgs e)
        {
            var newOperator = new Operator(AddOperatorTextBox.Text);

            UOW.Operators.Add(newOperator);
            RefreshOperators();
            OperatorTextBox.ItemsSource = Operators;
            AddOperatorTextBox.Clear();
            System.Windows.MessageBox.Show("Operator successfully added!");
        }

        //Clicking Timeslip shows Edit Timeslip window.
        private void TimeslipView_Click(object sender, RoutedEventArgs e)
        {
            EditTimeslipDialogWindow dlg = new EditTimeslipDialogWindow();
            Button clickedButton = sender as Button;
            Timeslip timeslipButton = clickedButton.DataContext as Timeslip;
            dlg.DataContext = timeslipButton;
            dlg.Show();
        }

        //Allows you to quickly delete a textbox when you select via click or tab.
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            WatermarkTextBox txtBox = sender as WatermarkTextBox;
            txtBox.SelectAll();
        }

        //When Edit window closed, refresh Timeslip list.
        public void OnEditWindowClosed(object sender, EventArgs e)
        {
            RefreshTimeslips();
        }
        
        //When a timeslip is added, clear fields to prepare for next timeslip.
        private void AddTimeslips_ClearAllFields()
        {
            OperatorTextBox.Text = "";
            GoggleTextBox.Text = "";
            StartDTPicker.Text = "";
            EndDTPicker.Text = "";
            BeginningCounterTextBox.Clear();
            EndingCounterTextBox.Clear();
            BeginningBoxCount.Clear();
            BeginningPieceCount.Clear();
            EndingPieceCount.Clear();
            EndingBoxCount.Clear();
        }

        //Edits goggle information, if validation is passed.
        private void EditGoggleSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var goggleValidation = new GoggleValidation(UOW)
            {
                Quote = EditGoggleQuoteTextBox.Text,
                PerBox = EditGogglePieceTextBox.Text,
                BoxesPerPallet = EditGogglePalletTextBox.Text
            };

            if (goggleValidation.Error == null)
            {
                Goggle selectedGoggle = EditGoggleTextBox.SelectedItem as Goggle;
                Goggle newGoggle = new Goggle();

                newGoggle.Id = selectedGoggle.Id;
                newGoggle.Name = selectedGoggle.Name;
                newGoggle.QuotedCycle = int.Parse(EditGoggleQuoteTextBox.Text);
                newGoggle.PerBox = int.Parse(EditGogglePieceTextBox.Text);
                newGoggle.BoxesPerPallet = int.Parse(EditGogglePalletTextBox.Text);

                if (UOW.Goggles.Update(newGoggle) == 1)
                {
                    RefreshGoggles();
                    System.Windows.MessageBox.Show("Goggle Saved!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    System.Windows.MessageBox.Show("Something went wrong. Goggle not changed.", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                System.Windows.MessageBox.Show(goggleValidation.Error);
            }
        }

        //Populates goggle information when selected in drop down.
        private void EditGoggleTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Goggle? heldGoggle = EditGoggleTextBox.SelectedItem as Goggle;

            EditGoggleQuoteTextBox.Text = heldGoggle?.QuotedCycle.ToString();
            EditGogglePieceTextBox.Text = heldGoggle?.PerBox.ToString();
            EditGogglePalletTextBox.Text = heldGoggle?.BoxesPerPallet.ToString();
        }

        //Search function in Edit Timeslip tab.
        private void EditTimeslipOperatorSearchButton_Click(object sender, RoutedEventArgs e)
        {
            Operator heldOperator = EditTimeslipOperatorSearch.SelectedItem as Operator;
            Timeslips.Clear();

            foreach (Timeslip i in new ObservableCollection<Timeslip>(UOW.Timeslips.GetByOperator(heldOperator.Id)))
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
                else if (i.IncentiveAchieved == 1)
                {
                    i.IncentiveString = "Yes";
                }
            }
        }
    }
}
