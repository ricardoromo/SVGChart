using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SVGChart.ViewModels
{
    public class VerticalBarChartViewModel : BaseViewModel
    {

        public ObservableCollection<Tuple<int, string>> ChartSegments { get; set; }
        public ICommand UpdateValuesCommand { get; private set; }

        public VerticalBarChartViewModel()
        {
            InitChartValue();
            UpdateValuesCommand = new Command(UpdateValuesCommandExecuted);
        }

        private bool isValuesVisible;
        public bool IsValuesVisible
        {
            get { return isValuesVisible; }
            set
            {
                isValuesVisible = value;
                OnPropertyChanged();
            }
        }

        public void InitChartValue()
        {
            ChartSegments = new ObservableCollection<Tuple<int, string>>()
            {
                new Tuple<int, string>(86, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(86, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(86, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547")
            };
        }

        private void UpdateValuesCommandExecuted(object obj)
        {
            IsValuesVisible = !IsValuesVisible;
        }
    }
}
