using System;


public class C
{
    public void DoSomething()
    {
        Console.WriteLine("C ");
    }
}

public class B
{
    private readonly C _c;

    public B(C c)
    {
        _c = c;
    }

    public void ExecuteB()
    {
        Console.WriteLine("B ");
        _c.DoSomething();
    }
}

public class A
{
    private readonly B _b;

    public A(B b)
    {
        _b = b;
    }

    public void ExecuteA()
    {
        Console.WriteLine("A");
        _b.ExecuteB();
    }
}

class Progrm
{
    static void Main(string[] args)
    {
        C cObj = new C();

        B bObj = new B(cObj);

        A aObj = new A(bObj);

        aObj.ExecuteA();
    }
}
