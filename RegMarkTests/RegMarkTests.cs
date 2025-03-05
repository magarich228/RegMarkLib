using REG_MARK_LIB;

namespace RegMarkTests;

[TestFixture]
public class RegistrationMarkTests
{
    [Test]
    public void CheckMark_NonExistentNumberRange_ReturnsFalse()
    {
        string mark = "Z000ZZ00";
        bool expected = false;
        
        bool actual = RegMarkHelper.CheckMark(mark);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfter_SimpleIncrement_ReturnsNextNumber()
    {
        string currentMark = "M023OT152";
        string expected = "M024OT152";
        
        string actual = RegMarkHelper.GetNextMarkAfter(currentMark);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfter_LetterCombinationOverflow_UpdatesLetters()
    {
        string currentMark = "A999BC52";
        string expected = "A001BT52";
        string actual = RegMarkHelper.GetNextMarkAfter(currentMark);
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void CheckMark_ValidFormat_ReturnsTrue()
    {
        string mark = "A777AA52";
        bool expected = true;
        
        bool actual = RegMarkHelper.CheckMark(mark);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CheckMark_MaxAllowedValue_ReturnsTrue()
    {
        string mark = "X999XX99";
        bool expected = true;
        
        bool actual = RegMarkHelper.CheckMark(mark);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CheckMark_InvalidLength_ReturnsFalse()
    {
        string mark = "B322OL333";
        bool expected = false;
        
        bool actual = RegMarkHelper.CheckMark(mark);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfterInRange_WithinValidRange_ReturnsNextMark()
    {
        string currentMark = "A041BC77";
        string rangeStart = "A030BC77";
        string rangeEnd = "A090BC77";
        string expected = "A042BC77";
        
        string actual = RegMarkHelper.GetNextMarkAfterInRange(currentMark, rangeStart, rangeEnd);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfterInRange_AtUpperBound_ReturnsOutOfStock()
    {
        string currentMark = "X190AX77";
        string rangeStart = "X120AX77";
        string rangeEnd = "X190AX77";
        string expected = "out of stock";
        
        string actual = RegMarkHelper.GetNextMarkAfterInRange(currentMark, rangeStart, rangeEnd);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetCombinationsCountInRange_SmallRange_ReturnsCorrectCount()
    {
        string start = "A123BC99";
        string end = "A127BC99";
        int expected = 5;
        
        int actual = RegMarkHelper.GetCombinationsCountInRange(start, end);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetCombinationsCountInRange_FullHundredRange_Returns100()
    {
        string start = "A000AA99";
        string end = "A099AA99";
        int expected = 100;
        
        int actual = RegMarkHelper.GetCombinationsCountInRange(start, end);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfter_RegionCodeOverflow_UpdatesRegion()
    {
        string currentMark = "A999YX99";
        string expected = "A001XA99";
        
        string actual = RegMarkHelper.GetNextMarkAfter(currentMark);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfterInRange_SingleValueRange_ReturnsOutOfStock()
    {
        string currentMark = "M023OT152";
        string rangeStart = "M023OT152";
        string rangeEnd = "M023OT152";
        string expected = "out of stock";
        
        string actual = RegMarkHelper.GetNextMarkAfterInRange(currentMark, rangeStart, rangeEnd);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfterInRange_InvalidInput_ReturnsFormatError()
    {
        string currentMark = " ";
        string rangeStart = " ";
        string rangeEnd = " ";
        string expected = "Invalid format";
        
        string actual = RegMarkHelper.GetNextMarkAfterInRange(currentMark, rangeStart, rangeEnd);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetCombinationsCountInRange_InvalidInput_ReturnsErrorCode()
    {
        string start = " ";
        string end = " ";
        int expected = -1;
        
        int actual = RegMarkHelper.GetCombinationsCountInRange(start, end);
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextMarkAfterInRange_LastPossibleMark_ReturnsOutOfStock()
    {
        string currentMark = "A999BC99";
        string rangeStart = "A990BC99";
        string rangeEnd = "A999BC99";
        string expected = "out of stock";
        
        string actual = RegMarkHelper.GetNextMarkAfterInRange(currentMark, rangeStart, rangeEnd);
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}