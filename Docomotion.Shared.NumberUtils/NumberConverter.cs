using System;
using System.Globalization;
using Docomotion.Shared.NumberUtils.Enums;
using Docomotion.Shared.NumberUtils.Humanizer;
using Docomotion.Shared.NumberUtils.Localization;

namespace Docomotion.Shared.NumberUtils
{
    public class NumberConverter
    {
        /// <summary>
        /// Converts <param name="number"/> that represents a sum to words with currency.
        /// </summary>
        /// <param name="number">A number that represents a sum</param>
        /// <param name="language">A language that number will be translated into</param>
        /// <param name="currency">A currency that will represent an amount of sum</param>
        /// <returns></returns>
        public static string ToWords(
            double number,
            Language language = Language.ENGLISH,
            Currency currency = Currency.USD)
        {
            if (number > 1000000000000)
                return "Not supported";
            
            var localization = LocalizationFactory.Crete(language);

            if (currency == Currency.NONE)
            {
                var roundedUnits = (long)Math.Round(number);
                return roundedUnits == 0 ? string.Empty : NumberToWords(roundedUnits, localization.GetCultureInfo());
            }
            
            return ToCurrencyWords(number, currency, localization);
        }

        private static string ToCurrencyWords(double number, Currency currency, ILocalization localization)
        {
            // Add whole-currency values in
            var units = (long) Math.Truncate(number);
            var pieces = GetCents(number);
            var unitsText = units == 0
                ? string.Empty
                : NumberToWords(units, localization.GetCultureInfo());
            var piecesText = pieces == 0
                ? string.Empty
                : pieces.ToString();
            var separatorText = units != 0 && pieces != 0 ? $" {localization.GetSeparator()} " : string.Empty;

            var currencyUnitName = GetCurrencyUnitName(units, localization, currency);
            var currencyPieceName = GetCurrencyPieceName(pieces, localization, currency);

            return $"{unitsText} {currencyUnitName}{separatorText}{piecesText} {currencyPieceName}"
                .Trim();
        }

        private static string NumberToWords(long units, CultureInfo cultureInfo)
        {
            return units.ToWords(cultureInfo)
                .Replace('-', ' ')
                .Humanize(LetterCasing.Title);
        }

        private static string GetCurrencyUnitName(long units, ILocalization localization, Currency currency)
        {
            switch (units)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return localization.GetUnitSingleName(currency);
                default:
                    return localization.GetUnitPluralName(currency);
            }
        }

        private static string GetCurrencyPieceName(long pieces, ILocalization localization, Currency currency)
        {
            switch (pieces)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return localization.GetPieceSingleName(currency);
                default:
                    return localization.GetPiecePluralName(currency);
            }
        }

        private static long GetCents(double number)
        {
            // Now, calculate cents

            // Negatives mess with the modulo math, and
            // are already handled by the whole-dollar
            // English expression
            var absoluteNumber = Math.Abs(number);
            // Total cents in the entire number
            var totalCents = Math.Truncate(absoluteNumber * 100);
            // Just the cent value from the number
            var justCents = totalCents % 100;
            // Cast back to an integral type
            var cents = (long) justCents;
            return cents;
        }
    }
}
