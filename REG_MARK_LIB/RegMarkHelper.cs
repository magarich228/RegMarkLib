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
        if (!CheckMark(mark))
        {
            return "Invalid format";
        }

        char letter1 = mark[0];
        char letter2 = mark[4];
        char letter3 = mark[5];
        
        int number = int.Parse(mark.Substring(1, 3));
        int region = int.Parse(mark.Substring(6));

        number++;

        if (number > 199)
        {
            number = 1;

            int indexLetter3 = validLetters.IndexOf(letter3);
            
            if (indexLetter3 < validLetters.Length - 1)
            {
                letter3 = validLetters[indexLetter3 + 1];
            }
            else
            {
                letter3 = validLetters[0];

                int indexLetter2 = validLetters.IndexOf(letter2);
                
                if (indexLetter2 < validLetters.Length - 1)
                {
                    letter2 = validLetters[indexLetter2 + 1];
                }
                else
                {
                    letter2 = validLetters[0];

                    int indexLetter1 = validLetters.IndexOf(letter1);
                    
                    if (indexLetter1 < validLetters.Length - 1)
                    {
                        letter1 = validLetters[indexLetter1 + 1];
                    }
                    else
                    {
                        letter1 = validLetters[0];
                        region++;
                        if (region > 199) region = 1;
                    }
                }
            }
        }

        return $"{letter1}{number:D3}{letter2}{letter3}{region}";
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
}