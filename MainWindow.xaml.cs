using System;
using System.Windows;
using System.Windows.Controls;

namespace Pet_Project_1_WPF
{
    public partial class MainWindow : Window
    {
        private RootSearchMethods RSM;
        private ValidationOfInputData VID;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Method_ComboBox.SelectedIndex = 0;
            Function_ComboBox.SelectedIndex = 0;
            RSM = new RootSearchMethods();
            VID = new ValidationOfInputData();
        }


        private void Solve_Button_Click(object sender, RoutedEventArgs e)
        {
            double left, right, accuracy, result = 0;
            int counter = 0, maxIterations = 0;
            int methodNum = Method_ComboBox.SelectedIndex, functionNum = Function_ComboBox.SelectedIndex;

            try
            {
                left = Convert.ToDouble(Left_TextBox.Text);
                right = Convert.ToDouble(Right_TextBox.Text);
                accuracy = Convert.ToDouble(Accuracy_TextBox.Text);
                if(Method_ComboBox.SelectedIndex == 1)
                    maxIterations = Convert.ToInt32(MaxIterations_TextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            VID.CheckAccuracyCorrectness(ref accuracy, ref Accuracy_TextBox);

            if (VID.CheckBordersCorrectness(ref left, ref right, ref Left_TextBox, ref Right_TextBox))
            {
                MessageBox.Show("Left and right borders must be difference.");
                return;
            }

            if (VID.CheckIfBorderIsRoot(left, accuracy, functionNum, ref Root_TextBox, ref Iterations_TextBox))
                return;

            if (VID.CheckIfBorderIsRoot(right, accuracy, functionNum, ref Root_TextBox, ref Iterations_TextBox))
                return;

            switch (methodNum)
            {
                case 0:
                    {
                        if (VID.CheckForSingleRootOnInterval(left, right, functionNum))
                        {
                            MessageBox.Show("There is no SINGLE root on interval");
                            return;
                        }
                        result = RSM.Halving(left, right, accuracy, ref counter, functionNum);
                    }
                    break;
                case 1:
                    {
                        double point, derivateWidth = accuracy/100;
                        if (VID.CheckConvergenceIsGuaranted(left, right, derivateWidth, functionNum, out point))
                        {
                            MessageBox.Show("Convergence of iterations is not guaranted");
                            return;
                        }
                        result = RSM.Newton(point, accuracy, maxIterations, ref counter, functionNum, derivateWidth);
                        if (counter == maxIterations)
                        {
                            MessageBox.Show($"Root wasn`t found for {maxIterations} iterations");
                            return;
                        }
                    }
                    break;
            }
            if (result is double.NaN)
            {
                MessageBox.Show("Unable to solve with actual input data");
                return;
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
    }
}
