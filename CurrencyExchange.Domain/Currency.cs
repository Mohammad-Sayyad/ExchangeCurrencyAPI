namespace CurrencyExchange.Domain;

public class Currency
{
    public string Code { get; set; }
    public string Name { get; set; }

    public Currency(string code , string name)
    {
        Code = code;
        Name = name;
    }
}
