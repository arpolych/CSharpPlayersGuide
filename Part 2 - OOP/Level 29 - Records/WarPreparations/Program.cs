Sword basic = new(Material.Iron, Gemstone.None, 10.5d);

Console.WriteLine(basic);

Sword basicSapphire = basic with { Gemstone = Gemstone.Sapphire };
Sword advanced = basic with { Material = Material.Binarium, Gemstone = Gemstone.Sapphire };

Console.WriteLine(basicSapphire);
Console.WriteLine(advanced);

public record Sword(Material Material, Gemstone Gemstone, double Crossguard);

public enum Gemstone { Emerald, Amber, Sapphire, Diamond, Bitstone, None }

public enum Material { Wood, Bronze, Iron, Steel, Binarium }