using System.Text.RegularExpressions;

namespace REG_MARK_LIB;

public class RegMarkHelper
{
    private const string validLetters = "ABEKMHOPCTYX";
    private static readonly Regex regMarkRegex = new(@"^[ABEKMHOPCTYX]\d{3}[ABEKMHOPCTYX]{2}\d{2,3}$");

    public static bool CheckMark(string mark)
    {
        if (!regMarkRegex.IsMatch(mark))
        {
            return false;
        }

        int region = int.Parse(mark.Substring(6));

        return (region is >= 1 and <= 199);
    }

    public static string GetNextMarkAfter(string mark)
    {
        if (!CheckMark(mark)) return "Invalid format";

        const int maxNumber = 199;
        const int maxRegion = 199;
    
        char[] chars = { mark[0], mark[4], mark[5] };
        int number = int.Parse(mark.Substring(1, 3)) + 1;
        int region = int.Parse(mark.Substring(6));

        if (number > maxNumber)
        {
            number = 1;
            bool overflow = UpdateLetters(chars);
            if (overflow) region = region % maxRegion + 1;
        }

        return $"{chars[0]}{number:D3}{chars[1]}{chars[2]}{region}";
    }

    public static string GetNextMarkAfterInRange(string prevMark, string rangeStart, string rangeEnd)
    {
        if (!CheckMark(prevMark) || !CheckMark(rangeStart) || !CheckMark(rangeEnd))
        {
            return "Invalid format";
        }

        string nextMark = GetNextMarkAfter(prevMark);

        if (String.CompareOrdinal(nextMark, rangeStart) >= 0 &&
            String.CompareOrdinal(nextMark, rangeEnd) <= 0)
        {
            return nextMark;
        }

        return "out of stock";
    }

    public static int GetCombinationsCountInRange(string mark1, string mark2)
    {
        if (!CheckMark(mark1) || !CheckMark(mark2))
        {
            return -1;
        }

        int count = 0;
        string currentMark = mark1;

        while (String.CompareOrdinal(currentMark, mark2) <= 0)
        {
            count++;
            currentMark = GetNextMarkAfter(currentMark);

            if (currentMark == mark1)
            {
                break;
            }
        }

        return count;
    }
    
    private static bool UpdateLetters(char[] chars)
    {
        for (int i = 2; i >= 0; i--)
        {
            int index = validLetters.IndexOf(chars[i]);
            if (index < validLetters.Length - 1)
            {
                chars[i] = validLetters[index + 1];
                return false;
            }
            chars[i] = validLetters[0];
        }
        return true;
    }
}