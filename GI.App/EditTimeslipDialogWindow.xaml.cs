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
using System.Windows.Shapes;
using GI.App.Validation;

namespace GI.App
{
    /// <summary>
    /// Interaction logic for EditTimeslipDialogWindow.xaml
    /// </summary>
    public partial class EditTimeslipDialogWindow : Window
    {
        public UnitOfWork UOW { get; set; }
        public ConnectionFactory factory { get; set; }
        public ObservableCollection<Timeslip> Timeslips { get; set; }
        public ObservableCollection<Operator> Operators { get; set; }
        public ObservableCollection<Goggle> Goggles { get; set; }
        public Timeslip oldSlip { get; set; }
        public EditTimeslipDialogWindow()
        {
            //Creates Unit of Work
            factory = new ConnectionFactory();
            UOW = new UnitOfWork(factory);
            ObservableCollection<string> overrideSettings = new ObservableCollection<string>();
            oldSlip = this.DataContext as Timeslip;
            overrideSettings.Add("");
            overrideSettings.Add("Earned");
            overrideSettings.Add("Failed");

            InitializeComponent();

            //Creates Collections, then pulls information from database
            Timeslips = new ObservableCollection<Timeslip>();
            Operators = new ObservableCollection<Operator>();
            Goggles = new ObservableCollection<Goggle>();

            EditTimeslipOperatorComboBox.ItemsSource = Operators;
            EditTimeslipGoggleComboBox.ItemsSource = Goggles;
            EditTimeslipOverrideComboBox.ItemsSource = overrideSettings;
            InitializeComponent();

            RefreshLists();

            //Closed Event Handling
            Closed += Window_Closed;
            ClosedEvent += ((MainWindow)Application.Current.MainWindow).OnEditWindowClosed;
        }

