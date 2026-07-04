namespace TestProject.Text;

/// <summary>
/// Metindeki tüm büyük harfleri küçük harfe çevirir.
/// </summary>
public static class UpperToLowerConverter
{
    public static string Convert(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? "";

        return value.ToLowerInvariant();
    }
}
