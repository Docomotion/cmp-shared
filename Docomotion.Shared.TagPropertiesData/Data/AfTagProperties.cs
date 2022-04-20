
namespace Docomotion.Shared.TagPropertiesData.Data
{
    public class AfTagProperties
    {
        public class TagProperties
        {
            public TagOutputProperties outputProperties;
            public string inputProperties = null;

            public TagProperties Clone()
            {
                TagProperties newClone = new TagProperties();
                newClone.outputProperties = (TagOutputProperties)outputProperties.Clone();
                if (inputProperties != null)
                {
                    newClone.inputProperties = (string)inputProperties.Clone();
                }
                return newClone;
            }
        }
    }
}
