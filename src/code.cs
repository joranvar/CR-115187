using System.Collections.Generic;
using System.Linq;

class Value
{
    public int idx;
    public int num;
    public Value(int idx, int num)
    {
        this.idx = idx;
        this.num = num;
    }
    public override string ToString() { return $"({idx}, {num})"; }
}

class Solver
{
    int N;
    int S;
    int T;
    int[] numbers;

    List<Value> values = new List<Value>();
    int maxPoint = -1;
    List<Value> result;

    public Solver(int N, int S, int T, int[] numbers)
    {
        this.N = N;
        this.S = S;
        this.T = T;
        this.numbers = numbers;
    }

    public List<Value> Solve()
    {
        CountInitValues();
        InnerSolve(values, new List<Value>(), 0, T);
        return result;
    }

    void InnerSolve(List<Value> remaining, List<Value> numSoFar, int pointSoFar, int StepsRemain)
    {
        System.Console.Out.WriteLine ("remaining   : {0}", string.Join(", ", remaining.Select(i => i.ToString())));
        System.Console.Out.WriteLine ("numSoFar    : {0}", string.Join(", ", numSoFar.Select(i => i.ToString())));
        System.Console.Out.WriteLine ("pointSoFar  : {0}", pointSoFar);
        System.Console.Out.WriteLine ("StepsRemain : {0}", StepsRemain);
        if (remaining.Count == 0 || StepsRemain == 0)
        {
            if (pointSoFar > maxPoint)
            {
                maxPoint = pointSoFar;
                result = new List<Value>(numSoFar);
            }
        }
        else
        {
            List<Value> newNum = new List<Value>(numSoFar);
            for (int i = 0; i < remaining.Count; i++)
            {
                List<Value> newRemaining = new List<Value>();
                newNum.Add(remaining[i]);
                newRemaining.AddRange(remaining.Take(i - T));
                newRemaining.AddRange(remaining.Skip(i + T));
                int ujPont = pointSoFar + remaining[i].num;
                int ujLepes = StepsRemain - 1;
                InnerSolve(newRemaining, newNum, ujPont, ujLepes);
                newNum.RemoveAt(newNum.Count - 1);
            }
        }
    }

    void CountInitValues()
    {
        for (int i = 0; i < N - T + 1; i++)
        {
            Value ee = new Value(i, CountValuesFrom(i));
            values.Add(ee);
        }
    }

    int CountValuesFrom(int startIndex)
    {
        int newValue = 0;
        for (int j = 0; j < T; j++)
        {
            newValue += numbers[startIndex + j];
        }
        return newValue;
    }
}

class Program
{
    const int N = 8;
    static int S = 2;
    static int T = 3;
    static int[] numbers = new int[N] { 1, 6, 8, 7, 6, 2, 1, 8 };

    static void Main(string[] args)
    {
        var result = new Solver(N, S, T, numbers).Solve();
        System.Console.Out.WriteLine ("The result is: {0}", string.Join(", ", result.Select(i => i.ToString())));
    }
}
