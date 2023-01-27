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

namespace Pet_Project_1_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Method_ComboBox.SelectedIndex = 0;
        }

        private void Solve_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextBox box in TexBoxes_Grid.Children)
                box.Clear();
            MaxIterations_TextBox.Clear();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Close program?", "Close", MessageBoxButton.YesNo);
            if (response == MessageBoxResult.Yes)
                Close();
        }

        private void Method_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaxIterations_TextBox.Visibility = Method_ComboBox.SelectedIndex == 1 ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
