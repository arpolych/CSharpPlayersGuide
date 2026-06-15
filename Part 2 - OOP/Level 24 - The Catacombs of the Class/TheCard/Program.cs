Color[] cardColors = new[] { Color.Red, Color.Green, Color.Blue, Color.Yellow };
Rank[] cardRanks = new[] { Rank.One, Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight,
                           Rank.Nine, Rank.Ten, Rank.DollarSign, Rank.Percent, Rank.Caret, Rank.Ampersand };

CreateAndDisplayAllCards(cardColors, cardRanks);

static void CreateAndDisplayAllCards(Color[] cardColors, Rank[] cardRanks)
{
    foreach (Color color in cardColors)
    {
        foreach (Rank rank in cardRanks)
        {
            Card newCard = new(color, rank);
            Console.WriteLine($"The {color} {rank}");
        }
    }
}

public class Card
{
    public Color Color { get; }
    public Rank Rank { get; }

    public Card(Color color, Rank rank)
    {
        Color = color;
        Rank = rank;
    }

    public static bool IsSymbolRankCard(Card aCard)
        => (aCard.Rank == Rank.DollarSign
         || aCard.Rank == Rank.Percent
         || aCard.Rank == Rank.Caret
         || aCard.Rank == Rank.Ampersand);

    public static bool IsNumberRankCard(Card aCard)
        => !IsSymbolRankCard(aCard);
}

public enum Color { Red, Green, Blue, Yellow }

public enum Rank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, DollarSign, Percent, Caret, Ampersand }

/*
 * Answer to question:
 * 
 * I believe we used enumeration for the color here, because we didn't
 * require the color carry any significant action, we only wanted the name,
 * and thus enumeration is perfect for it.
 */ 