class Complex(double a, double b)
{
    double _a = a;
    double _b = b;

    public double A
    {
        set => _a = value;
        get => a;
    }

    public double B
    {
        set => _b = value;
        get => b;
    }

    public Complex() : this(0, 0)
    {
    }

    public static Complex operator ++(Complex c)
    {
        return c + c / c.Abs();
    }

    public static Complex operator --(Complex c)
    {
        return c - c / c.Abs();
    }

    public static Complex operator +(Complex c) => c;
    public static Complex operator +(Complex c1, Complex c2) => new(c1._a + c2._a, c1._b + c2._b);
    public static Complex operator +(Complex c1, double c2) => new(c1._a + c2, c1._b);
    public static Complex operator -(Complex c) => new(-c._a, -c._b);
    public static Complex operator -(Complex c1, Complex c2) => new(c1._a - c2._a, c1._b - c2._b);
    public static Complex operator -(Complex c1, double c2) => new(c1._a - c2, c1._b);

    public static Complex operator *(Complex c1, Complex c2)
    {
        return new Complex(c1._a * c2._a - c1._b * c2._b, c1._a * c2._b + c1._b * c2._a);
    }

    public static Complex operator *(Complex c1, double c2) => new(c1._a * c2, c1._b * c2);

    public static Complex operator /(Complex c1, Complex c2)
    {
        var a = (c1._a * c2._a + c1._b * c2._b) / (c2._a * c2._a + c2._b * c2._b);
        var b = (c1._b * c2._a - c1._a * c2._b) / (c2._a * c2._a + c2._b * c2._b);
        return new Complex(a, b);
    }

    public double Abs()
    {
        return Math.Sqrt(_a * _a + _b * _b);
    }

    public static Complex operator /(Complex c1, double c2) => new(c1._a / c2, c1._b / c2);


    public override string ToString()
    {
        return $"{_a}+{_b}i";
    }
}


class ComplexPolar
{
    double _r;

    double _phi;


    protected ComplexPolar(double r, double phi)
    {
        this._r = r;
        this._phi = phi;
    }

    public static ComplexPolar Numerical(double a, double b)
    {
        return new ComplexPolar(Math.Atan(b / a), Math.Sqrt(a * a + b * b));
    }

    public static ComplexPolar Polar(double r, double phi)
    {
        return new ComplexPolar(r, phi);
    }


    public ComplexPolar Pow(double power)
    {
        var r = Math.Pow(this._r, power);
        var phi = this._phi * power;
        return new ComplexPolar(r, phi);
    }
}


class Main1
{
    public static void Main(string[] args)
    {
        
    }
}