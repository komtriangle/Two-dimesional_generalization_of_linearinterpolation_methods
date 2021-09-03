using System;

namespace ConsoleApp17
{
    class Program
    {
        private static double x0, y0, x1, y1, x2, y2;
        static  double f(double x, double y)
        {
            return y - Math.Tan(x);
        }
        static double g(double x, double y)
        {
            return x * y - 1.5;
        }
        static (double, double) InitialPointChoice(double Xm, double Ym, double Xp, double Yp, int area)
        {
            var point = (0.0, 0.0);
            double dl = 0.1;
            double curmin = 100_000;
            while(curmin > 99_999)
            {
                for (double x = Xm; x < Xp; x += dl)
                {
                    for (double y = Ym; y < Yp; y += dl)
                    {
                        double fc = f(x, y);
                        double gc = g(x, y);
                        switch (area)
                        {
                            case 1:
                                if (fc <= 0 || gc <= 0)
                                    continue;
                                break;
                            case 2:
                                if (fc >= 0 || gc >= 0)
                                    continue;
                                break;
                            case 3:
                                if (fc <= 0 || gc >= 0)
                                    continue;
                                break;
                            case 4:
                                if (fc >= 0 || gc <= 0)
                                    continue;
                                break;
                            default:
                                break;
                        }
                        double dv = Math.Abs(fc) + Math.Abs(gc);
                        if (dv > curmin)
                            continue;
                        point.Item1 = x;
                        point.Item2 = y;
                        curmin = dv;

                    }
                    
                }
                if (curmin <= 99_999)
                    break;
                dl /= 2;
            }
           return point;
        }
        static double stri(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return 0.5 * Math.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));
        }
        static void TwoDemetnialLinearInterpolation()
        {
            double x = 10, y = 10;
            double xls = 100_000;
            double yls = 100_000;
            double dv = Math.Abs(x - xls) + Math.Abs(y - yls);
            while(dv > 0.0000005)
            {
                double zf0 = f(x0, y0);
                double zg0 = g(x0, y0);

                double zf1 = f(x1, y1);
                double zg1 = g(x1, y1);

                double zf2 = f(x2, y2);
                double zg2 = g(x2, y2);

                double Af = (y1 - y0) * (zf2 - zf0) - (y2 - y0) * (zf1 - zf0);
                double Bf = (x2 - x0) * (zf1 - zf0) - (x1 - x0) * (zf2 - zf0);
                double Cf = (x1 - x0) * (y2 - y0) - (x2 - x0) * (y1 - y0);
                double Df = -1 * x0 * Af - y0 * Bf - zf0 * Cf;

                double Ag = (y1 - y0) * (zg2 - zg0) - (y2 - y0) * (zg1 - zg0);
                double Bg = (x2 - x0) * (zg1 - zg0) - (x1 - x0) * (zg2 - zg0);
                double Cg = (x1 - x0) * (y2 - y0) - (x2 - x0) * (y1 - y0);
                double Dg = -1*x0 * Ag - y0 * Bg - zg0 * Cg;

                double dt = Af * Bg - Ag * Bf;
                if (Math.Abs(dt) < 0.000001)
                    break;
                x = (-1 * Df * Bg + Dg * Bf) / dt;
                y = (-1 * Af * Dg + Ag * Df) / dt;

                double zfc = f(x, y);
                double zgc = g(x, y);

                dv = Math.Abs(x - xls) + Math.Abs(y - yls);

                xls = x;
                yls = y;

                double s0 = stri(x1, y1, x2, y2, x, y);
                double s1 = stri(x0, y0, x2, y2, x, y);
                double s2 = stri(x0, y0, x1, y1, x, y);

                if(s0>=s1 && s0 >= s2)
                {
                    x0 = x1;
                    y0 = y1;
                    x1 = x2;
                    y1 = y2;
                    x2 = x;
                    y2 = y;
                    continue;
                }
                if(s1 >= s0 && s1 >= s2)
                {
                    x1 = x2;
                    y1 = y2;
                    x2 = x;
                    y2 = y;
                    continue;
                }
                if(s2>=s0 && s2 >= s1)
                {
                    x2 = x;
                    y2 = y;
                    continue;
                }
            }

        }
        static void Main(string[] args)
        {
            double xp0 = 0, yp1 = 0;
            double xp1 = 0, yp2 = 5;
            double xp2 = Math.PI/2, yp3 = 5;
            double xp3 = Math.PI / 2, yp4 = 0;

            var point1 = InitialPointChoice(xp0, yp1, xp2, yp3, 1);
            var point2 = InitialPointChoice(xp0, yp1, xp2, yp3, 2);
            var point3 = InitialPointChoice(xp0, yp1, xp2, yp3, 3);
            x0 = point1.Item1;
            y0 = point1.Item2;
            x1 = point2.Item1;
            y1 = point2.Item2;
            x2 = point3.Item1;
            y2 = point3.Item2;

            for(int i = 0; i < 5; i++)
            {
                TwoDemetnialLinearInterpolation();
                Console.WriteLine($"X: {x2} Y: {y2} f(x,y): {f(x2, y2)} g(x,y): {g(x2,y2)}");

            }

            
           

        }
    }
}
