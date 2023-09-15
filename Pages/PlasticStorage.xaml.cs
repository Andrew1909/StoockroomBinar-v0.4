using MaterialDesignColors;
using StockroomBinar.BD;
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
    /// Логика взаимодействия для PlasticStorage.xaml
    /// </summary>
    public partial class PlasticStorage : Page
    {
        public RecyclingPlastic recyclingPlastic = new RecyclingPlastic();
        public PlasticStor plasticStor = new PlasticStor();
        string TypeNamePlast;//для запси названия типа платика, выбранного из комбобокс
        public PlasticStorage()
        {
            InitializeComponent();
            MyFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            var a = Connect.bd.PlasticType.Where(p => p.ID != 0).Count(); //считаем количество типов пластика
            PlastType.Items.Add("Все типы");
            for (int j = 1; j <= int.Parse(a.ToString()); j++)
            {
                var a1 = Connect.bd.PlasticType.First(p => p.ID == j);
                
                PlastType.Items.Add(a1.NameType.ToString());
                

            }
            PlastType.SelectedIndex = 0;
            var plast = Connect.bd.PlasticStor.Where(p => p.ID != 0).Count(); //считаем количество пластика
            plast++;

            for (int i = 0; i < int.Parse(plast.ToString()); i++)
            {
                var plast2 = Connect.bd.PlasticStor.Where(p => p.Weight.Value <= 0.2m ).Count();//считаем количество пластика
                if (plast2 != 0)
                {
                    var objX = Connect.bd.PlasticStor.First(p => p.Weight.Value <= 0.2m);
                    recyclingPlastic.ID = objX.ID;
                    recyclingPlastic.ColorNameRecucling = objX.ColorName;
                    recyclingPlastic.PlasticTypeRecucling = objX.PlasticType;
                    recyclingPlastic.ManufacturerRecucling = objX.Manufacturer;
                    recyclingPlastic.WeightRecucling = objX.Weight;
                    Connect.bd.RecyclingPlastic.Add(recyclingPlastic);
                    Connect.bd.SaveChanges();
                    Connect.bd.PlasticStor.Remove(objX);
                    Connect.bd.SaveChanges();
                }
            }
            PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.ToList();
        }



        private void PlastType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = PlastType.SelectedIndex;
            if (PlastType.SelectedIndex == index)
            {
                if (index > 0)
                { 
                    var a1 = Connect.bd.PlasticType.First(p => p.ID == index);
                    TypeNamePlast = a1.NameType;
                    PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.Where(p => p.PlasticType == TypeNamePlast).ToList();
                }
            }
            if (PlastType.SelectedIndex == 0)
            {   
                PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.ToList();
            }
        }

        private void SearchColor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.Where(p => p.ColorName.StartsWith(SearchColor.Text)).ToList();
        }

        private void AddPlatic_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new AddPlasticPage());
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var a = PlastitStoageView.SelectedItem as PlasticStor;
            if (a != null)
            {
                MyFrame.Navigate(new EditInfoPlastPage(a));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
