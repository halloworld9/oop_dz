class Program
{
    public static void Main(string[] args)
    {
        var game = new Game();
        game.PlayGame();
    }
}

class Game
{
    private Dictionary<char, int> num = new();

    public Game()
    {
        var str = InitFromFile();
        Console.WriteLine(str);
        int i = 0;
        foreach (var c in str)
        {
            num.Add(c, i);
            i++;
        }

    }

    private KeyValuePair<int, int> CheckStr(string str)
    {
        var cows = 0;
        var bulls = 0;
        if (str.Length != 5)
            return new KeyValuePair<int, int>();
        for (int i = 0; i < 5; i++)
        {
            char v = str[i];
            if (!num.ContainsKey(v))
                continue;

            if (num[v] != i)
                cows++;
            else
                bulls++;
        }

        return new KeyValuePair<int, int>(bulls, cows);
    }

    private bool CheckInput(string? str)
    {
        return str != null && str.Length == 5;
    }

    private string Input()
    {
        Console.WriteLine("Введите слово из 5 букв");
        var s = Console.ReadLine();
        while (true)
        {
            if (CheckInput(s))
                return s!;
            Console.WriteLine("Введите слово из 5 букв");
            if (s == null)
                continue;


            s = Console.ReadLine();
        }

        return s;
    }

    private string InitFromFile()
    {
        using var file = File.Open("/home/prish/Документы/SHARP/SHARP/dict.txt", FileMode.Open);

        {
            var maxRand = (file.Length + 1) / 11;
            var r = Random.Shared;
            var n = r.Next((int)maxRand);
            string? s = "";
            using var reader = new StreamReader(file);

            {
                for (var i = 0; i < n; i++)
                    s = reader.ReadLine();
            }
            return s ?? "";
        }
    }

    public void PlayGame()
    {
        string s = Input();
        var pair = CheckStr(s);
        Console.WriteLine("Быков " + pair.Key + " Коров " + pair.Value);
        while (pair.Key != 5)
        {
            s = Input();

            pair = CheckStr(s);
            Console.WriteLine("Быков " + pair.Key + " Коров " + pair.Value);
        }
    }
}