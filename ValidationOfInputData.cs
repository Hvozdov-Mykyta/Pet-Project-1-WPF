using System;
using System.Windows;
using System.Windows.Controls;

namespace Pet_Project_1_WPF
{
    public class ValidationOfInputData
    {
        private Functions F = new Functions();

        public void CheckAccuracyCorrectness(ref double accuracy, ref TextBox accuracyTextBox)
        {
            if (accuracy < 1e-15 || accuracy > 1e-3)
            {
                accuracy = 1e-6;
                accuracyTextBox.Text = accuracy.ToString();
            }
        }

        public bool CheckBordersCorrectness(ref double left, ref double right, ref TextBox leftTextBox, ref TextBox rightTextBox)
        {
            if (left == right)
                return true;

            if (left > right)
            {
                (right, left) = (left, right);
                leftTextBox.Text = left.ToString();
                rightTextBox.Text = right.ToString();
            }

            return false;
        }

        public bool CheckIfBorderIsRoot(double point, double accuracy, int funcNum, ref TextBox rootTextBox, ref TextBox iterationsTextBox)
        {
            if (Math.Abs(F.Function(point, funcNum)) < accuracy)
            {
                rootTextBox.Text = point.ToString();
                iterationsTextBox.Text = "0";
                return true;
            }
            return false;
        }

        public bool CheckForSingleRootOnInterval(double left, double right, int funcNum)
        {
            if (F.Function(left, funcNum) * F.Function(right, funcNum) > 0)
                return true;
            else
                return false;
        }

        public bool CheckConvergenceIsGuaranted(double left, double right, double derivateWidth, int funcNum, out double point)
        {
            if (F.Function(left, funcNum) * F.SecondDerivate(left, derivateWidth, funcNum) < 0)
            {
                if (F.Function(right, funcNum) * F.SecondDerivate(right, derivateWidth, funcNum) < 0)
                {
                    point = 0;
                    return true;
                }
                point = right;
            }
            else
                point = left; 
            return false;
        }
    }
}
