using System;

namespace Pet_Project_1_WPF
{
    public class RootSearchMethods
    {
        public double Halving(double left, double right, double accuracy, ref int counter)
        {
            double middle = left;
            while (Math.Abs(right - left) > accuracy)
            {
                middle = (right + left) / 2;
                counter++;
                if (Math.Abs(Function(middle)) < accuracy)
                    break;
                else if (Function(left) * Function(middle) < 0)
                    right = middle;
                else
                    left = middle;
            }
            return middle;
        }

        public double Newton(double left, double right, double accuracy, int maxIterations, ref int counter)
        {
            double delta, x;
            double derivateWidth = accuracy / 100;
            if (Function(left) * SecondDerivate(left, derivateWidth) < 0)
            {
                if (Function(right) * SecondDerivate(right, derivateWidth) < 0)
                {
                    throw new Exception("Convergence of iterations is not guaranted");
                }
                x = right;
            }
            else
                x = left;
            for (int i = 0; i < maxIterations; i++)
            {
                delta = Function(x) / Derivate(x, derivateWidth);
                x -= delta;
                if (Math.Abs(delta) < accuracy)
                {
                    counter = ++i;
                    return x;
                }
            }
            throw new Exception("No solution was found for the given number of iterations");
        }

        public double Function(double X)
        {
            return X * X - 4;
        }



        private double Derivate(double x, double width)
        {
            return (Function(x + width) - Function(x)) / width;
        }

        private double SecondDerivate(double x, double width)
        {
            return (Function(x + width) + Function(x - width) - 2 * Function(x)) / (width * width);
        }
    }
}
