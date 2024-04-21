using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string pop = "") {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pop));
        }

        private int _Clicks;

        public int Clicks {  
            get {
                return _Clicks; 
            } 
            set {  
                _Clicks = value;
                OnPropertyChanged();
            } 
        }
    }
}
