
using Domain.Rooms.Enums;

namespace Domain.Rooms.ValueObjects;

public class Price
{
    public decimal Value { get; set; }
    public AcceptedCurrencies Currency { get; set; }

    // Construtor sem parâmetros
    public Price() { }

    // Construtor com parâmetros
    public Price(decimal value, AcceptedCurrencies currency)
    {
        Value = value;
        Currency = currency;
    }
}


