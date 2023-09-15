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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using StockroomBinar.Class;

namespace StockroomBinar.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
           // DeliveriesView.ItemsSource = Connect.bd.Deliveries.ToList();
            CountPlastOnStock.Text = (Connect.bd.PlasticStor.Where(p => p.ID != 0).Count()).ToString();
            if (Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() == 1|| Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() == 21)
            {
                ColorCount.Text = "цвет";
            }
            if (Connect.bd.PlasticStor.Where(p => p.ID != 0).Count()> 1&& Connect.bd.PlasticStor.Where(p => p.ID != 0).Count()<5|| Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() > 21 && Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() <=24)
            {
                ColorCount.Text = "цвета";
            }
            if (Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() >=5 && Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() <=20)
            {
                ColorCount.Text = "цветов";
            }
            if (Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() >= 21 && Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() <= 20)
            {
                ColorCount.Text = "цветов";
            }
            if(Connect.bd.PlasticStor.Where(p => p.ID != 0).Count() >= 25)
            {
                ColorCount.Text = "цвета";
            }

            pieChart();
            for (int j = 0; j < 3; j++)
            {
                doud("qwe",j);
            }
            doud2("qweee",3);



        }
        public void doud(string nam, int count)
        {

            seriesCollection = new SeriesCollection
            {
               
                new PieSeries
                {
                    Title=nam,
                    Values=new ChartValues<ObservableValue> {new ObservableValue(count)},
                    DataLabels=true
                }
               
            };
            DataContext = this;
        }

        public void doud2(string nam, int count)
        {

            seriesCollection2 = new SeriesCollection
            {

                new PieSeries
                {
                    Title=nam,
                    Values=new ChartValues<ObservableValue> {new ObservableValue(count)},
                    DataLabels=true
                }

            };
            DataContext = this;
        }

        public void pieChart()
        {
            Pointlabel = ChartPoint => string.Format("{0}({1:P)", ChartPoint.Y, ChartPoint.Participation);
            DataContext = this;
        }

        public Func<ChartPoint, string> Pointlabel { get; set; }
        public SeriesCollection seriesCollection { get; set; }
        public SeriesCollection seriesCollection2 { get; set; }
        private void info_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
