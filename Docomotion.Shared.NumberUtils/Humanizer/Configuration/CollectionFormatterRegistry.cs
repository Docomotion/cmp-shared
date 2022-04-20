using Docomotion.Shared.NumberUtils.Humanizer.Localisation.CollectionFormatters;

namespace Docomotion.Shared.NumberUtils.Humanizer.Configuration
{
    internal class CollectionFormatterRegistry : LocaliserRegistry<ICollectionFormatter>
    {
        public CollectionFormatterRegistry()
            : base(new DefaultCollectionFormatter("&"))
        {
            Register("en", new OxfordStyleCollectionFormatter("and"));
        }
    }
}
