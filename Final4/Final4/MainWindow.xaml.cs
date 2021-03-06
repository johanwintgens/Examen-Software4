﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
using Newtonsoft.Json;

namespace Final4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<Personen> PersonenLijst = new ObservableCollection<Personen>();
        private string jsonData;
        private string fileDir = Environment.CurrentDirectory + @"\so4data.json";
        private Rootobject data;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            jsonData = File.ReadAllText(fileDir);
            data = JsonConvert.DeserializeObject<Rootobject>(jsonData);
            foreach (var persoon in data.Personen)
            {
                PersonenLijst.Add(persoon);
            }
            personenListBox.ItemsSource = PersonenLijst;
        }

        private void personenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            summaryGrid.DataContext = personenListBox.SelectedItem;
        }

        private void ageButton_Click(object sender, RoutedEventArgs e)
        {
            var jongVolk = from persoon in PersonenLijst
                           where persoon.age < 30
                           select persoon;
            MessageBox.Show(jongVolk.Count() + " personen zijn jonger dan 30.");
        }

        private void moreAgeButton_Click(object sender, RoutedEventArgs e)
        {
            PersonenLijst[personenListBox.SelectedIndex].age++;
        }
    }

    public class GenderToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string t = value as string;
            if (t == "male")
            {
                return Environment.CurrentDirectory + @"/male.png";
            }
            else
            {
                return Environment.CurrentDirectory + @"/female.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
