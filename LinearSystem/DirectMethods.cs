using System;

namespace LinearSystems
{
    public class DirectMethods
    {
        private void Substitute(
            int n,
            double[,] w,
            double[] b,
            double[] x,
            int[] pivot)
        {
            double sum;
            int i, j, n1 = n - 1;

            if (n == 1)
            {
                x[0] = b[0] / w[0, 0];
                return;
            }

            // forward substitution
            x[0] = b[pivot[0]];
            
            for (i = 1; i < n; i++)
            {
                for (j = 0, sum = 0; j < i; j++)
                    sum += w[i, j] * x[j];
                x[i] = b[pivot[i]] - sum;
            }
            
            // backward substitution
            x[n1] /= w[n1, n1];
            
            for (i = n - 2; i >= 0; i--)
            {
                for (j = i + 1, sum = 0; j < n; j++)
                    sum += w[i, j] * x[j];
                x[i] = (x[i] - sum) / w[i, i];
            }
        }

        public bool GaussianElimimation(
            int n,
            double[,] w,
            double[] b,
            double[] x)
        // returns false if matrix is singular
        {
            double awikod, col_max, ratio, row_max, temp;
            double[] d = new double[n];
            int flag = 1, i, i_star, j, k;
            int[] pivot = new int[n];

            for (i = 0; i < n; i++)
            {
                pivot[i] = i;
                row_max = 0;
                for (j = 0; j < n; j++)
                    row_max = Math.Max(row_max, Math.Abs(w[i, j]));
                if (row_max == 0)
                {
                    flag = 0;
                    row_max = 1;
                }
                d[i] = row_max;
            }
            if (n <= 1) return flag != 0;
            // factorization
            for (k = 0; k < n - 1; k++)
            {
                // determine pivot row the row i_star
                col_max = Math.Abs(w[k, k]) / d[k];
                i_star = k;
                for (i = k + 1; i < n; i++)
                {
                    awikod = Math.Abs(w[i, k]) / d[i];
                    if (awikod > col_max)
                    {
                        col_max = awikod;
                        i_star = i;
                    }
                }
                if (col_max == 0)
                    flag = 0;
                else
                {
                    if (i_star > k)
                    {
                        // make k the pivot row by
                        // interchanging with i_star
                        flag *= -1;
                        i = pivot[i_star];
                        pivot[i_star] = pivot[k];
                        pivot[k] = i;
                        temp = d[i_star];
                        d[i_star] = d[k];
                        d[k] = temp;
                        for (j = 0; j < n; j++)
                        {
                            temp = w[i_star, j];
                            w[i_star, j] = w[k, j];
                            w[k, j] = temp;
                        }
                    }
                    // eliminate x[k]
                    for (i = k + 1; i < n; i++)
                    {
                        w[i, k] /= w[k, k];
                        ratio = w[i, k];
                        for (j = k + 1; j < n; j++)
                            w[i, j] -= ratio * w[k, j];
                    }
                }
            }

            if (w[n - 1, n - 1] == 0) flag = 0;

            if (flag == 0)
                return false;

            Substitute(n, w, b, x, pivot);
            return true;
        }

        public void LUDecomposition(
            int n,
            double[,] a,
            double[] b,
            double[] x)
        {
            double[] y = new double[n];
            double[,] l = new double[n, n];
            double[,] u = new double[n, n];
            int n1 = n - 1;

            for (int k = 0; k < n; k++)
            {
                for (int j = k; j < n; j++)
                {
                    double sum = 0.0;

                    for (int p = 0; p <= k - 1; p++)
                        sum += l[k, p] * u[p, j];

                    u[k, j] = a[k, j] - sum;
                }

                for (int i = k + 1; i < n; i++)
                {
                    double sum = 0.0;

                    for (int p = 0; p <= k - 1; p++)
                        sum += l[i, p] * u[p, k];

                    l[i, k] = (a[i, k] - sum) / u[k, k];
                }
            }

            // forward substitution
            
            for (int k = 0; k < n; k++)
            {
                double sum = 0.0;

                for (int j = 0; j < k; j++)
                    sum += l[k, j] * y[j];
                
                y[k] = b[k] - sum;
            }

            // backward substitution
           
            for (int k = n - 1; k >= 0; k--)
            {
                double sum = 0;

                for (int j = k + 1; j < n; j++)
                    sum += u[k, j] * x[j];

                x[k] = (y[k] - sum) / u[k, k];
            }
        }

