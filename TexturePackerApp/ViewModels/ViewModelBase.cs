using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TexturePackerApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        SynchronizationContext sctxUI = new SynchronizationContext();


        protected virtual void OnPropertyChanged(string propertyName) {
            sctxUI.Post(o => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)), null);
            
        }

        public void __init__()
        {
            sctxUI = SynchronizationContext.Current;
        }
    }
}
