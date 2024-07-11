using MudBlazor.Utilities;
using MudBlazor;

namespace Dima.Web
{
    public static class Configuration
    {
        public const string HttpClientName = "dima";
        public static string BackUrl = "http://localhost:5098";

        public static MudTheme Theme = new()
        {
            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },
            Palette = new PaletteLight
            {
                Primary = new MudColor("#b5b5b5"),
                PrimaryContrastText = new MudColor("#ffff"),
                Secondary = Colors.LightGreen.Darken3,
                Background = new MudColor("#666666"),
                AppbarBackground = new MudColor("#666666"),
                AppbarText = new MudColor("#ffff"),
                TextPrimary = new MudColor("#3c3c3c"),
                DrawerText = new MudColor("#ffff"),
                DrawerBackground = new MudColor("#3c3c3c")
            },
            PaletteDark = new PaletteDark
            {
                Primary = new MudColor("#666666"),
                Secondary = Colors.LightGreen.Darken3,
                Background = Colors.Shades.Black,
                AppbarBackground = Colors.Shades.Black,
                AppbarText = new MudColor("#b5b5b5"),
                PrimaryContrastText = new MudColor("#b5b5b5"),
            }
        };
    }
}
