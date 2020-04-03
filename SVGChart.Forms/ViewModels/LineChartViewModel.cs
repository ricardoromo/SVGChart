using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SVGChart.ViewModels
{
    public class LineChartViewModel : BaseViewModel
    {
        public ObservableCollection<int> ChartSegments { get; set; }
        public ICommand UpdateValuesCommand { get; private set; }

        public LineChartViewModel()
        {
            InitChartValue();
            UpdateValuesCommand = new Command(UpdateValuesCommandExecuted);
        }

        private bool displayPoints;
        public bool DisplayPoints
        {
            get { return displayPoints; }
            set
            {
                displayPoints = value;
                OnPropertyChanged();
            }
        }

        public void InitChartValue()
        {
            ChartSegments = new ObservableCollection<int>()
            {
               66,34,88,20,90,55,65,80,45
            };
        }

        private void UpdateValuesCommandExecuted(object obj)
        {
            DisplayPoints = !DisplayPoints;
            var random = new Random();
            var randomNumber = random.Next(0, 100);
            ChartSegments.Add(randomNumber);
        }
    }
}
