using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PresentationModel;

namespace ViewModelAPI
{
    public class ViewModelAPI : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int width { get; } = 500;
        public int height { get; } = 500;

        private bool _enabled { get; set; } = true;
        private bool _disabled { get; set; } = false;
        private string _inputNumber = "3";
        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }
        public ObservableCollection<ModelBall> Balls { get; }
        private iModelAPI model;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
           this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String _amount;

        private int radius = 60;

        public String amount { 
                get => _amount;
                set { 
                    _amount = value;
                    RaisePropertyChanged(String.Empty);
                } 
        }

        public ViewModelAPI() { 
            model = iModelAPI.Instance(width, height);
            Start = new RelayCommand(() => StartProcess());
            Stop = new RelayCommand(() => StopProcess());
        }

        public void StartProcess()
        {
            int integerAmount = int.Parse(amount);
            if (integerAmount > 0)
            {
                this._enabled = true;
                this._disabled = false;
                model.AddBalls(integerAmount);
            }
        }

        public void StopProcess()
        {
            model.stop();
            Balls.Clear();
            this._enabled = false;
            this._disabled = true;
        }
    }
}
