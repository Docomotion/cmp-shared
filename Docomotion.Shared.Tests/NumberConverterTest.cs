using Docomotion.Shared.NumberUtils;
using Docomotion.Shared.NumberUtils.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Docomotion.Shared.Tests
{
    [TestFixture]
    public class NumberConverterTest
    {
        [TestCase(0.0, Language.HEBREW, Currency.NIS, "")]
        [TestCase(0.01, Language.HEBREW, Currency.NIS, "1 אג'")]
        [TestCase(1.0, Language.HEBREW, Currency.NIS, "אחד ש\"ח")]
        [TestCase(10.0, Language.HEBREW, Currency.NIS, "עשרה ש\"ח")]
        [TestCase(11.11, Language.HEBREW, Currency.NIS, "אחד עשר ש\"ח + 11 אג'")]
        [TestCase(43210987.65, Language.HEBREW, Currency.NIS, "ארבעים ושלושה מיליון ומאתיים עשרה אלף ותשע מאות שמונים ושבעה ש\"ח + 65 אג'")]
        [TestCase(100000000000, Language.HEBREW, Currency.NIS, "מאה מיליארד ש\"ח")]
        
        [TestCase(0.0, Language.HEBREW, Currency.USD, "")]
        [TestCase(0.01, Language.HEBREW, Currency.USD, "1 סנט")]
        [TestCase(1.0, Language.HEBREW, Currency.USD, "אחד דולר")]
        [TestCase(10.0, Language.HEBREW, Currency.USD, "עשרה דולרים")]
        [TestCase(11.11, Language.HEBREW, Currency.USD, "אחד עשר דולרים + 11 סנטים")]
        [TestCase(43210987.65, Language.HEBREW, Currency.USD, "ארבעים ושלושה מיליון ומאתיים עשרה אלף ותשע מאות שמונים ושבעה דולרים + 65 סנטים")]
        [TestCase(100000000000, Language.HEBREW, Currency.USD, "מאה מיליארד דולרים")]
        
        [TestCase(0.0, Language.HEBREW, Currency.EURO, "")]
        [TestCase(0.01, Language.HEBREW, Currency.EURO, "1 סנט")]
        [TestCase(1.0, Language.HEBREW, Currency.EURO, "אחד אירו")]
        [TestCase(10.0, Language.HEBREW, Currency.EURO, "עשרה אירו")]
        [TestCase(11.11, Language.HEBREW, Currency.EURO, "אחד עשר אירו + 11 סנטים")]
        [TestCase(43210987.65, Language.HEBREW, Currency.EURO, "ארבעים ושלושה מיליון ומאתיים עשרה אלף ותשע מאות שמונים ושבעה אירו + 65 סנטים")]
        [TestCase(100000000000, Language.HEBREW, Currency.EURO, "מאה מיליארד אירו")]
        
        [TestCase(0.0, Language.HEBREW, Currency.NONE, "")]
        [TestCase(0.01, Language.HEBREW, Currency.NONE, "")]
        [TestCase(0.89, Language.HEBREW, Currency.NONE, "אחד")]
        [TestCase(1.0, Language.HEBREW, Currency.NONE, "אחד")]
        [TestCase(1.89, Language.HEBREW, Currency.NONE, "שניים")]
        [TestCase(10.0, Language.HEBREW, Currency.NONE, "עשרה")]
        [TestCase(11.11, Language.HEBREW, Currency.NONE, "אחד עשר")]
        [TestCase(43210987.45, Language.HEBREW, Currency.NONE, "ארבעים ושלושה מיליון ומאתיים עשרה אלף ותשע מאות שמונים ושבעה")]
        [TestCase(100000000000, Language.HEBREW, Currency.NONE, "מאה מיליארד")]
        public void ShouldConvertToCurrencyWordsToHebrewSuccessfully(
            double number,
            Language language,
            Currency currency,
            string expectedResult)
        {
            var actualResult = NumberConverter.ToWords(number, language, currency);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        
        [TestCase(0.0, Language.ENGLISH, Currency.NIS, "")]
        [TestCase(0.01, Language.ENGLISH, Currency.NIS, "1 Agora")]
        [TestCase(1.0, Language.ENGLISH, Currency.NIS, "One NIS")]
        [TestCase(10.0, Language.ENGLISH, Currency.NIS, "Ten NIS")]
        [TestCase(11.11, Language.ENGLISH, Currency.NIS, "Eleven NIS and 11 Agorot")]
        [TestCase(43210987.65, Language.ENGLISH, Currency.NIS, "Forty Three Million and Two Hundred Ten Thousand and Nine Hundred Eighty Seven NIS and 65 Agorot")]
        [TestCase(100000000000, Language.ENGLISH, Currency.NIS, "One Hundred Billion NIS")]
        
        [TestCase(0.0, Language.ENGLISH, Currency.USD, "")]
        [TestCase(0.01, Language.ENGLISH, Currency.USD, "1 Cent")]
        [TestCase(1.0, Language.ENGLISH, Currency.USD, "One Dollar")]
        [TestCase(10.0, Language.ENGLISH, Currency.USD, "Ten Dollars")]
        [TestCase(11.11, Language.ENGLISH, Currency.USD, "Eleven Dollars and 11 Cents")]
        [TestCase(43210987.65, Language.ENGLISH, Currency.USD, "Forty Three Million and Two Hundred Ten Thousand and Nine Hundred Eighty Seven Dollars and 65 Cents")]
        [TestCase(100000000000, Language.ENGLISH, Currency.USD, "One Hundred Billion Dollars")]

        [TestCase(0.0, Language.ENGLISH, Currency.EURO, "")]
        [TestCase(0.01, Language.ENGLISH, Currency.EURO, "1 Cent")]
        [TestCase(1.0, Language.ENGLISH, Currency.EURO, "One Euro")]
        [TestCase(10.0, Language.ENGLISH, Currency.EURO, "Ten Euro")]
        [TestCase(11.11, Language.ENGLISH, Currency.EURO, "Eleven Euro and 11 Cents")]
        [TestCase(43210987.65, Language.ENGLISH, Currency.EURO, "Forty Three Million and Two Hundred Ten Thousand and Nine Hundred Eighty Seven Euro and 65 Cents")]
        [TestCase(100000000000, Language.ENGLISH, Currency.EURO, "One Hundred Billion Euro")]

        [TestCase(0.0, Language.ENGLISH, Currency.NONE, "")]
        [TestCase(0.01, Language.ENGLISH, Currency.NONE, "")]
        [TestCase(0.89, Language.ENGLISH, Currency.NONE, "One")]
        [TestCase(1.0, Language.ENGLISH, Currency.NONE, "One")]
        [TestCase(1.89, Language.ENGLISH, Currency.NONE, "Two")]
        [TestCase(10.0, Language.ENGLISH, Currency.NONE, "Ten")]
        [TestCase(11.11, Language.ENGLISH, Currency.NONE, "Eleven")]
        [TestCase(43210987.45, Language.ENGLISH, Currency.NONE, "Forty Three Million and Two Hundred Ten Thousand and Nine Hundred Eighty Seven")]
        [TestCase(100000000000, Language.ENGLISH, Currency.NONE, "One Hundred Billion")]
        public void ShouldConvertToCurrencyWordsToEnglishSuccessfully(
            double number,
            Language language,
            Currency currency,
            string expectedResult)
        {
            var actualResult = NumberConverter.ToWords(number, language, currency);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ShouldReturnNotSupportedOnNumberLimit()
        {
            var actualResult = NumberConverter.ToWords(1000000000001);
            actualResult.Should().BeEquivalentTo("Not supported");
        }
    }
}
