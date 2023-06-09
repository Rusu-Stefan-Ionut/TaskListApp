using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Tema_2_Notite.Model;
using Tema_2_Notite.Resources;
using Tema_2_Notite.ViewModel;

namespace Tema_2_Notite.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        
        public NotesWindow()
        {
            InitializeComponent();
            Closing += OnWindowClosing;
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            MainVM data = this.DataContext as MainVM;
            SerializationActions actions = new SerializationActions(data.BaseObject.DataBases);
            actions.SerializeObject(data.BaseObject);
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
