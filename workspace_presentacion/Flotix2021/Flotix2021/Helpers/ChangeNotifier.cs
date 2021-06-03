using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flotix2021.Helpers
{
    public class ChangeNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
