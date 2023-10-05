using System;

public interface IEdible
{
    string Name { get; }
    int Weight { get; }
    int SugarContent { get; }
}

public abstract class Sweet : IEdible
{
    public string Name { get; protected set; }
    public int Weight { get; protected set; }
    public int SugarContent { get; protected set; }

    public Sweet(string name, int weight, int sugarContent)
    {
        Name = name;
        Weight = weight;
        SugarContent = sugarContent;
    }

    public override string ToString()
    {
        return $"{Name} (Вага: {Weight}г, Вмiст цукру: {SugarContent}г)";
    }
}

public class Chocolate : Sweet
{
    public string Type { get; private set; }

    public Chocolate(string name, int weight, int sugarContent, string type)
        : base(name, weight, sugarContent)
    {
        Type = type;
    }
}

public class Candy : Sweet
{
    public Candy(string name, int weight, int sugarContent)
        : base(name, weight, sugarContent)
    {
    }
}

public class NewYearsGift
{
    private IEdible[] items;

    public NewYearsGift(params IEdible[] items)
    {
        this.items = items;
    }

    public int GetTotalWeight()
    {
        return items.Sum(item => item.Weight);
    }

    public void SortBySugarContent()
    {
        Array.Sort(items, (x, y) => x.SugarContent.CompareTo(y.SugarContent));
    }

    public IEdible FindSweetByCriteria(Func<IEdible, bool> criteria)
    {
        return items.FirstOrDefault(criteria);
    }

    public void DisplayContents()
    {
        Console.WriteLine("Вмiст цукру:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var candy1 = new Candy("Ромашка", 20, 15);
        var candy2 = new Candy("Льодяник", 10, 10);
        var chocolate1 = new Chocolate("Молочний шоколад", 50, 30, "Молочний");
        var chocolate2 = new Chocolate("Чорний шоколад", 40, 25, "Чорний");

        var newYearsGift = new NewYearsGift(candy1, candy2, chocolate1, chocolate2);

        newYearsGift.DisplayContents();

        var sweet = newYearsGift.FindSweetByCriteria(item => item.SugarContent < 20);
        if (sweet != null)
        {
            Console.WriteLine($"Знайдено цукерку за критерiями: {sweet}");
        }
        else
        {
            Console.WriteLine("Не знайдено цукерку, що вiдповiдає критерiям.");
        }

        newYearsGift.SortBySugarContent();
        Console.WriteLine("\nСкiльки цукру у подарунку (Вiдсортовано за вмiстом цукру):");
        newYearsGift.DisplayContents();

        int totalWeight = newYearsGift.GetTotalWeight();
        Console.WriteLine($"\nМаса подарунка: {totalWeight}г");
    }
}
