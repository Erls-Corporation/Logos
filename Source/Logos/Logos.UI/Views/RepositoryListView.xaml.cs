using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logos.Infrastructure.Common;
using Logos.UI.ViewModels;

namespace Logos.UI.Views
{
    /// <summary>
    /// Interaction logic for RepositoryListView.xaml
    /// </summary>
    public partial class RepositoryListView : UserControl
    {
        public RepositoryListView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            RepositoryListViewModel repositoryListVm = (RepositoryListViewModel)DataContext;

            var selectedSourcefile = RepositoryTreeView.SelectedItem as SourcefileViewModel;

            repositoryListVm.TagSourcefile(selectedSourcefile);
        }
    }
}
