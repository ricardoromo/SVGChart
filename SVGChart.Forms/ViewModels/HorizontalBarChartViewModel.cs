using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SVGChart.ViewModels
{
    public class HorizontalBarChartViewModel : BaseViewModel
    {
        public ObservableCollection<Tuple<int, string>> ChartSegments { get; set; }
        public ICommand UpdateValuesCommand { get; private set; }

        public HorizontalBarChartViewModel()
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
                new Tuple<int, string>(67, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(45, "#ffa14f"),
                new Tuple<int, string>(10, "#40bf40"),
                new Tuple<int, string>(55, "#ff4d4d"),
                new Tuple<int, string>(66, "#b5f547"),
                new Tuple<int, string>(100, "#ffa14f"),
                new Tuple<int, string>(67, "#ff8787"),
            };
        }


        private void UpdateValuesCommandExecuted(object obj)
        {
            IsValuesVisible = !IsValuesVisible;
            var random = new Random();
            var randomNumber = random.Next(0, 100);
            ChartSegments.Add(new Tuple<int, string>(randomNumber, "#ff8787"));
        }
    }
}
