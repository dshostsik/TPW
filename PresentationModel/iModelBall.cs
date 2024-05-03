using System.ComponentModel;

namespace PresentationModel
{
    public abstract class iModelBall : INotifyPropertyChanged
    {
        public static iModelBall createModelBallInstance() {
            return new ModelBall();
        }

        public abstract event PropertyChangedEventHandler? PropertyChanged;

        public abstract int posix{ get; set; }
        public abstract int posiy { get; set; }
        public abstract int radius { get; set; }

        public abstract void updateModelBall(Object o, PropertyChangedEventArgs args);

    }
}
