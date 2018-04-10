using System;

public class Conta
{
    //função para gerar números aleatórios
    private static readonly Random random = new Random();
    private static readonly object syncLock = new object();
    public static int RandomNumber(int min, int max)
    {
        lock (syncLock)
        { // synchronize
            return random.Next(min, max);
        }
    }

    //gerar expressões
    //diff = 1 a 10, dificuldade dos números
    public static String gerarSoma(int diff)
    {
        return RandomNumber(1 * diff, 20 * diff).ToString() + "+" + RandomNumber(1 * diff, 20 * diff).ToString();
    }

    public static String gerarSubtracao(int diff)
    {
        int i = RandomNumber(1 * diff, 20 * diff);
        int j = RandomNumber(1 * diff, 20 * diff);
        if (i >= j)
        {
            return i.ToString() + "-" + j.ToString();
        }
        return j.ToString() + "-" + i.ToString();

    }

    public static String gerarMultiplicacao(int diff)
    {
        return RandomNumber(1 * diff, 10 * diff / 2).ToString() + "*" + RandomNumber(1 * diff, 10 * diff / 2).ToString();
    }

    public static String gerarDivisao(int diff)
    {
        int i = RandomNumber(1 * diff, 20 * diff);
        int j = i * RandomNumber(1, 10 * diff / 2);
        return j.ToString() + "/" + i.ToString();        
    }

    public static String gerarPotenciacao(int diff)
    {
        int i = RandomNumber(2 * diff, 5 * diff);
        int j;
        if (diff >= 3)
            j = 3;
        else
            j = diff + 1;

        return i.ToString() + "^" + j.ToString();
    }

    public static String gerarRadiciacao(int diff)
    {
        int i = RandomNumber(2 * diff, 5 * diff);
        int j;
        if (diff >= 3)
            j = 3;
        else
            j = diff + 1;

        i = (int)Math.Pow(i, j);

        return i.ToString() + "√" + j.ToString();

    }

    //resolvedor universal hyper mega blaster
    public static int resolver(string str)
    {
        string[] spl = str.Split("+-/*^√");

        int pr = Convert.ToInt64(spl[0]);
        int sc = Convert.ToInt64(spl[1]);

        int res;
        if (str.Contains("+"))
        {
            res = pr + sc;
        }
        else if(str.Contains("-")){
            res = pr - sc;
        }
        else if (str.Contains("*"))
        {
            res = pr * sc;
        }
        else if (str.Contains("/"))
        {
            res = pr / sc;
        }
        else if (str.Contains("^"))
        {
            res = (int)Math.Pow(pr, sc);
        }
        else if (str.Contains("√"))
        {
            res = (int)Math.Pow(pr, 1.0 / sc);
        }
        else
        {
            return;
        }

        return res;
    }
}
