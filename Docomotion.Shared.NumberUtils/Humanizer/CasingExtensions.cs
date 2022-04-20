﻿using System;
using Docomotion.Shared.NumberUtils.Humanizer.Transformer;

namespace Docomotion.Shared.NumberUtils.Humanizer
{
    /// <summary>
    /// ApplyCase method to allow changing the case of a sentence easily
    /// </summary>
    internal static class CasingExtensions
    {
        /// <summary>
        /// Changes the casing of the provided input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="casing"></param>
        /// <returns></returns>
        public static string ApplyCase(this string input, LetterCasing casing)
        {
            switch (casing)
            {
                case LetterCasing.Title:
                    return input.Transform(To.TitleCase);

                case LetterCasing.LowerCase:
                    return input.Transform(To.LowerCase);

                case LetterCasing.AllCaps:
                    return input.Transform(To.UpperCase);

                case LetterCasing.Sentence:
                    return input.Transform(To.SentenceCase);

                default:
                    throw new ArgumentOutOfRangeException(nameof(casing));
            }
        }
    }
}