        private void RefreshLists()
        {
            RefreshOperators();
            RefreshGoggles();
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            oldSlip = this.DataContext as Timeslip;
            if (EditButton.Content == "Delete")
            {
                UOW.Timeslips.Delete(oldSlip);
                this.Close();
            }

            EditTimeslipOperatorComboBox.IsEnabled = true;
            EditTimeslipGoggleComboBox.IsEnabled = true;
            EditTimeslipDateTextBox.IsEnabled = true;
            EditTimeslipStartTimeTextBox.IsEnabled = true;
            EditTimeslipEndTimeTextBox.IsEnabled = true;
            EditTimeslipStartCounterTextBox.IsEnabled = true;
            EditTimeslipEndCounterTextBox.IsEnabled = true;
            EditTimeslipStartBoxCountTextBox.IsEnabled = true;
            EditTimeslipStartPieceCountTextBox.IsEnabled = true;
            EditTimeslipEndBoxCountTextBox.IsEnabled = true;
            EditTimeslipEndPieceCountTextBox.IsEnabled = true;
            EditTimeslipOverrideComboBox.IsEnabled = true;

            SaveButton.IsEnabled = true;
            EditButton.Content = "Delete";

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            oldSlip = this.DataContext as Timeslip;
            var newTimeslip = new Timeslip();
            var timeslipValidation = new TimeslipValidation(UOW)
            {
                Operator = EditTimeslipOperatorComboBox.Text,
                Goggle = EditTimeslipGoggleComboBox.Text,
                StartCounter = EditTimeslipStartCounterTextBox.Text,
                EndCounter = EditTimeslipEndCounterTextBox.Text,
                StartBox = EditTimeslipStartBoxCountTextBox.Text,
                StartPiece = EditTimeslipStartPieceCountTextBox.Text,
                EndBox = EditTimeslipEndBoxCountTextBox.Text,
                EndPiece = EditTimeslipEndPieceCountTextBox.Text
            };
            if (timeslipValidation.Error == null)
            {


                Operator chosenOperator = EditTimeslipOperatorComboBox.SelectedItem as Operator;
                Goggle chosenGoggle = EditTimeslipGoggleComboBox.SelectedItem as Goggle;

                //Solves dates and times
                if (EditTimeslipStartTimeTextBox.Text.Substring(6) == "PM" && EditTimeslipEndTimeTextBox.Text.Substring(6) == "AM")
                {
                    string startTime = EditTimeslipDateTextBox.Text + " " + EditTimeslipStartTimeTextBox.Text;
                    DateTimeOffset startDT = DateTimeOffset.ParseExact(startTime + " -07:00" , "MM/dd/yy hh:mm tt zzz", null, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    newTimeslip.StartTime = startDT.ToUnixTimeSeconds();

                    string endTime = EditTimeslipDateTextBox.Text + " " + EditTimeslipEndTimeTextBox.Text;
                    DateTimeOffset endDT = DateTimeOffset.ParseExact(endTime + " -07:00", "MM/dd/yy hh:mm tt zzz", null, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    newTimeslip.EndTime = endDT.ToUnixTimeSeconds() + 86400;
                }
                else
                {
                    string startTime = EditTimeslipDateTextBox.Text + " " + EditTimeslipStartTimeTextBox.Text;
                    DateTimeOffset startDT = DateTimeOffset.ParseExact(startTime + " -07:00", "MM/dd/yy hh:mm tt zzz", null, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    newTimeslip.StartTime = startDT.ToUnixTimeSeconds();

                    string endTime = EditTimeslipDateTextBox.Text + " " + EditTimeslipEndTimeTextBox.Text;
                    DateTimeOffset endDT = DateTimeOffset.ParseExact(endTime + " -07:00", "MM/dd/yy hh:mm tt zzz", null, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    newTimeslip.EndTime = endDT.ToUnixTimeSeconds();
                }

                //Solves Good Goggles
                newTimeslip.StartBoxCount = int.Parse(EditTimeslipStartBoxCountTextBox.Text);
                newTimeslip.StartPieceCount = int.Parse(EditTimeslipStartPieceCountTextBox.Text);
                newTimeslip.EndBoxCount = int.Parse(EditTimeslipEndBoxCountTextBox.Text);
                newTimeslip.EndPieceCount = int.Parse(EditTimeslipEndPieceCountTextBox.Text);
                newTimeslip.GoggleId = chosenGoggle.Id;
                if (newTimeslip.EndBoxCount < newTimeslip.StartBoxCount)
                {
                    int boxPerPalletGrabber = UOW.Goggles.GetById(newTimeslip.GoggleId).BoxesPerPallet;
                    newTimeslip.GoodParts = (((newTimeslip.EndBoxCount + boxPerPalletGrabber) * chosenGoggle.PerBox) + newTimeslip.EndPieceCount) - ((newTimeslip.StartBoxCount * chosenGoggle.PerBox) + newTimeslip.StartPieceCount);
                }
                else
                {
                    newTimeslip.GoodParts = ((newTimeslip.EndBoxCount * chosenGoggle.PerBox) + newTimeslip.EndPieceCount) - ((newTimeslip.StartBoxCount * chosenGoggle.PerBox) + newTimeslip.StartPieceCount);
                }
                //Solves most other fields
                newTimeslip.Id = oldSlip.Id;
                newTimeslip.OperatorId = chosenOperator.Id;
                newTimeslip.StartCounter = int.Parse(EditTimeslipStartCounterTextBox.Text);
                newTimeslip.EndCounter = int.Parse(EditTimeslipEndCounterTextBox.Text);
                newTimeslip.HoursRan = newTimeslip.EndTime - newTimeslip.StartTime;
                newTimeslip.CycleCount = newTimeslip.EndCounter - newTimeslip.StartCounter;
                newTimeslip.CyclesPerHour = Math.Round((double)((newTimeslip.CycleCount) / (newTimeslip.HoursRan / 3600)), 2);
                newTimeslip.Scrap = newTimeslip.CycleCount - newTimeslip.GoodParts;
                newTimeslip.ScrapPercent = Math.Round((double)(newTimeslip.Scrap / newTimeslip.CycleCount) * 100, 2);
                newTimeslip.Efficiency = Math.Round((newTimeslip.CyclesPerHour / chosenGoggle.QuotedCycle) * 100, 2);


                //Incentive Solver
                if (EditTimeslipOverrideComboBox.Text == "Failed")
                {
                    newTimeslip.IncentiveAchieved = 0;
                    newTimeslip.Override = 0;
                }
                else if (EditTimeslipOverrideComboBox.Text == "Earned")
                {
                    newTimeslip.IncentiveAchieved = 1;
                    newTimeslip.Override = 1;
                }
                else if (newTimeslip.Efficiency >= 80)
                {
                    newTimeslip.IncentiveAchieved = 1;
                }
                else if (newTimeslip.Efficiency < 80)
                {
                    newTimeslip.IncentiveAchieved = 0;
                }

                //Update
                UOW.Timeslips.Update(newTimeslip);
                this.Close();
            }
            else
            {
                MessageBox.Show(timeslipValidation.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClosedEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ClosedEvent;
    }
}
