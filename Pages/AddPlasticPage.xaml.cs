using StockroomBinar.BD;
using StockroomBinar.Class;
using StockroomBinar.DialogWindow;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockroomBinar.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPlasticPage.xaml
    /// </summary>
    public partial class AddPlasticPage : Page
    {
        public PlasticStor plasticStor = new PlasticStor();
        public ColorPlastic colorPlastic = new ColorPlastic();
        public PlasticType plasticType = new PlasticType();
        public DefectivePlastic defectivePlastic = new DefectivePlastic();

        string ColorNamePlast;//для запси названия цвета платика, выбранного из комбобокс
        string TypeNamePlast;//для запси названия типа платика, выбранного из комбобокс
        int DefectiveCoilsCount=0;//количество бракованных катушек
        public AddPlasticPage()
        {
            InitializeComponent();
           

            var a = Connect.bd.ColorPlastic.Where(p => p.ID != 0).Count();
            AddColordNamePlastic.Items.Add("Выберите цвет платика");
            for (int j = 1; j <= int.Parse(a.ToString()); j++)
            {
                var a1 = Connect.bd.ColorPlastic.First(p => p.IDInside == j);
                AddColordNamePlastic.Items.Add(a1.NameColor.ToString());
            }
            AddColordNamePlastic.SelectedIndex = 0;


            var b = Connect.bd.PlasticType.Where(p => p.ID != 0).Count();
            AddTypePlastic.Items.Add("Выберите тип пластика");
            for (int j = 1; j <= int.Parse(b.ToString()); j++)
            {
                var b1 = Connect.bd.PlasticType.First(p => p.ID == j);
                AddTypePlastic.Items.Add(b1.NameType.ToString());
            }
            AddTypePlastic.SelectedIndex = 0;
            CountDefectCoilsText.Visibility = Visibility.Hidden;
        }

        private void AddPlast_Click(object sender, RoutedEventArgs e)
        {
            int index1 = AddColordNamePlastic.SelectedIndex;
            if (AddColordNamePlastic.SelectedIndex == index1)
            {
                if (index1 > 0)
                {
                    var a1 = Connect.bd.ColorPlastic.First(p => p.IDInside == index1);
                    ColorNamePlast = a1.NameColor;
                }
            }
            int index2 = AddTypePlastic.SelectedIndex;
            if (AddTypePlastic.SelectedIndex == index2)
            {
                if (index2 > 0)
                {
                    var a1 = Connect.bd.PlasticType.First(p => p.ID == index2);
                    TypeNamePlast = a1.NameType;
                }
            }

            var objA = Connect.bd.PlasticStor.Where(p => p.ColorName == ColorNamePlast).Count();
            if (objA != 0)
            {
                if(DefectiveCoilsCount!=0) SaveDefectivePlast();
                var ObjB=Connect.bd.PlasticStor.First(p=>p.ColorName == ColorNamePlast);
                ObjB.Weight = (decimal.Parse(AddWightPlastic.Text)- DefectiveCoilsCount) + ObjB.Weight;
                ObjB.NumberСoils=(int.Parse(AddCoilsPlastic.Text)- DefectiveCoilsCount) + ObjB.NumberСoils;
                ObjB.Notes = AddNotesPlastic.Text;
                Connect.bd.SaveChanges();
                MessageBox.Show("Пластик добавлен!");
                MyFrame.Navigate(new AddPlasticPage());
            }

            if (objA == 0)
            {
                if (DefectiveCoilsCount != 0) SaveDefectivePlast();
                var a = Connect.bd.PlasticStor.Where(p => p.ID != 0).Count(); //считаем количество типов пластика
                plasticStor.IDInsaid = int.Parse(a.ToString()) + 1;
                plasticStor.ColorName = ColorNamePlast;
                plasticStor.PlasticType = TypeNamePlast;
                plasticStor.Weight = decimal.Parse(AddWightPlastic.Text)- DefectiveCoilsCount;
                plasticStor.NumberСoils = int.Parse(AddCoilsPlastic.Text)- DefectiveCoilsCount;
                plasticStor.Manufacturer = AddManufactPlastic.Text;
                plasticStor.Notes = AddNotesPlastic.Text;

                Connect.bd.PlasticStor.Add(plasticStor);
                Connect.bd.SaveChanges();
                MessageBox.Show("Пластик добавлен!");
                MyFrame.Navigate(new AddPlasticPage());
            }

        }


        void SaveDefectivePlast()
        {
            var a = Connect.bd.PlasticStor.Where(p => p.ID != 0).Count(); //считаем количество типов пластика
            defectivePlastic.ID = int.Parse(a.ToString()) + 1;
            defectivePlastic.ColorName= ColorNamePlast;
            defectivePlastic.PlasticType = TypeNamePlast;
            defectivePlastic.Manufacturer= AddManufactPlastic.Text;
            defectivePlastic.Weight = DefectiveCoilsCount;
            defectivePlastic.NumberСoils = DefectiveCoilsCount;
            Connect.bd.DefectivePlastic.Add(defectivePlastic);
            Connect.bd.SaveChanges();
        }

        private void AddCoilsPlastic_SelectionChanged(object sender, RoutedEventArgs e)
        {
            AddWightPlastic.Text = AddCoilsPlastic.Text;
        }

        private void AddColordNamePlastic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index2 = AddColordNamePlastic.SelectedIndex;
            if (AddColordNamePlastic.SelectedIndex == index2)
            {
                if (index2 > 0)
                {
                    var a1 = Connect.bd.ColorPlastic.First(p => p.IDInside == index2);
                    var objA = Connect.bd.PlasticStor.Where(p => p.ColorName == a1.NameColor).Count(); //проверка, есть ли цвет или тип в главной таблице
                    if (objA != 0)
                    {
                        var ObjB = Connect.bd.PlasticStor.First(p => p.ColorName == a1.NameColor);
                        var objC = Connect.bd.PlasticType.First(p => p.NameType == ObjB.PlasticType);
                        AddTypePlastic.SelectedIndex = objC.ID;
                        AddManufactPlastic.Text = ObjB.Manufacturer;
                        AddNotesPlastic.Text = ObjB.Notes;
                    }
                }
            }  
        }

        private void AddNewNameColor_Click(object sender, RoutedEventArgs e)
        {
            AddNewNameColorWindow ColorWindow = new AddNewNameColorWindow();
            var a = Connect.bd.ColorPlastic.Where(p => p.ID != null).Count();
            if (ColorWindow.ShowDialog() == true)
            {
                colorPlastic.IDInside = a + 1;
                colorPlastic.NameColor = ColorWindow.Color;
                Connect.bd.ColorPlastic.Add(colorPlastic);
                Connect.bd.SaveChanges();
                MessageBox.Show("Новый цвет добавлен!");
                MyFrame.Navigate(new AddPlasticPage());
            }
        }

        private void AddNewNameType_Click(object sender, RoutedEventArgs e)
        {
            AddNewTypeWindow TypeWindow = new AddNewTypeWindow();
            var a = Connect.bd.PlasticType.Where(p => p.ID != null).Count();
            if (TypeWindow.ShowDialog() == true)
            {
                plasticType.ID = a + 1;
                plasticType.NameType = TypeWindow.Type;
                Connect.bd.PlasticType.Add(plasticType);
                Connect.bd.SaveChanges();
                MessageBox.Show("Новый тип пластика добавлен!");
                MyFrame.Navigate(new AddPlasticPage());
            }
        }

        private void Brak_Checked(object sender, RoutedEventArgs e)
        {
            AddDefectiveCoilsWindow TypeWindow = new AddDefectiveCoilsWindow();
            var a = Connect.bd.PlasticType.Where(p => p.ID != null).Count();
            if (TypeWindow.ShowDialog() == true)
            {
                DefectiveCoilsCount = int.Parse(TypeWindow.Defective.ToString());
                CountDefectCoilsText.Visibility = Visibility.Visible;
                if (DefectiveCoilsCount == 1) CountDefectCoilsText.Text = DefectiveCoilsCount + " катушка будет списана как брак";
                //if (DefectiveCoilsCount>1 && DefectiveCoilsCount <=4) CountDefectCoilsText.Text = DefectiveCoilsCount + " катушки будут списаны как брак";
                else CountDefectCoilsText.Text = DefectiveCoilsCount + " катушек будет списано как брак";
            }
            else
            {
                Brak.IsChecked = false;
            }
        }

        private void Brak_Unchecked(object sender, RoutedEventArgs e)
        {
            CountDefectCoilsText.Visibility = Visibility.Hidden;
            DefectiveCoilsCount = 0;
        }
    }
}
