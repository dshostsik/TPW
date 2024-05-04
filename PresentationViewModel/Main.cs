using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationModel;

namespace PresentationViewModel
{
    public class Main : changedProperty
    {
        private iModelAPI model;
        public ObservableCollection<iModelBall> iModelBalls => model.ModelBalls();
        public RelayCommand Start { get; }
        public RelayCommand Stop { get; }

        private String _amount;

        private int radius = 60;

        private String amount { get => _amount; set { _amount = value; RaisePropertyChanged(String.Empty); } }

        public Main() { 
            model = iModelAPI.Instance();
            Start = new RelayCommand(StartProcess);
            Stop = new RelayCommand(StopProcess);
        }

        public void StartProcess()
        {
            int integerAmount = int.Parse(amount);
            model.start(integerAmount, radius);
            RaisePropertyChanged("modelPropertyChanged");
        }

        public void StopProcess()
        {
            model.removeBalls();
            RaisePropertyChanged("modelPropertyChanged");
        }
    }
}
