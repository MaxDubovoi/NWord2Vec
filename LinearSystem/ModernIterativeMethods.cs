using System;

namespace LinearSystems
{
    public class ModernIterativeMethods
    {
        private double InnerProduct(int n, double[] a, double[] b)
        {
            double sum = 0.0;

            for (int i = 0; i < n; i++)
                sum += a[i] * b[i];

            return sum;
        }

        public double ConjugateGradient(
            int n,
            int maxIterations,
            double tolerance,
            double[,] a,
            double[] b,
            double[] x)
        {
            double t = 0.0;
            double alpha = 0;
            double rzold = 0.0;
            double rznew = 0.0;
            double[] p = new double[n];
            double[] r = new double[n];
            double[] z = new double[n];
            double[] ap = new double[n];
            double[,] pinv = new double[n, n];
            int its = 0;
            DirectMethods dm = new DirectMethods();

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    pinv[i, j] = a[i, j];

            dm.CholeskyInverse(pinv, n);

            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    pinv[j, i] = pinv[i, j];

            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;

                for (int j = 0; j < n; j++)
                    sum += pinv[i, j] * b[j];

                x[i] = sum;
            }

            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;

                for (int j = 0; j < n; j++)
                    sum += a[i, j] * x[j];
                
                r[i] = b[i] - sum;

                sum = 0.0;

                for (int j = 0; j < n; j++)
                    sum += pinv[i, j] * r[j];

                z[i] = sum;
                p[i] = z[i];
            }

            rzold = InnerProduct(n, r, z);

            while (its < maxIterations)
            {
                its++;

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        ap[i] = a[i, j] * p[j];

                double denom = InnerProduct(n, p, ap);

                if (denom < tolerance)
                    break;

                alpha = rzold / denom;

                for (int i = 0; i < n; i++)
                    x[i] += alpha * p[i];

                for (int i = 0; i < n; i++)
                    r[i] -= alpha * ap[i];              
                
                t = Math.Sqrt(InnerProduct(n, r, r));

                if (t < tolerance)
                    break;

                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;

                    for (int j = 0; j < n; j++)
                        sum += pinv[i, j] * r[j];

                    z[i] = sum;
                }

                rznew = InnerProduct(n, r, z);

                for (int i = 0; i < n; i++)
                    p[i] = z[i] + p[i] * rznew / rzold;

                rzold = rznew;
            }

            return t;
        }

        public double ModifiedRichardson(
            int n,
            int maxIterations,
            double tolerance,
            double[,] a,
            double[] b,
            double[] x0)
        {
            double omega;
            double t = 0.0;
            double[] x = new double[n];
            double[] em = new double[8];
            double[] val = new double[n + 1];
            double[,] ar = new double[n + 1, n + 1];
            double[,] vec = new double[n + 1, n + 1];
            int its = 0;
            EigenVVReal eigenVVReal = new EigenVVReal();
            
            em[0] = 1.0e-12;
            em[2] = 1.0e-10;
            em[4] = 50.0;
            
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    ar[i + 1, j + 1] = a[i, j];

            int m = eigenVVReal.reaeig3(ar, n, em, val, vec);

            omega = 2.0 / (val[1] + val[n]);

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

                    x[i] = x0[i] + omega * (b[i] - sum);
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
    }
}