        private double matvec(int l, int u, int i, double[,] a, double[] b)
        {
            int k;
            double s = 0.0;

            for (k = l; k <= u; k++) s += a[i, k] * b[k];
            return (s);
        }

        private double tammat(int l, int u, int i, int j, double[,] a, double[,] b)
        {
            int k;
            double s = 0.0;

            for (k = l; k <= u; k++) s += a[k, i] * b[k, j];
            return (s);
        }

        double tamvec(int l, int u, int i, double[,] a, double[] b)
        {
            int k;
            double s = 0.0;

            for (k = l; k <= u; k++) s += a[k, i] * b[k];
            return (s);
        }

        void chlsol2(double[,] a, int n, double[] b)
        {
            int i;

            for (i = 1; i <= n; i++) b[i] = (b[i] - tamvec(1, i - 1, i, a, b)) / a[i, i];
            for (i = n; i >= 1; i--) b[i] = (b[i] - matvec(i + 1, n, i, a, b)) / a[i, i];
        }

        private void chldec2(double[,] a, int n, double[] aux)
        {
            int k, j;
            double r, epsnorm;

            r = 0.0;
            for (k = 1; k <= n; k++)
                if (a[k, k] > r) r = a[k, k];
            epsnorm = aux[2] * r;
            for (k = 1; k <= n; k++)
            {
                r = a[k, k] - tammat(1, k - 1, k, k, a, a);
                if (r <= epsnorm)
                {
                    aux[3] = k - 1;
                    return;
                }
                a[k, k] = r = Math.Sqrt(r);
                for (j = k + 1; j <= n; j++)
                    a[k, j] = (a[k, j] - tammat(1, k - 1, j, k, a, a)) / r;
            }
            aux[3] = n;
        }

        private void dupvecrow(int l, int u, int i, double[] a, double[,] b)
        {
            for (; l <= u; l++) a[l] = b[i, l];
        }

        private void chlinv2(double[,] a, int n)
        {
            int i, j, i1;
            double r;
            double[] u = new double[n + 1];

            for (i = n; i >= 1; i--)
            {
                r = 1.0 / a[i, i];
                i1 = i + 1;
                dupvecrow(i1, n, i, u, a);
                for (j = n; j >= i1; j--)
                    a[i, j] = -(tamvec(i1, j, j, a, u) + matvec(j + 1, n, j, a, u)) * r;
                a[i, i] = (r - matvec(i1, n, i, a, u)) * r;
            }
        }

        private void chldecinv2(double[,] a, int n, double[] aux)
        {
            chldec2(a, n, aux);
            if (aux[3] == n) chlinv2(a, n);
        }

        public void CholeskyInverse(double[,] a, int n)
        {
            double[] aux = new double[4];
            double[,] aone = new double[n + 1, n + 1];

            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                    aone[i + 1, j + 1] = a[i, j];

            chldecinv2(aone, n, aux);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = aone[i + 1, j + 1];
        }

        public bool CholeskyDecomposition(
            int n,
            double[,] a,
            double[] b,
            double[] x)
        {
            double[] aux = new double[4];
            double[,] aone = new double[n + 1, n + 1];
            double[] bone = new double[n + 1];

            for (int i = 0; i < n; i++)
            {
                bone[i + 1] = b[i];

                for (int j = i; j < n; j++)
                    aone[i + 1, j + 1] = a[i, j];
            }

            chldec2(aone, n, aux);

            if (aux[3] == n)
            {
                chlsol2(aone, n, bone);

                for (int i = 1; i <= n; i++)
                    x[i - 1] = bone[i];

                return true;
            }

            else
                return false;
        }

        public bool SimpleGaussianElimination(
            int n,
            double[,] a,
            double[] b,
            double[] x)
        {
            double[] y = new double[n];


            for (int k = 0; k < n - 1; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    if (a[k, k] == 0.0)
                        return false;

                    double mik = a[i, k] / a[k, k];

                    for (int j = k + 1; j < n; j++)
                        a[i, j] -= mik * a[k, j];
                }
            }

            // forward substitution

            for (int k = 0; k < n; k++)
            {
                double sum = 0.0;

                for (int j = 0; j < k; j++)
                    sum += a[k, j] * y[j];

                y[k] = b[k] - sum;
            }

            // backward substitution

            for (int k = n - 1; k >= 0; k--)
            {
                double sum = 0;

                for (int j = k + 1; j < n; j++)
                    sum += a[k, j] * x[j];

                x[k] = (y[k] - sum) / a[k, k];
            }

            return true;
        }
    }
}