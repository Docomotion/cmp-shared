using System;
using System.Collections.Generic;
using System.Globalization;

namespace Docomotion.Shared.NumberUtils.Humanizer.Localisation.NumberToWords
{
    internal class HebrewNumberToWordsConverter : GenderedNumberToWordsConverter
    {
        private static readonly string[] UnitsFeminine = { "אפס", "אחת", "שתיים", "שלוש", "ארבע", "חמש", "שש", "שבע", "שמונה", "תשע", "עשר" };
        private static readonly string[] UnitsMasculine = { "אפס", "אחד", "שניים", "שלושה", "ארבעה", "חמישה", "שישה", "שבעה", "שמונה", "תשעה", "עשרה" };
        private static readonly string[] TensUnit = { "עשר", "עשרים", "שלושים", "ארבעים", "חמישים", "שישים", "שבעים", "שמונים", "תשעים" };

        private const string AndSymbol = "ו";

        private readonly CultureInfo _culture;

        private class DescriptionAttribute : Attribute
        {
            public string Description { get; set; }

            public DescriptionAttribute(string description)
            {
                Description = description;
            }
        }

        private enum Group
        {
            Hundreds = 100,
            Thousands = 1000,
            [Description("מיליון")]
            Millions = 1000000,
            [Description("מיליארד")]
            Billions = 1000000000
        }

        public HebrewNumberToWordsConverter(CultureInfo culture)
        {
            _culture = culture;
        }

        public override string Convert(long input, GrammaticalGender gender, bool addAnd = true)
        {
            var number = input;

            if (number < 0)
            {
                return string.Format("מינוס {0}", Convert(-number, gender));
            }

            if (number == 0)
            {
                return UnitsFeminine[0];
            }

            var parts = new List<string>();
            if (number >= (int)Group.Billions)
            {
                ToBigNumber(number, Group.Billions, parts);
                number %= (long)Group.Billions;
            }

            if (number >= (int)Group.Millions)
            {
                ToBigNumber(number, Group.Millions, parts);
                number %= (long)Group.Millions;
            }

            if (number >= (int)Group.Thousands)
            {
                ToThousands(number, parts, addAnd);
                number %= (long)Group.Thousands;
            }

            if (number >= (int)Group.Hundreds)
            {
                ToHundreds(number, parts, addAnd);
                number %= (long)Group.Hundreds;
            }

            if (number > 0)
            {
                if (number <= 10)
                {
                    var unit = gender == GrammaticalGender.Masculine ? UnitsMasculine[number] : UnitsFeminine[number];
                    parts.Add(unit);
                }
                else if (number < 20)
                {
                    var unit = Convert(number % 10, gender);
                    unit = unit.Replace("יי", "י");
                    unit = string.Format("{0} {1}", unit, gender == GrammaticalGender.Masculine ? "עשר" : "עשרה");
                    parts.Add(unit);
                }
                else
                {
                    var tenUnit = TensUnit[number / 10 - 1];
                    if (number % 10 == 0)
                    {
                        parts.Add(tenUnit);
                    }
                    else
                    {
                        var unit = Convert(number % 10, gender);
                        parts.Add(string.Format("{0} ו{1}", tenUnit, unit));
                    }
                }
            }

            return string.Join(" ", parts);
        }

        public override string ConvertToOrdinal(int number, GrammaticalGender gender)
        {
            return number.ToString(_culture);
        }

        private void ToBigNumber(long number, Group group, List<string> parts)
        {
            // Big numbers (million and above) always use the masculine form
            // See https://www.safa-ivrit.org/dikduk/numbers.php

            var digits = number / (long)@group;
            if (digits == 2)
            {
                parts.Add("שני");
            }
            else if (digits > 2)
            {
                parts.Add(Convert(digits, GrammaticalGender.Masculine));
            }

            parts.Add(@group.Humanize());
        }

        private void ToThousands(long number, List<string> parts, bool addAnd = true)
        {
            var thousands = number / (int)Group.Thousands;
            string part;

            switch (thousands)
            {
                case 1:
                    part = "אלף";
                    break;
                case 2:
                    part = "אלפיים";
                    break;
                case 8:
                    part = "שמונת אלפים";
                    break;
                default:
                {
                    if (thousands <= 10)
                    {
                        part = UnitsFeminine[thousands] + "ת" + " אלפים";
                        break;
                    }

                    part = Convert(thousands) + " אלף";
                    break;
                }
            }
            
            parts.Add((parts.Count > 0 && addAnd ? AndSymbol : string.Empty) + part);
        }

        private static void ToHundreds(long number, List<string> parts, bool addAnd = true)
        {
            // For hundreds, Hebrew is using the feminine form
            // See https://www.safa-ivrit.org/dikduk/numbers.php

            var hundreds = number / (int)Group.Hundreds;
            string part;

            switch (hundreds)
            {
                case 1:
                    part = "מאה";
                    break;
                case 2:
                    part = "מאתיים";
                    break;
                default:
                    part = UnitsFeminine[hundreds] + " מאות";
                    break;
            }
            
            parts.Add((parts.Count > 0 && addAnd ? AndSymbol : string.Empty) + part);
        }
    }
}
