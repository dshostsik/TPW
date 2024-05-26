using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationModel
{
    public interface iModelBall : INotifyPropertyChanged
    { 
        float Left { get; }
        float Top { get; }
        int radius { get; }
    }

    public class ModelBall : iModelBall, INotifyPropertyChanged
            {
                public event PropertyChangedEventHandler? PropertyChanged;
                public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }

                private float _left;
                private float _top;
                private int _radius;
                
                public ModelBall(float X, float Y, int R)
                {
                    Top = Y - R;
                    Left = X - R;
                }

                public float Left
                {
                    get { return _left;}
                    private set
                    {
                        _left = value;
                        RaisePropertyChanged();
                    }
                }

                public float Top
                {
                    get { return _top;}
                    private set
                    {
                        _top = value;
                        RaisePropertyChanged();
                    }
                }

                public int radius
                {
                    get
                    {
                        return _radius;
                    }
                }


                public void Move(float X, float Y)
                {
                    this.Left = X;
                    this.Top = Y;
                }
            }
    

    
}
