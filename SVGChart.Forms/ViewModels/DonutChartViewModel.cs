using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SVGChart.ViewModels
{
    public class DonutChartViewModel : BaseViewModel
    {
        public ObservableCollection<Tuple<int, string>> ChartSegments { get; set; }
        public ICommand UpdateValuesCommand { get; private set; }

        public DonutChartViewModel()
        {
            InitChartValue();
            UpdateValuesCommand = new Command(UpdateValuesCommandExecuted);
        }

        private string chartTitle;
        public string ChartTitle
        {
            get { return chartTitle; }
            set
            {
                chartTitle = value;
                OnPropertyChanged();
            }
        }

        public void InitChartValue()
        {

            ChartSegments = new ObservableCollection<Tuple<int, string>>()
            {
                new Tuple<int, string>(15, "#ff8787"),
                new Tuple<int, string>(10, "#42c0ff"),
                new Tuple<int, string>(25, "#b5f547"),
                new Tuple<int, string>(18, "#ffa14f"),
                new Tuple<int, string>(5, "#ff4281"),
            };

            ChartTitle = "80%";
        }

      
        private void UpdateValuesCommandExecuted(object obj)
        {
            ChartTitle = "123..";
            ChartSegments.RemoveAt(0);
        }

    }
}
