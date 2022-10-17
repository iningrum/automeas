using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace automeas_ui.Core
{
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new(name));
        }
    }
}
