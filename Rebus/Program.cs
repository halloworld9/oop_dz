public class Rebus
{
    private readonly string _first;
    private readonly string _second;
    private readonly string _result;

    public Rebus(string first, string second, string result)
    {
        _first = first.ToUpper();
        _second = second.ToUpper();
        _result = result.ToUpper();
    }
    

    public void Solve()
    {
        var letters = new HashSet<char>();
        var dict = new Dictionary<char, int>();
        foreach (var c in _first)
            letters.Add(c);
        foreach (var c in _second)
            letters.Add(c);
        foreach (var c in _result)
            letters.Add(c);
        
        var l = Permute(letters.Count);
        int count = 0;
        foreach (var n in l)
        {
            int i = 1;
            foreach (var c in letters)
            {
                dict[c] = n / i % 10;
                i *= 10;
            }

            CheckSum(dict);
            count++;
        }
    }

    private void PrintWords(Dictionary<char, int> letters)
    {
        int n1 = 0;
        int n2 = 0;
        int n3 = 0;
        int mult = 1;
        for (int i = _first.Length - 1; i >= 0; i--)
        {
            n1 += mult * letters[_first[i]];

            mult *= 10;
        }

        mult = 1;
        for (int i = _second.Length - 1; i >= 0; i--)
        {
            n2 += mult * letters[_second[i]];
            mult *= 10;
        }

        mult = 1;
        for (int i = _result.Length - 1; i >= 0; i--)
        {
            n3 += mult * letters[_result[i]];
            mult *= 10;
        }

        Console.WriteLine($"{_first}:{n1} + {_second}:{n2} = {_result}:{n3}");
    }

    private void CheckSum(Dictionary<char, int> letters)
    {
        int n1 = 0;
        int n2 = 0;
        int n3 = 0;
        int mult = 1;
        for (int i = _first.Length - 1; i >= 0; i--)
        {
            n1 += mult * letters[_first[i]];

            mult *= 10;
        }
        
        mult = 1;
        for (int i = _second.Length - 1; i >= 0; i--)
        {
            n2 += mult * letters[_second[i]];
            mult *= 10;
        }

        mult = 1;
        for (int i = _result.Length - 1; i >= 0; i--)
        {
            n3 += mult * letters[_result[i]];
            mult *= 10;
        }
        if (n1 + n2 == n3)
        {
            Console.WriteLine($"{_first}:{n1} + {_second}:{n2} = {_result}:{n3}");
        }
    }

    private HashSet<int> Permute(int k)
    {
        var list = new HashSet<int>();
        var ints = new int[10];
        int mod = 1;
        for (int i = 0; i < k; i++)
            mod *= 10;
        for (int i = 0; i < 10; i++)
            ints[i] = i;
        Permute(ints, 0, 9, list, mod);
        return list;
    }

    private void Permute(int[] a, int i, int n, HashSet<int> letters, int mod)
    {
        if (i == n)
        {
            int m = 1;
            int num = 0;
            for (int j = n - 1; j >= 0; j--)
            {
                num += m * a[j];
                m *= 10;
            }

            letters.Add(num % mod);
            return;
        }
        
        for (int j = i; j <= n; j++)
        {
            Swap(ref a[i], ref a[j]);
            Permute(a, i + 1, n, letters, mod);
            Swap(ref a[i], ref a[j]);
        }
    }


    private static void Swap<T>(ref T lhs, ref T rhs)
    {
        (lhs, rhs) = (rhs, lhs);
    }
}

class Program
{
    public static void Main()
    {
        var s1 = Console.ReadLine();
        var s2 = Console.ReadLine();
        var s3 = Console.ReadLine();
        var r = new Rebus(s1!, s2!, s3!);
        r.Solve();
    }
}