using FailSafeWeatherMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>()
    .Build();

var options = new DbContextOptionsBuilder<FailSafeWeatherMonitorDbContext>()
    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
    .Options;

using var _db = new FailSafeWeatherMonitorDbContext(options);

//var dataPoint = _db.WeatherDataPoints
//    .Where(w => w.Id == 1)
//    .FirstOrDefault();

//if (dataPoint is not null)
//{
//    Console.WriteLine($"{dataPoint.StationName}");
//    _db.WeatherDataPoints.Remove(dataPoint);
//    _db.SaveChanges();
//}
//else
//{
//    Console.WriteLine("not found");
//}


Console.BackgroundColor = ConsoleColor.Black;
Console.Clear();
Console.CursorVisible = false;

string logo = """
┏╾┄───┐┏╾┄───┐┏╾┄─┐┏╾┄─┐  ┏╾┄───┐┏╾┄───┐┏╾┄───┐┏╾┄───┐
┊  ──┬┘┊  ┼  │┊   │┊   │  ┊  └──┤┊  ┼  │┊  ──┬┘┊   ─┬┘
│  ─┬┘ │  ╷  ││   ││   ├─┐├──┐  ││  ╷  ││   ┬┘ │   ─┴┐
└──┄┛  └──┴─┄┛└──┄┛└────┄┛└────┄┛└──┴─┄┛└──┄┛  └────┄┛
""";
string logoColorMask = """
11E666611E666611E6611E666611E666611E666611E666611E6666
E666666E66E666E6666E666666E666666E66E666E666666E666666
666666666666666666666666666666666666666666666666666666
666EE6666666EE666EE66666EE66666EE66666EE666EE6666666EE
""";
string logoBackgroundColorMask = """
000000000000000000000000000000000000000000000000000000
000000000000000000000000000000000000000000000000000000
000000000000000000000000000000000000000000000000000000
000000000000000000000000000000000000000000000000000000
""";

var panel = new Panel(ToMarkup(logo, logoColorMask, logoBackgroundColorMask)) { Width = 80, Height = 25 }
    .HeavyBorder()
    .Header("┫ Header ┣");

AnsiConsole.Write(panel);




Console.CursorVisible = false;
Console.ResetColor();
Console.WriteLine("\n\n");


static string CycleStringLeft(string s)
{
    string cycled = s[1..] + s[..1];

    return cycled;
}

static string ToMarkup(string line, string fgColorMask, string? bgColorMask)
{
    string markupString = "";
    char? last = null;
    for (int i = 0; i < line.Length; i++)
    {
        char current = fgColorMask[i % fgColorMask.Length];
        char next = fgColorMask[(i + 1) % fgColorMask.Length];

        if ("\n\t\r".Contains(line[i]) is false)
        {

            if (last is null || last != current)
            {
                int fgColorIndex = int.Parse(fgColorMask[i % fgColorMask.Length].ToString(), System.Globalization.NumberStyles.HexNumber);
                string colorCode = C64Palette.All[fgColorIndex];
                string bgColorCode = "";
                if (bgColorMask is not null)
                {
                    int bgColorIndex = int.Parse(bgColorMask[i % bgColorMask.Length].ToString(), System.Globalization.NumberStyles.HexNumber);
                    bgColorCode = $" on {C64Palette.All[bgColorIndex]}";
                }
                markupString += $"[{colorCode}{bgColorCode}]";
            }

            markupString += $"{line[i]}";

            if (current != next || i == line.Length - 1)
            {
                markupString += "[/]";
            }

        }
        else
        {
            markupString += $"{line[i]}";

        }
        last = current;
    }

    return markupString;
}


public class C64Palette
{
    public static string Black => "#000000";
    public static string White => "#FFFFFF";
    public static string Red => "#924A40";
    public static string Cyan => "#84C5CC";
    public static string Purple => "#9351B6";
    public static string Green => "#72B14B";
    public static string Blue => "#483AAA";
    public static string Yellow => "#D5DF7C";
    public static string Orange => "#99692D";
    public static string Brown => "#675200";
    public static string Pink => "#C18178";
    public static string DarkGrey => "#606060";
    public static string Gray => "#8A8A8A";
    public static string LightGreen => "#B3EC91";
    public static string LightBlue => "#867ADE";
    public static string LightGray => "#B3B3B3";

    public static string[] All => [
        Black,      // 0
        White,      // 1
        Red,        // 2
        Cyan,       // 3
        Purple,     // 4
        Green,      // 5
        Blue,       // 6
        Yellow,     // 7
        Orange,     // 8
        Brown,      // 9
        Pink,       // A
        DarkGrey,   // B
        Gray,       // C
        LightGreen, // D
        LightBlue,  // E
        LightGray   // F
    ];
}

/*


┏╾┄───┐┏╾┄───┐┏╾┄─┐┏╾┄─┐  ┏╾┄───┐┏╾┄───┐┏╾┄───┐┏╾┄───┐
┊  ──┬┘┊  ┼  │┊   │┊   │  ┊  └──┤┊  ┼  │┊  ──┬┘┊   ─┬┘
│  ─┬┘ │  ╷  ││   ││   ├─┐├──┐  ││  ╷  ││   ┬┘ │   ─┴┐
└──┄┛  └──┴─┄┛└──┄┛└────┄┛└────┄┛└──┴─┄┛└──┄┛  └────┄┛
                ┌─────────────────┐
                │ WEATHER MONITOR │
                └─────────────────┘






*/