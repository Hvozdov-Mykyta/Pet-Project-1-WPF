using System;
using System.Windows;
using System.Windows.Controls;

namespace Pet_Project_1_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Method_ComboBox.SelectedIndex = 0;
        }

        private void Solve_Button_Click(object sender, RoutedEventArgs e)
        {
            double left, right, accuracy, result = 0;
            int counter = 0, maxIterations;

            try
            {
                left = Convert.ToDouble(Left_TextBox.Text);
                right = Convert.ToDouble(Right_TextBox.Text);
                accuracy = Convert.ToDouble(Accuracy_TextBox.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }

            if (!CheckDataCorrectness(ref left, ref right, ref accuracy, counter))
                return;

            switch (Method_ComboBox.SelectedIndex)
            {
                case 0:
                    {
                        if (RSM.Function(left) * RSM.Function(right) > 0)
                        {
                            MessageBox.Show("There is no SINGLE root on interval");
                            return;
                        }
                        result = RSM.Halving(left, right, accuracy, ref counter);
                    }
                    break;
                case 1:
                    {
                        try
                        {
                            maxIterations = Convert.ToInt32(MaxIterations_TextBox.Text);
                            result = RSM.Newton(left, right, accuracy, maxIterations, ref counter);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    break;
            }
            Root_TextBox.Text = result.ToString();
            Iterations_TextBox.Text = counter.ToString();
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


        private bool CheckDataCorrectness(ref double left, ref double right, ref double accuracy, double counter)
        {
            if (accuracy < 1e-15 || accuracy > 1e-3)
            {
                accuracy = 1e-6;
                Accuracy_TextBox.Text = accuracy.ToString();
            }

            if (left > right)
            {
                (right, left) = (left, right);
                Left_TextBox.Text = left.ToString();
                Right_TextBox.Text = right.ToString();
            }

            if (left == right)
            {
                MessageBox.Show("Left and right borders must be difference.");
                return false;
            }

            if (Math.Abs(RSM.Function(left)) < accuracy)
            {
                Root_TextBox.Text = left.ToString();
                Iterations_TextBox.Text = counter.ToString();
                return false;
            }

            if (Math.Abs(RSM.Function(right)) < accuracy)
            {
                Root_TextBox.Text = right.ToString();
                Iterations_TextBox.Text = counter.ToString();
                return false;
            }

            return true;
        }

        private RootSearchMethods RSM = new RootSearchMethods();
    }
}
