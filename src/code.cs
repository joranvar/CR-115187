class Value
{
    public int idx = -1;
    public int num = -1;
    public Value(int idx, int num)
    {
        this.idx = idx;
        this.num = num;
    }
}

class Program
{
    const int N = 8;
    static int S = 2;
    static int T = 3;
    static int[] numbers = new int[N] { 1, 6, 8, 7, 6, 2, 1, 8 };
    static List<Value> values = new List<Value>();
    static int maxPoint = -1;
    static List<Value> result;

    static void Main(string[] args)
    {
        CountInitValues();
        Solve(values, new List<Value>(), 0, T);
    }

    static void Solve(List<Value> remaining, List<Value> numSoFar, int pointSoFar, int StepsRemain)
    {
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
                Solve(newRemaining, newNum, ujPont, ujLepes);
                newNum.RemoveAt(newNum.Count - 1);
            }
        }
    }

    static void CountInitValues()
    {
        for (int i = 0; i < N - T + 1; i++)
        {
            int newValue = 0;
            for (int j = 0; j < T; j++)
            {
                newValue += numbers[i + j];
            }
            Value ee = new Value(i, newValue);
            values.Add(ee);
        }
    }
}
