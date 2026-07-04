namespace TestProject.Trim;

/// <summary>
/// Metnin başındaki ve sonundaki boşlukları kırpar.
/// </summary>
public static class WhitespaceTrimmer
{
    public static string Trim(string? value) =>
        string.IsNullOrEmpty(value) ? value ?? "" : value.Trim();
}
