public class SquareMatrix : Matrix
{
    public SquareMatrix(int n) : base(n, n)
    {
    }

    public SquareMatrix(int n, int min, int max) : base(n, n, min, max)
    {
    }

    public SquareMatrix(int n, TextReader r) : base(n, n, r)
    {
    }

    public SquareMatrix Triangular()
    {
        var m = new SquareMatrix(rows);
        for (int i = 0; i < rows; i++)
        {
            for (var j = 0; j < rows; j++)
                m[i, j] = this[i, j];
        }

        m.SortRows();
        int xPivot = 0;


        for (int yPivot = 0; yPivot < rows - 1; yPivot++)
        {
            for (int i = xPivot; i < m.rows; i++)
            {
                if (m[yPivot, i] == 0 && i == m.cols - 1)
                    return m;
                if (m[yPivot, i] == 0) continue;
                xPivot = i;
                break;
            }

            var pivot = m[yPivot, xPivot];
            for (int y = yPivot + 1; y < m.rows; y++)
            {
                var mult = m[y, xPivot] / pivot;
                for (int x = xPivot; x < m.rows; x++)
                {
                    m[y, x] -= m[yPivot, x] * mult;
                }
            }

            m.SortRows();
        }

        return m;
    }

    public void SortRows()
    {
        int y = 0;
        for (int x = 0; x < rows; x++)
        {
            if (SortRows(y, x))
                y++;
        }
    }

    private bool SortRows(int yStart, int x)
    {
        var zeros = new LinkedList<int>();
        for (int i = yStart; i < rows; i++)
        {
            if (this[i, x] == 0)
            {
                zeros.AddLast(i);
                continue;
            }

            if (zeros.Count > 0)
            {
                var zero = zeros.First();
                zeros.RemoveFirst();
                SwapRows(i, zero);
                continue;
            }
        }

        return zeros.Count != rows;
    }

    public SquareMatrix Reverse()
    {
        var m = new SquareMatrix(rows);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                m[j, i] = AlgebraicAddition(i, j);
            }
        }

        m /= Det();

        return m;
    }

    private double AlgebraicAddition(int i, int j)
    {
        var m = new SquareMatrix(rows - 1);
        for (int y = 0; y < rows; y++)
        {
            if (y == i) continue;
            var y0 = y > i ? y - 1 : y;
            for (int x = 0; x < rows; x++)
            {
                if (x == j) continue;
                var x0 = x > j ? x - 1 : x;
                m[y0, x0] = this[y, x];
            }
        }

        var det = m.Det();

        return det * ((i + j) % 2 == 0 ? 1 : -1);
    }

    public double Det()
    {
        if (rows == 1) return this[0, 0];
        if (rows == 2)
        {
            return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
        }
        var res = 0.0;

        for (int i = 0; i < rows; i++)
        {
            var mult = this[0, i];
            if (i % 2 == 1) mult *= -1;
            var n1 = rows - 1;
            var matrix = new SquareMatrix(n1);
            for (int j = 0; j < n1; j++)
            {
                int y = j+1;
                for (int k = 0; k < n1; k++)
                {
                    int x = k;
                    if (x >= i) x+=1;
                    matrix[j, k] = this[y, x];
                }
            }

            res += mult * matrix.Det();
        }
        return res;
    }


    public SquareMatrix Transpose()
    {
        var s = new SquareMatrix(rows);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                s[i, j] = this[j, i];
            }
        }

        return s;
    }
    
    

    public static SquareMatrix operator +(SquareMatrix a, SquareMatrix b)
    {
        if (a.cols != b.cols || a.rows != b.rows)
            throw new ArgumentException("Matrices are not the same size");
        var m = new SquareMatrix(a.rows);
        for (var i = 0; i < a.rows; i++)
        {
            for (var j = 0; j < a.cols; j++)
                m[i, j] = a[i, j] + b[i, j];
        }

        return m;
    }

    public static SquareMatrix operator *(SquareMatrix a, double b)
    {
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                a[i, j] = Math.Round(a[i, j] * b, 6);
            }
        }

        return a;
    }

    public static SquareMatrix operator *(double b, SquareMatrix a)
    {
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                a[i, j] = Math.Round(a[i, j] * b, 6);
            }
        }

        return a;
    }

    public static SquareMatrix operator -(SquareMatrix a, SquareMatrix b)
    {
        if (a.cols != b.cols)
            throw new ArgumentException("Matrices are not the same size");
        var m = new SquareMatrix(a.rows);
        for (var i = 0; i < a.rows; i++)
        {
            for (var j = 0; j < a.cols; j++)
                m[i, j] = a[i, j] - b[i, j];
        }

        return m;
    }

    public static SquareMatrix operator -(SquareMatrix a)
    {
        var m = new SquareMatrix(a.rows);
        for (var i = 0; i < a.rows; i++)
        {
            for (var j = 0; j < a.cols; j++)
                m[i, j] = -a[i, j];
        }

        return m;
    }

    public static SquareMatrix operator *(SquareMatrix a, SquareMatrix b)
    {
        if (a.cols != b.rows)
            throw new ArgumentException("Matrices are wrong size");
        var m = new SquareMatrix(a.rows);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < b.cols; j++)
            {
                for (int k = 0; k < a.rows; k++)
                {
                    m[i, j] += a[i, k] * b[k, j];
                }
            }
        }

        return m;
    }

    public static SquareMatrix operator /(SquareMatrix a, double b)
    {
        var m = new SquareMatrix(a.rows);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                m[i, j] = Math.Round(a[i, j] / b, 6);
            }
        }

        return m;
    }


    public static void Main(string[] args)
    {
        var m = new SquareMatrix(4, Console.In);
        Console.WriteLine(m);
        Console.WriteLine(m.Det());
    }
}