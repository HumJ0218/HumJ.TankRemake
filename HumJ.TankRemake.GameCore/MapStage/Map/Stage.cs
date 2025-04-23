using HumJ.TankRemake.GameCore.MapStage.Tile;
using System.Drawing;
using System.Text;

namespace HumJ.TankRemake.GameCore.MapStage.Map
{
    public class Stage
    {
        public const int XTiles = 40;
        public const int YTiles = 30;

        public required string Caption { get; set; }
        public required string BackgroundImage { get; set; }
        public required Color BackgroundColor { get; set; }
        public required bool BackgroundImageHorizontalRepeat { get; set; }
        public required bool BackgroundImageVerticalRepeat { get; set; }
        public required Difficulty SkippedDifficulty { get; set; }
        public required SpecialOption SpecialOption { get; set; }
        public required int[] EnemyCount { get; set; }
        public required int MaxCount { get; set; }
        public required string StageInitializerImage { get; set; }

        public required List<TileBase> BackgroundTile { get; set; }
        public required List<TileBase> ForeGroundTile { get; set; }

        public static Stage Parse(string text)
        {
            var lines = text.Split('\n');
            var kv = lines.Select(line => line.Split('=')).Where(m => m.Length > 1).ToDictionary(p => p[0].Trim(), p => p[1].Trim());

            var bt = new List<TileBase>();
            var ft = new List<TileBase>();
            for (var y = 0; y < YTiles; y++)
            {
                var bg = kv[$"BG{y:D2}"].Split(' ');
                var fg = kv[$"FG{y:D2}"].Split(' ');

                for (var x = 0; x < XTiles; x++)
                {
                    var b = Convert.ToInt32(bg[x], 16);
                    if (b != 0)
                    {
                        bt.Add(Parse(b, x, y)!);
                    }

                    var f = Convert.ToInt32(fg[x], 16);
                    if (f != 0)
                    {
                        ft.Add(Parse(f, x, y)!);
                    }
                }
            }

            bt.RemoveAll(m => m is null);
            ft.RemoveAll(m => m is null);

            return new Stage
            {
                Caption = kv["Caption"],
                BackgroundImage = kv["BG_Bitmap"],
                BackgroundColor = Color.FromArgb(Convert.ToInt32($"{kv["BG_Color"][5]}{kv["BG_Color"][6]}{kv["BG_Color"][3]}{kv["BG_Color"][4]}{kv["BG_Color"][1]}{kv["BG_Color"][2]}", 16)),
                BackgroundImageHorizontalRepeat = int.Parse(kv["BG_H_Tile"]) != 0,
                BackgroundImageVerticalRepeat = int.Parse(kv["BG_V_Tile"]) != 0,
                SkippedDifficulty = (Difficulty)int.Parse(kv["SkippedDif"]),
                SpecialOption = (SpecialOption)int.Parse(kv["SpecialOpt"]),
                EnemyCount = kv["EnemyCount"].Split(',').Select(m => int.Parse(m)).ToArray(),
                MaxCount = int.Parse(kv["MaxCount"]),
                StageInitializerImage = kv["SI_Bitmap"],
                BackgroundTile = bt,
                ForeGroundTile = ft,
            };
        }

        private static Dictionary<Difficulty, Stage[]> Parse(byte[] bytes)
        {
            var sliceSize = 6000;
            var maps = new List<Stage>();

            while (maps.Count * sliceSize < bytes.Length)
            {
                var slice = bytes.AsSpan(maps.Count * sliceSize, sliceSize);
                maps.Add(Parse(slice));
            }

            var allStages = System.Enum.GetValues<Difficulty>().ToDictionary(d => d, d => maps.Where(m => (m.SkippedDifficulty & d) != d).ToArray());
            return allStages;
        }

        private static Stage Parse(Span<byte> bytes)
        {
            var Caption = Encoding.ASCII.GetString(bytes.Slice(1, 63)).TrimEnd('\0');
            var BG_Bitmap = Encoding.ASCII.GetString(bytes.Slice(65, 63)).TrimEnd('\0');
            var BG_Color = Color.FromArgb(bytes[128], bytes[129], bytes[130]);
            var BG_H_Tile = bytes[132] != 0;
            var BG_V_Tile = bytes[133] != 0;
            var SkippedDif = bytes[210] + bytes[211] * 256;
            var SpecialOpt = bytes[212] + bytes[213] * 256;
            var EnemyCount = ParseEnemyCount(bytes.Slice(144, 48));
            var MaxCount = bytes[208] + bytes[209] * 256;
            var SI_Bitmap = Encoding.ASCII.GetString(bytes.Slice(225, 63)).TrimEnd('\0');
            var BackgroundTile = ParseTile(bytes.Slice(1200, 2400));
            var ForeGroundTile = ParseTile(bytes.Slice(3600, 2400));

            var map = new Stage
            {
                Caption = Caption,
                BackgroundImage = BG_Bitmap,
                BackgroundColor = BG_Color,
                BackgroundImageHorizontalRepeat = BG_H_Tile,
                BackgroundImageVerticalRepeat = BG_V_Tile,
                SkippedDifficulty = (Difficulty)SkippedDif,
                SpecialOption = (SpecialOption)SpecialOpt,
                EnemyCount = EnemyCount,
                MaxCount = MaxCount,
                StageInitializerImage = SI_Bitmap,
                BackgroundTile = BackgroundTile,
                ForeGroundTile = ForeGroundTile,
            };

            return map;
        }

