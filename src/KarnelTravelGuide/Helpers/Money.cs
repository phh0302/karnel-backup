using System.Globalization;

namespace KarnelTravelGuide.Helpers;

/// <summary>
/// Formats prices the way the GUI Standards require: "1.800.000 ₫"
/// (dot as thousands separator, the dong sign after the number).
/// </summary>
public static class Money
{
    private static readonly CultureInfo Vi = new("vi-VN");

    /// <summary>Returns the amount as Vietnamese dong, e.g. 1800000 → "1.800.000 ₫". Zero shows "Free".</summary>
    public static string Vnd(decimal amount, bool zeroIsFree = false)
    {
        if (zeroIsFree && amount == 0) return "Free";
        return amount.ToString("#,0", Vi) + " ₫";
    }

    /// <summary>Returns "★★★★☆" style text for a 1-5 star value.</summary>
    public static string Stars(int stars)
    {
        stars = Math.Clamp(stars, 0, 5);
        return new string('★', stars) + new string('☆', 5 - stars);
    }

    /// <summary>Returns "₫", "₫₫" ... for a restaurant price level 1-4.</summary>
    public static string PriceLevel(int level) => new('₫', Math.Clamp(level, 1, 4));
}
