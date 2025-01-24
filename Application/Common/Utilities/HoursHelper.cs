namespace Application.Common.Utilities;
internal static partial class HoursHelper
{
    [GeneratedRegex(@"(\d+)\s+(?:or|to)\s+(\d+)")]
    internal static partial Regex HoursRegex();

    internal static (byte minHours, byte maxHours) FromText(string hours)
    {
        byte minHours;
        byte maxHours = 0;

        Match multipleHours = HoursRegex().Match(hours);

        if (multipleHours.Success)
        {
            _ = byte.TryParse(multipleHours.Groups[1].Value, out minHours);
            _ = byte.TryParse(multipleHours.Groups[2].Value, out maxHours);
        }
        else
        {
            _ = byte.TryParse(hours, out minHours);
        }

        return (minHours, maxHours);
    }
}
