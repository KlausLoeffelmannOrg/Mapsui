namespace Mapsui.Samples.WindowsForms.Direct2D;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        MessageBox.Show(
            "The Mapsui Direct2D sample host is scaffolded. The interactive map browser arrives with the Direct2D MapControl.",
            "Mapsui Direct2D",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}
