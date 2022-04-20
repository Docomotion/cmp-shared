using System.Globalization;
using Docomotion.Shared.NumberUtils.Enums;

namespace Docomotion.Shared.NumberUtils.Localization
{
    /// <summary>
    /// Localization class that provides required information to represent data in particular localization.
    /// </summary>
    public interface ILocalization
    {
        /// <summary>
        /// Returns a single unit name regarding to <param name="currency"/>.
        /// </summary>
        ///<param name="currency">A currency that will represent a unit. Example: USD - Dollar</param>
        string GetUnitSingleName(Currency currency);

        /// <summary>
        /// Returns a plural unit name regarding to <param name="currency"/>.
        /// </summary>
        ///<param name="currency">A currency that will represent a unit. Example: USD - Dollars</param>
        string GetUnitPluralName(Currency currency);

        /// <summary>
        /// Returns a single piece name regarding to <param name="currency"/>.
        /// </summary>
        ///<param name="currency">A currency that will represent a single piece. Example: USD - Cent</param>
        string GetPieceSingleName(Currency currency);

        /// <summary>
        /// Returns a plural piece name regarding to <param name="currency"/>.
        /// </summary>
        ///<param name="currency">A currency that will represent a piece. Example: USD - Cents</param>
        string GetPiecePluralName(Currency currency);

        /// <summary>
        /// Returns a separator that will divide units and pieces.
        /// </summary>
        string GetSeparator();

        /// <summary>
        /// Returns a culture info that provides information about a specific culture.
        /// </summary>
        CultureInfo GetCultureInfo();
    }
}