        private static int[] ParseEnemyCount(Span<byte> bytes)
        {
            var count = new int[24];
            for (var i = 0; i < count.Length; i++)
            {
                count[i] = bytes[i * 2] + bytes[i * 2 + 1] * 256;
            }
            return count;
        }

        private static List<TileBase> ParseTile(Span<byte> bytes)
        {
            var tiles = new List<TileBase>();
            for (var i = 0; i < XTiles * YTiles * 2; i += 2)
            {
                var x = i / 2 / YTiles;
                var y = i / 2 % YTiles;
                var z = bytes[i] + bytes[i + 1] * 256 + 1 & 0xFFFF;

                var tile = Parse(z, x, y);
                if (tile is not null)
                {
                    tiles.Add(tile);
                }
            }

            return tiles;
        }

        private static TileBase? Parse(int hex, int x, int y) => hex switch
        {
            0x01 => new Ice(x, y, 0),
            0x02 => new Ice(x, y, 1),
            0x03 => new Ice(x, y, 2),
            0x04 => new Ice(x, y, 3),

            0x05 => new Water(x, y, 0),
            0x06 => new Water(x, y, 1),
            0x07 => new Water(x, y, 2),
            0x08 => new Water(x, y, 3),

            0x09 => new Bush(x, y, 0),
            0x0A => new Bush(x, y, 1),
            0x0B => new Bush(x, y, 2),
            0x0C => new Bush(x, y, 3),

            0x0D => new Dust(x, y, 0),
            0x0E => new Dust(x, y, 1),
            0x0F => new Dust(x, y, 2),
            0x10 => new Dust(x, y, 3),

            0x19 => new BrickFull(x, y, 0),
            0x1A => new BrickFull(x, y, 1),
            0x1B => new BrickFull(x, y, 2),
            0x1C => new BrickFull(x, y, 3),

            0x1D => new BrickHalfTop(x, y, 0),
            0x1E => new BrickHalfTop(x, y, 1),

            0x1F => new BrickHalfRight(x, y, 0),
            0x20 => new BrickHalfRight(x, y, 1),

            0x21 => new BrickHalfBottom(x, y, 0),
            0x22 => new BrickHalfBottom(x, y, 1),

            0x23 => new BrickHalfLeft(x, y, 0),
            0x24 => new BrickHalfLeft(x, y, 1),

            0x25 => new BrickQuarterTopLeft(x, y),

            0x26 => new BrickQuarterTopRight(x, y),

            0x27 => new BrickQuarterBottomRight(x, y),

            0x28 => new BrickQuarterBottomLeft(x, y),

            0x29 => new Concrete4(x, y, 0),
            0x2A => new Concrete4(x, y, 1),
            0x2B => new Concrete4(x, y, 2),
            0x2C => new Concrete4(x, y, 3),

            0x2D => new Concrete3(x, y, 0),
            0x2E => new Concrete3(x, y, 1),
            0x2F => new Concrete3(x, y, 2),
            0x30 => new Concrete3(x, y, 3),

            0x31 => new Concrete2(x, y, 0),
            0x32 => new Concrete2(x, y, 1),
            0x33 => new Concrete2(x, y, 2),
            0x34 => new Concrete2(x, y, 3),

            0x35 => new Concrete1(x, y, 0),
            0x36 => new Concrete1(x, y, 1),
            0x37 => new Concrete1(x, y, 2),
            0x38 => new Concrete1(x, y, 3),

            0x39 => new Iron(x, y, 0),
            0x3A => new Iron(x, y, 1),
            0x3B => new Iron(x, y, 2),
            0x3C => new Iron(x, y, 3),

            0x3D => new Crystal(x, y, 0),
            0x3E => new Crystal(x, y, 1),
            0x3F => new Crystal(x, y, 2),
            0x40 => new Crystal(x, y, 3),

            _ => null,
        };

        public static Dictionary<Difficulty, Stage[]> Stages { get; } = Parse(File.ReadAllBytes("./Assets/Stage.dat"));
        public static (Difficulty Difficulty, Stage Stage)[] AllStages { get; } = Stages.SelectMany(kv => kv.Value.Select(v => (kv.Key, v))).ToArray();
    }
}
