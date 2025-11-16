using MudBlazor;

public class CustomMudTheme : MudTheme
{
    public CustomMudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = Colors.Blue.Darken2,
            Secondary = Colors.Pink.Default,
            Info = Colors.Blue.Lighten1,
            Success = Colors.Green.Default,
            Warning = Colors.Amber.Default,
            Error = Colors.Red.Darken1,
            Background = "#f5f5f5",
            Surface = "#ffffff"
        };
    }
}