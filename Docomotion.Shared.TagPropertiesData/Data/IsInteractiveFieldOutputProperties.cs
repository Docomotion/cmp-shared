using System.Collections.Generic;
using System.Xml.Serialization;
//Autofont
using Docomotion.Shared.TagPropertiesData.Defines;
using static Docomotion.Shared.ComDef.FreeFormDefinitions.FDC.Enums;

namespace Docomotion.Shared.TagPropertiesData.Data
{
    public class IsInteractiveFieldFormating : InteractiveFieldFormating
    {
        public override BaseTagProperty Clone()
        {
            IsInteractiveFieldFormating newClone = new IsInteractiveFieldFormating();
            newClone.version = this.version;
            newClone.type = this.type;
            newClone.general = this.general.Clone();
            newClone.appearance = appearance.Clone();
            newClone.options = this.options.Clone();
            newClone.actions = this.actions.Clone();
            newClone.custom_meta_data = this.custom_meta_data.Clone();
            return newClone;
        }
    }
}