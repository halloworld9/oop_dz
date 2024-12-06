using System.Text;

public class Matrix
{
    private readonly double[][] matrix;
    public readonly int rows;
    public readonly int cols;

    public Matrix(int rows, int columns)
    {
        this.rows = rows;
        this.cols = columns;
        matrix = new double[rows][];
        for (var row = 0; row < rows; row++)
            matrix[row] = new double[columns];
    }

    public Matrix(int rows, int columns, int min, int max) : this(rows, columns)
    {                                               
        var r = Random.Shared;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
                matrix[i][j] = r.Next(min, max);
        }
    }

    public Matrix(int rows, int columns, TextReader r) : this(rows, columns)
    {
        for (var row = 0; row < rows; row++)
        {
            var s = r.ReadLine();
            if (string.IsNullOrEmpty(s))
                continue;
            var str = s.Split(" ");
            if (str.Length > rows)
            {
                Console.WriteLine("Слишком много чисел, вводите дальше");
                continue;
            }

            for (int i = 0; i < str.Length; i++)
            {
                this[row, i] = Convert.ToDouble(str[i]);
            }
        }
    }


    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.cols != b.cols || a.rows != b.rows)
            throw new ArgumentException("Matrices are not the same size");
        var m = new Matrix(a.rows, a.cols);
        for (var i = 0; i < a.rows; i++)
        {
            for (var j = 0; j < a.cols; j++)
                m[i, j] = a[i, j] + b[i, j];
        }

        return m;
    }

    public static Matrix operator *(Matrix a, double b)
    {
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                a[i, j] *= b;
            }
        }

        return a;
    }

    public static Matrix operator *(double b, Matrix a)
    {
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                a[i, j] *= b;
            }
        }

        return a;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.cols != b.cols || a.rows != b.rows)
            throw new ArgumentException("Matrices are not the same size");
        var m = new Matrix(a.rows, a.cols);
        for (var i = 0; i < a.rows; i++)
        {
            for (var j = 0; j < a.cols; j++)
                m[i, j] = a[i, j] - b[i, j];
        }

        return m;
    }

    public static Matrix operator -(Matrix a)
    {
        var m = new Matrix(a.rows, a.cols);
        for (var i = 0; i < a.rows; i++)
        {
            for (var j = 0; j < a.cols; j++)
                m[i, j] = -a[i, j];
        }

        return m;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.cols != b.rows)
            throw new ArgumentException("Matrices are wrong size");
        var m = new Matrix(a.rows, b.cols);
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

    public static Matrix operator /(Matrix a, double b)
    {
        var m = new Matrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                m[i, j] = a[i, j] / b;
            }
        }

        return m;
    }

    public double this[int y, int x]
    {
        get => matrix[y][x];
        set => matrix[y][x] = value;
    }

    public static Matrix E(int size)
    {
        var m = new Matrix(size, size);
        for (var i = 0; i < size; i++)
            m[i, i] = 1;
        return m;
    }

    protected void SwapRows(int y1, int y2)
    {
        (matrix[y1], matrix[y2]) = (matrix[y2], matrix[y1]);
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        for (var i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                s.Append(matrix[i][j]).Append('\t');
            s.Append('\n');
        }
        return s.ToString();
    }
}

