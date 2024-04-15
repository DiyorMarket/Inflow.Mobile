using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string _counter;
        public string Counter
        {
            get => _counter;
            set => SetProperty(ref _counter, value);
        }

        public ICommand CounterUpdateCommand { get; }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            CounterUpdateCommand = new Command(OnUpdateCounter);
        }

        private void OnUpdateCounter()
        {
            Counter += 1;
        }

        public ICommand OpenWebCommand { get; }
    }
}