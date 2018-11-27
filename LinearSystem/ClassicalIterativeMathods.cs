using System;

namespace LinearSystems
{
    public class ClassicalIterativeMethods
    {
        public double Jacobi(
            int n,
            int maxIterations,
            double tolerance,
            double[,] a,
            double[] b,
            double[] x0)
        {
            double t = 0.0;
            double[] x = new double[n];
            int its = 0;

            for (int i = 0; i < n; i++)
                x[i] = x0[i];

            while (its < maxIterations)
            {
                its++;

                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;

                    for (int j = 0; j != i && j < n; j++)
                        sum += a[i, j] * x0[j];

                    x[i] = (b[i] - sum) / a[i, i];
                }

                t = 0.0;

                for (int i = 0; i < n; i++)
                    t += Math.Pow(x[i] - x0[i], 2.0);

                t = Math.Sqrt(t);

                if (t < tolerance)
                    break;

                for (int i = 0; i < n; i++)
                    x0[i] = x[i];
            }

            return t;
        }
                
        public double GaussSeidel(
            int n,
            int maxIterations,
            double tolerance,
            double[,] a,
            double[] b,
            double[] x0)
        {
            double t = 0.0;
            double[] x = new double[n];
            int its = 0;

            for (int i = 0; i < n; i++)
                x[i] = x0[i];

            while (its < maxIterations)
            {
                its++;

                for (int i = 0; i < n; i++)
                {
                    double sum0 = 0;
                    double sum1 = 0;

                    for (int j = 0; j <= i - 1; j++)
                        sum0 += a[i, j] * x[j];

                    for (int j = i + 1; j < n; j++)
                        if (i != n)
                            sum1 += a[i, j] * x0[j];

                    x[i] = (b[i] - sum0 - sum1) / a[i, i];
                }

                t = 0.0;

                for (int i = 0; i < n; i++)
                    t += Math.Pow(x[i] - x0[i], 2.0);

                t = Math.Sqrt(t);

                if (t < tolerance)
                    break;

                for (int i = 0; i < n; i++)
                    x0[i] = x[i];
            }

            return t;
        }

        public double SOR(
            int n,
            int maxIterations,
            double omega,
            double tolerance,
            double[,] a,
            double[] b,
            double[] x0)
        {
            double t = 0.0;
            double[] x = new double[n];
            int its = 0;

            for (int i = 0; i < n; i++)
                x[i] = x0[i];

            while (its < maxIterations)
            {
                its++;

                for (int i = 0; i < n; i++)
                {
                    double sum0 = 0;
                    double sum1 = 0;

                    for (int j = 0; j <= i - 1; j++)
                        sum0 += a[i, j] * x[j];

                    for (int j = i + 1; j < n; j++)
                        if (i != n)
                            sum1 += a[i, j] * x0[j];

                    x[i] = (1.0 - omega) * x0[i] +
                        omega * (b[i] - sum0 - sum1) / a[i, i];
                }

                t = 0.0;

                for (int i = 0; i < n; i++)
                    t += Math.Pow(x[i] - x0[i], 2.0);

                t = Math.Sqrt(t);

                if (t < tolerance)
                    break;

                for (int i = 0; i < n; i++)
                    x0[i] = x[i];
            }

            return t;
        }

        private double InnerProduct(int n, double[] a, double[] b)
        {
            double sum = 0.0;

            for (int i = 0; i < n; i++)
                sum += a[i] * b[i];

            return sum;
        }

        public double GradientMethod(
            int n,
            int maxIterations,
            double tolerance,
            double[,] a,
            double[] b,
            double[] x0)
        {
            double t = 0.0;
            double[] r = new double[n];
            double[] x = new double[n];
            double[] y = new double[n];
            int its = 0;

            for (int i = 0; i < n; i++)
                x[i] = x0[i];

            while (its < maxIterations)
            {
                its++;

                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;

                    for (int j = 0; j < n; j++)
                        sum += a[i, j] * x0[j];

                    r[i] = b[i] - sum;
                }

                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;

                    for (int j = 0; j < n; j++)
                        sum += a[i, j] * r[j];

                    y[i] = sum;
                }

                double alpha = InnerProduct(n, r, r);

                alpha /= InnerProduct(n, r, y);

                for (int i = 0; i < n; i++)
                    x[i] = x0[i] + alpha * r[i];

                t = 0.0;

                for (int i = 0; i < n; i++)
                    t += Math.Pow(x[i] - x0[i], 2.0);

                t = Math.Sqrt(t);

                if (t < tolerance)
                    break;

                for (int i = 0; i < n; i++)
                    x0[i] = x[i];
            }

            return t;
        }
    }
}