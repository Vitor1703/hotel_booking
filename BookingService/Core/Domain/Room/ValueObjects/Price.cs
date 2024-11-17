using Domain.Rooms.Enums;

namespace Domain.Rooms.ValueObjects;

public class Price
{
    public decimal Value { get; set; }
    public AcceptedCurrencies Currency { get; set; }

    // Construtor sem parâmetros (padrão)
    public Price() 
    {
        Currency = AcceptedCurrencies.DOLLAR; // Define uma moeda padrão
    }

    // Construtor com parâmetros
    public Price(decimal value, AcceptedCurrencies currency)
    {
        Value = value;
        Currency = currency;
    }

    // Método para alterar o nome da propriedade para 'Amount', se necessário
    public decimal Amount => Value;
}
