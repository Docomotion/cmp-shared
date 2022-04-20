using System.Collections.Generic;
using Docomotion.Shared.NumberUtils.Humanizer.Configuration;

namespace Docomotion.Shared.NumberUtils.Humanizer
{
    /// <summary>
    /// Humanizes an IEnumerable into a human readable list
    /// </summary>
    internal static class CollectionHumanizeExtensions
    {
        /// <summary>
        /// Formats the collection for display, calling ToString() on each object and
        /// using the default separator for the current culture.
        /// </summary>
        public static string Humanize<T>(this IEnumerable<T> collection)
        {
            return Configurator.CollectionFormatter.Humanize(collection);
        }
    }
}
