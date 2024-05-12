using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationModel;

namespace ViewModelAPI
{
    public class ViewModelAPI : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private iModelAPI model;
        public ObservableCollection<iModelBall> iModelBalls => model.ModelBalls();
        public RelayCommand Start { 
                get; 
            }
        public RelayCommand Stop { 
                get;
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
            model = iModelAPI.Instance();
            Start = new RelayCommand(StartProcess);
            Stop = new RelayCommand(StopProcess);
        }

        public void StartProcess()
        {
            int integerAmount = int.Parse(amount);
            model.start(integerAmount, radius);
            RaisePropertyChanged("ProcessStarted");
        }

        public void StopProcess()
        {
            model.removeBalls();
            RaisePropertyChanged("ProcessStopped");
        }
    }
}
