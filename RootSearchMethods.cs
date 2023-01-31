using System;

namespace Pet_Project_1_WPF
{
    public class RootSearchMethods
    {
        private Functions F = new Functions();

        public double Halving(double left, double right, double accuracy, ref int counter, int functionNumber)
        {
            double middle = left;
            while (Math.Abs(right - left) > accuracy)
            {
                middle = (right + left) / 2;
                counter++;
                if (Math.Abs(F.Function(middle, functionNumber)) < accuracy)
                    break;
                if (F.Function(left, functionNumber) * F.Function(middle, functionNumber) < 0)
                    right = middle;
                else
                    left = middle;
            }
            return middle;
        }


        public double Newton(double point, double accuracy, int maxIterations, ref int counter, int funcNum, double derivateWidth)
        {
            double delta;
            for (int i = 0; i < maxIterations; i++)
            {
                delta = F.Function(point, funcNum) / F.Derivate(point, derivateWidth, funcNum);
                point -= delta;
                if (Math.Abs(delta) < accuracy)
                {
                    counter = ++i;
                    break;
                }
            }
            return point;
        }

    }
}
