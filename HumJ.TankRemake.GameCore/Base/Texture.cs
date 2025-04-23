namespace HumJ.TankRemake.GameCore.Base
{
    public record Texture(string ImageName, int StartX, int StartY, int Width, int Height)
    {
        public static Texture[][] _B_16 { get; } = CreateItem("_B_16.png", 16, 16, 8, 1);
        public static Texture[][] _B_32A { get; } = CreateItem("_B_32A.png", 32, 32, 8, 1);
        public static Texture[][] _B_32B { get; } = CreateItem("_B_32B.png", 32, 32, 8, 1);
        public static Texture[][] _B_48 { get; } = CreateItem("_B_48.png", 48, 48, 8, 1);
        public static Texture[][] _B_Explose { get; } = CreateItem("_B_Explose.png", 48, 48, 1, 8);
        public static Texture[][] _B_Fire { get; } = CreateItem("_B_Fire.png", 16, 16, 8, 3);
        public static Texture[][] B_Armor { get; } = CreateItem("B_Armor.png", 16, 16, 4, 1);
        public static Texture[][] B_Explose { get; } = CreateItem("B_Explose.png", 16, 16, 4, 1);
        public static Texture[][] B_Fire { get; } = CreateItem("B_Fire.png", 16, 16, 8, 1);
        public static Texture[][] B_Mine { get; } = CreateItem("B_Mine.png", 16, 16, 4, 6);
        public static Texture[][] B_Normal { get; } = CreateItem("B_Normal.png", 16, 16, 8, 1);
        public static Texture[][] B_Repid { get; } = CreateItem("B_Repid.png", 16, 16, 8, 1);
        public static Texture[][] Back { get; } = CreateItem("Back.png", 16, 16, 16, 4);
        public static Texture[][] Cookie { get; } = CreateItem("Cookie.png", 32, 32, 25, 1);
        public static Texture[][] Flag { get; } = CreateItem("Flag.png", 32, 32, 8, 3);
        public static Texture[][] TankOut { get; } = CreateItem("Tank Out.png", 32, 32, 6, 1);
        public static Texture[][] Tank { get; } = CreateItem("Tank.png", 32, 32, 28, 8);
        public static Texture[][] Tank_L { get; } = CreateItem("Tank_L.png", 32, 32, 4, 4);
        public static Texture[][] Tank_T { get; } = CreateItem("Tank_T.png", 48, 48, 8, 3);
        public static Texture[][] Text { get; } = CreateItem("Text.png", 12, 24, 16, 9);
        public static Texture[][] Text2 { get; } = CreateItem("Text2.png", 8, 16, 32, 4);
        public static Texture[][] Text3 { get; } = CreateItem("Text3.png", 18, 36, 32, 4);

        private static Texture[][] CreateItem(string name, int width, int height, int columns, int rows) => Enumerable.Range(0, rows).Select(y => Enumerable.Range(0, columns).Select(x => new Texture(name, width * x, height * y, width, height)).ToArray()).ToArray();
    }
}