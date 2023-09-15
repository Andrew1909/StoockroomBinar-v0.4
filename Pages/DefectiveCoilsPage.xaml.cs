using StockroomBinar.Class;
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

namespace StockroomBinar.Pages
{
    /// <summary>
    /// Логика взаимодействия для DefectiveCoilsPage.xaml
    /// </summary>
    public partial class DefectiveCoilsPage : Page
    {
        public DefectiveCoilsPage()
        {
            InitializeComponent();
            PlastitDefectiveView.ItemsSource = Connect.bd.DefectivePlastic.ToList();
        }

        private void AddRecyclingName_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AddRecyclingName_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void PlastType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchColor_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void RecyclingNameDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
