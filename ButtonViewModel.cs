using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeVersion
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush _buttonBackground = Brushes.Wheat;
        public SolidColorBrush ButtonBackground
        {
            get { return _buttonBackground; }
            set
            {
                _buttonBackground = value;
                OnPropertyChanged(nameof(ButtonBackground));
            }
        }

        private SolidColorBrush _buttonForeground = Brushes.Black;
        public SolidColorBrush ButtonForeground
        {
            get { return _buttonForeground; }
            set
            {
                _buttonForeground = value;
                OnPropertyChanged(nameof(ButtonForeground));
            }
        }

        private string _buttonContent="Buscar Cidade";
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}