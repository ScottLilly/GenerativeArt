using System.Drawing;
using System.Security.Cryptography;

namespace GenerativeArt.Core;

public static class Randomizer
{
    public static int GetRandomNumberBetween(int minimumValue, int maximumValue)
    {
        // Need to add one to maximumValue, because otherwise,
        // this function will never generate a value that matches the maximumValue.
        // For example: to get a value from 1 to 10 (inclusive),
        // The code must (effectively) call: RandomNumberGenerator.GetInt32(1, 11);
        return RandomNumberGenerator.GetInt32(minimumValue, maximumValue + 1);
    }

    public static string GetRandomColor()
    {
        return ColorTranslator.ToHtml(Color.FromArgb(255,
            RandomByteValue(), RandomByteValue(), RandomByteValue()));
    }

    public static byte RandomByteValue()
    {
        return (byte)Randomizer.GetRandomNumberBetween(0, byte.MaxValue + 1);
        //return (byte)RngCreator.GetRandomNumberBetween(0, (byte.MaxValue + 1)/2); // Dark colors
        //return (byte)RngCreator.GetRandomNumberBetween(128, byte.MaxValue + 1); // Light colors
    }
}