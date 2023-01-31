using System;

namespace Pet_Project_1_WPF
{
    public class Functions
    {
        public double Function(double x, int funcNum)
        {
            switch(funcNum)
            {
                case 0: return x * x - 4;
                case 1: return Math.Pow((x - 2), 3) + 5;
                default: throw new Exception("Function number out of range");
            }
        }

        public double Derivate(double x, double width, int funcNum)
        {
            return (Function(x + width, funcNum) - Function(x, funcNum)) / width;
        }

        public double SecondDerivate(double x, double width, int funcNum)
        {
            return (Function(x + width, funcNum) + Function(x - width, funcNum) - 2 * Function(x, funcNum)) / (width * width);
        }
    }
}
