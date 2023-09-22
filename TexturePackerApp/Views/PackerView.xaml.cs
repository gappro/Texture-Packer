using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TexturePackerApp.ViewModels;

namespace TexturePackerApp.Views
{
    /// <summary>
    /// Lógica de interacción para PackerView.xaml
    /// </summary>
    public partial class PackerView : UserControl
    {
        public PackerView()
        {
            InitializeComponent();

            // Your ViewModel set in DataContext.
            DataContext = new PackerViewModel();
        }
    }
}
