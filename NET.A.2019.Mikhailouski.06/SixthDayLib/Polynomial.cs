using System;
using System.Text;

namespace SixthDayLib
{
    /// <summary>
    /// Class represents polynomal
    /// </summary>
    public sealed class Polynomial
    {
        
        private const double eps = double.Epsilon;

        public Polynomial(double[] index)
        {
            if (index == null) throw new ArgumentNullException();
            if (index.Length < 1) throw new ArgumentException();
            Index = index;
        }

        /// <summary>
        /// Order of the polynomal
        /// </summary>
        public int Order
        {
            get
            {
                for (int i = Index.Length - 1; i >= 0; i--)
                    if (Math.Abs(Index[i]) > eps)
                        return 1;
                return 0;
            }
        }

        public double[] Index { get; private set; }

        public double this[int factor]
        {
            get
            {
                if (factor < 0) throw new ArgumentOutOfRangeException();
                if (factor >= Index.Length) return 0;
                return Index[factor];
            }
        }

        public static Polynomial operator +(Polynomial first, Polynomial second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            int maxLength = 1 + (first.Order > second.Order ? first.Order : second.Order);
            int minLength = 1 + (first.Order < second.Order ? first.Order : second.Order);

            double[] resultFactors = new double[maxLength];
            for (int i = 0; i < minLength; i++)
                resultFactors[i] = first[i] + second[i];

            if (first.Order > second.Order)
                Array.Copy(first.Index, minLength, resultFactors, minLength, maxLength - minLength);
            else
                Array.Copy(second.Index, minLength, resultFactors, minLength, maxLength - minLength);

            return new Polynomial(resultFactors);
        }

        public static Polynomial operator -(Polynomial first, Polynomial second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            int maxLength = 1 + (first.Order > second.Order ? first.Order : second.Order);
            int minLength = 1 + (first.Order < second.Order ? first.Order : second.Order);

            double[] resultFactors = new double[maxLength];
            for (int i = 0; i < minLength; i++)
                resultFactors[i] = first[i] - second[i];

            if (first.Order > second.Order)
                Array.Copy(first.Index, minLength, resultFactors, minLength, maxLength - minLength);
            else
                for (int i = minLength; i < maxLength; i++)
                    resultFactors[i] = -second[i];

            return new Polynomial(resultFactors);
        }

        public static Polynomial operator *(Polynomial polinomial, int multiplier)
        {
            if (polinomial == null) throw new ArgumentNullException();

            double[] resultFactors = new double[polinomial.Order + 1];
            for (int i = 0; i < polinomial.Order + 1; i++)
                resultFactors[i] = polinomial[i] * multiplier;
            return new Polynomial(resultFactors);
        }

        public static Polynomial operator *(int multiplier, Polynomial polinomial)
        {
            if (polinomial == null) throw new ArgumentNullException();

            return polinomial * multiplier;
        }

        public static Polynomial operator *(Polynomial polinomial, double multiplier)
        {
            if (polinomial == null) throw new ArgumentNullException();

            double[] resultFactors = new double[polinomial.Order + 1];
            for (int i = 0; i < polinomial.Order + 1; i++)
                resultFactors[i] = polinomial[i] * multiplier;
            return new Polynomial(resultFactors);
        }

        public static Polynomial operator *(double multiplier, Polynomial polinomial)
        {
            if (polinomial == null) throw new ArgumentNullException();

            return polinomial * multiplier;
        }

        public static Polynomial operator *(Polynomial first, Polynomial second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            int resultOrder = first.Order + second.Order + 1;
            double[] resultForces = new double[resultOrder];

            for (int i = 0; i <= first.Order; i++)
            {
                if (Math.Abs(first[i]) > eps)
                {
                    for (int j = 0; j <= second.Order; j++)
                        resultForces[i + j] += first[i] * second[j];
                }
            }
            return new Polynomial(resultForces);
        }

        public static Polynomial operator /(Polynomial polinomial, int multiplier)
        {
            if (polinomial == null) throw new ArgumentNullException();

            double[] resultFactors = new double[polinomial.Order + 1];
            for (int i = 0; i < polinomial.Order + 1; i++)
                resultFactors[i] = polinomial[i] / multiplier;
            return new Polynomial(resultFactors);
        }

        public static Polynomial operator /(int multiplier, Polynomial polinomial)
        {
            if (polinomial == null) throw new ArgumentNullException();

            return polinomial / multiplier;
        }

        public static bool operator ==(Polynomial first, Polynomial second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            if (first.Index.Length != second.Index.Length)
            {
                return false;
            }
            for (int i = 0; i < first.Index.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(Polynomial first, Polynomial second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            return !(first == second);
        }
        /// <summary>
        /// Returns string representation of the given polynomal
        /// </summary>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Index.Length; i++)
            {
                if (i == 0 && Math.Abs(Index[i]) > eps)
                {
                    str.AppendFormat($"{Index[i]}");
                    continue;
                }

                if (Math.Abs(Index[i]) > eps)
                    if (Index[i] > 0 && str.Length > 0)
                        str.AppendFormat($" + {Index[i]}*x^{i}");
                    else
                        str.AppendFormat($" {Index[i]}*x^{i}");
            }
            return str.ToString().Trim();
        }
        /// <summary>
        /// Performs deep comparison of the two given polynomals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null) throw new ArgumentNullException();

            Polynomial p = obj as Polynomial;

            if (p?.Order != this.Order)
                return false;

            for (int i = 0; i <= this.Order; i++)
            {
                if (Math.Abs(this[i] - p[i]) > eps)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Returns hash code of the given polynomal
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 1;
            foreach (var factor in Index)
            {
                hash *= (int)factor;
                hash += Order;
            }
            return hash;
        }
    }
}
