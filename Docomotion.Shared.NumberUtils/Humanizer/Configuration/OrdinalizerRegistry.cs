using Docomotion.Shared.NumberUtils.Humanizer.Localisation.Ordinalizers;

namespace Docomotion.Shared.NumberUtils.Humanizer.Configuration
{
    internal class OrdinalizerRegistry : LocaliserRegistry<IOrdinalizer>
    {
        public OrdinalizerRegistry() : base(new DefaultOrdinalizer())
        {
            Register("en", new EnglishOrdinalizer());
        }
    }
}
