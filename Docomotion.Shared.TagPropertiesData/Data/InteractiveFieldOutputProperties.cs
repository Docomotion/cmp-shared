using System.Collections.Generic;
using System.Xml.Serialization;
using Docomotion.Shared.JScripts;
//Autofont
using Docomotion.Shared.TagPropertiesData.Defines;
using static Docomotion.Shared.ComDef.FreeFormDefinitions.FDC.Enums;

namespace Docomotion.Shared.TagPropertiesData.Data
{
    public class InteractiveFieldFormating : BaseTagProperty
    {
        public double version = 1.0;
        public InteractiveFieldType type = InteractiveFieldType.editfield;

        public InteractiveFieldGeneral general = new InteractiveFieldGeneral();
        public InteractiveFieldAppearance appearance = new InteractiveFieldAppearance();
        public InteractiveFieldOptions options = new InteractiveFieldOptions();
        public InteractiveFieldActions actions = new InteractiveFieldActions();

        public InteractiveFieldCustomProperties custom_meta_data = new InteractiveFieldCustomProperties();

        public TextFormatting formatting = new TextFormatting();

        public override BaseTagProperty Clone()
        {
            InteractiveFieldFormating newClone = new InteractiveFieldFormating();
            newClone.version = this.version;
            newClone.type = this.type;
            newClone.general = this.general.Clone();
            newClone.appearance = appearance.Clone();
            newClone.options = this.options.Clone();
            newClone.actions = this.actions.Clone();
            newClone.custom_meta_data = this.custom_meta_data.Clone();
            newClone.formatting = (TextFormatting)this.formatting.Clone();
            return newClone;
        }
    }

    public class InteractiveFieldGeneral
    {
        public string name;
        public string alias;
        public string description;
        public int tab_order = 0;
        public int submit_action = 1;

        public InteractiveFieldGeneral Clone()
        {
            InteractiveFieldGeneral newClone = new InteractiveFieldGeneral();
            newClone.name = this.name;
            newClone.alias = this.alias;
            newClone.description = this.description;
            newClone.tab_order = tab_order;
            newClone.submit_action = this.submit_action;
            return newClone;
        }
    }

    public class InteractiveFieldAppearance
    {
        public InteractiveFieldCommonProperties common_properties = new InteractiveFieldCommonProperties();
        public InteractiveFieldBordersAndColors borders_and_colors = new InteractiveFieldBordersAndColors();
        public InteractiveFieldText text = new InteractiveFieldText();
        public bool show_reset_button = true;

        public InteractiveFieldAppearance Clone()
        {
            InteractiveFieldAppearance newClone = new InteractiveFieldAppearance();
            newClone.common_properties = this.common_properties.Clone();
            newClone.borders_and_colors = this.borders_and_colors.Clone();
            newClone.text = this.text.Clone();
            newClone.show_reset_button = this.show_reset_button;
            return newClone;
        }
    }

    public class InteractiveFieldAppearanceResetButton
    {
        public InteractiveFieldBordersAndColors borders_and_colors = new InteractiveFieldBordersAndColors();
        public InteractiveFieldText text = new InteractiveFieldText();

        public InteractiveFieldAppearanceResetButton Clone()
        {
            InteractiveFieldAppearanceResetButton newClone = new InteractiveFieldAppearanceResetButton();
            newClone.borders_and_colors = this.borders_and_colors.Clone();
            newClone.text = this.text.Clone();
            return newClone;
        }
    }

    public class DateTimePickerOptions
    {
        public string time_format = "24";
        public bool allow_edit_time = false;

        public DateTimePickerOptions Clone()
        {
            return new DateTimePickerOptions() { allow_edit_time = this.allow_edit_time, time_format = this.time_format };
        }
    }


    public class InteractiveFieldOptions
    {
        public InteractiveAlignment alignment = InteractiveAlignment.left;
        public InteractiveFieldDefaultValue default_value = new InteractiveFieldDefaultValue();
        public int limit_of_characters = 0;
        public bool allow_user_to_enter_text = false;
        public InteractiveFieldItemsList item_list = new InteractiveFieldItemsList();
        public bool multiple_selection = false;
        public InteractiveShapeStyle radiobutton_style = InteractiveShapeStyle.circle;
        public string radiobutton_group = string.Empty;
        public bool checked_by_default = false;
        public InteractiveShapeStyle checkbox_style = InteractiveShapeStyle.check;
        public string checkbox_export_value = string.Empty;
        public InteractiveButtonLayout button_layout = InteractiveButtonLayout.label;
        public string button_icon = string.Empty;
        public string button_label = string.Empty;
        public bool multi_line = false;
        public bool mandatory_to_fill = false;
        public bool show_popup = false;
        public DateTimePickerOptions datetimepicker = new DateTimePickerOptions();
        [XmlElement("upload_file")]
        public InteractiveFieldsFileUploadButton fileUploadButton = new InteractiveFieldsFileUploadButton();
        public InteractiveFieldsSignatureButton reset_button = new InteractiveFieldsSignatureButton();
        public InteractiveFieldsSignatureButton save_button = new InteractiveFieldsSignatureButton();
        public InteractiveFieldsSignatureButton close_button = new InteractiveFieldsSignatureButton();

        [XmlElement("uploadEmbeddedImage")]
        public InteractiveFieldsUploadEmbeddedImage uploadEmbeddedImage = new InteractiveFieldsUploadEmbeddedImage();


        public InteractiveFieldOptions Clone()
        {
            InteractiveFieldOptions newClone = new InteractiveFieldOptions();
            newClone.alignment = this.alignment;
            newClone.default_value = this.default_value.Clone();
            newClone.limit_of_characters = this.limit_of_characters;
            newClone.allow_user_to_enter_text = this.allow_user_to_enter_text;
            newClone.item_list = this.item_list.Clone();
            newClone.multiple_selection = this.multiple_selection;
            newClone.radiobutton_style = this.radiobutton_style;
            newClone.radiobutton_group = this.radiobutton_group;
            newClone.checked_by_default = this.checked_by_default;
            newClone.checkbox_style = this.checkbox_style;
            newClone.checkbox_export_value = this.checkbox_export_value;
            newClone.button_layout = this.button_layout;
            newClone.button_icon = this.button_icon;
            newClone.button_label = this.button_label;
            newClone.multi_line = this.multi_line;
            newClone.mandatory_to_fill = this.mandatory_to_fill;
            newClone.fileUploadButton = this.fileUploadButton.Clone();
            newClone.reset_button = this.reset_button.Clone();
            newClone.save_button = this.save_button.Clone();
            newClone.close_button = this.close_button.Clone();
            newClone.show_popup = this.show_popup;
            newClone.datetimepicker = this.datetimepicker.Clone();
            newClone.uploadEmbeddedImage = this.uploadEmbeddedImage.Clone();
            return newClone;
        }
    }

    public class InteractiveFieldsUploadEmbeddedImage
    {
        public uint max_size = 2000;
        public string max_size_error_text = string.Empty;

        public InteractiveFieldsUploadEmbeddedImageButton reset_button = new InteractiveFieldsUploadEmbeddedImageButton();

        public InteractiveFieldsUploadEmbeddedImage Clone()
        {
            InteractiveFieldsUploadEmbeddedImage newClone = new Data.InteractiveFieldsUploadEmbeddedImage();
            newClone.max_size = this.max_size;
            newClone.max_size_error_text = this.max_size_error_text;
            newClone.reset_button = this.reset_button.Clone();
            return newClone;
        }
    }

    public class InteractiveFieldsUploadEmbeddedImageButton
    {
        public InteractiveFieldAppearanceResetButton appearance = new InteractiveFieldAppearanceResetButton();
        public InteractiveFieldOptionsResetButton options = new InteractiveFieldOptionsResetButton();

        public InteractiveFieldsUploadEmbeddedImageButton Clone()
        {
            InteractiveFieldsUploadEmbeddedImageButton newClone = new InteractiveFieldsUploadEmbeddedImageButton();
            newClone.appearance = appearance.Clone();
            newClone.options = this.options.Clone();
            return newClone;
        }
    }

    public class InteractiveFieldOptionsResetButton
    {
        public InteractivePosition position = InteractivePosition.bottomRight;
        public string button_label = string.Empty;

        public InteractiveFieldOptionsResetButton Clone()
        {
            InteractiveFieldOptionsResetButton newClone = new InteractiveFieldOptionsResetButton();
            newClone.button_label = this.button_label;
            newClone.position = this.position;

            return newClone;
        }
    }

    public class InteractiveFieldDefaultValue
    {
        public InteractiveDefaultValueType type = InteractiveDefaultValueType.ThisTag;
        public string value = string.Empty;

        public InteractiveFieldDefaultValue Clone()
        {
            InteractiveFieldDefaultValue newClone = new InteractiveFieldDefaultValue();
            newClone.type = this.type;
            newClone.value = this.value;

            return newClone;
        }
    }

 
    public class InteractiveFieldsFileUploadButton
    {
        public uint max_size = 2000;
        public InteractiveFileUploadButtonFileTypes type = InteractiveFileUploadButtonFileTypes.Image;
        public bool embedded = false;
        public uint max_files = 1;
        public string max_files_error_text = string.Empty;
        public string max_size_error_text = string.Empty;
        public status status = new status();
        
        public InteractiveFieldsFileUploadButton Clone()
        {
            InteractiveFieldsFileUploadButton newClone = new InteractiveFieldsFileUploadButton();
            newClone.status = this.status.Clone();
            newClone.max_size = this.max_size;
            newClone.type = this.type;
            newClone.embedded = this.embedded;
            newClone.max_files = this.max_files;
            newClone.max_files_error_text = this.max_files_error_text;
            newClone.max_size_error_text = this.max_size_error_text;
            return newClone;
        }
    }

    public class status
    {
        public bool show = true;
        public string file_name_text = string.Empty;
        //public InteractiveTextDirection text_direction = InteractiveTextDirection.ltr;
        public bool show_progress_bar = true;

        public status Clone()
        {
            status newClone = new status();
            newClone.file_name_text = this.file_name_text;
            newClone.show_progress_bar = this.show_progress_bar;
            newClone.show = this.show;
            //newClone.text_direction = this.text_direction;
            return newClone;
        }

    }

    public class InteractiveFieldsSignatureButton
    {
        public InteractiveFieldAppearanceResetButton appearance = new InteractiveFieldAppearanceResetButton();
        public InteractiveFieldOptionsResetButton options = new InteractiveFieldOptionsResetButton();

        public InteractiveFieldsSignatureButton Clone()
        {
            InteractiveFieldsSignatureButton newClone = new InteractiveFieldsSignatureButton();
            newClone.appearance = appearance.Clone();
            newClone.options = this.options.Clone();
            return newClone;
        }


    }

    public class InteractiveFieldCustomProperties
    {
        [XmlElement("custom_prop")]
        public List<InteractiveFieldCustomProperty> CustomProp { get; set; }

        public InteractiveFieldCustomProperties Clone()
        {
            InteractiveFieldCustomProperties newClone = new InteractiveFieldCustomProperties();
            if (this.CustomProp != null && this.CustomProp.Count > 0)
            {
                newClone.CustomProp = new List<InteractiveFieldCustomProperty>(this.CustomProp.Count);
                this.CustomProp.ForEach(item => { newClone.CustomProp.Add(item.Clone()); });
            }

            return newClone;
        }
    }

    public class InteractiveFieldCustomProperty
    {
        private string name;
        [XmlElement("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private string value;
        [XmlElement("value")]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public InteractiveFieldCustomProperty Clone()
        {
            InteractiveFieldCustomProperty newClone = new InteractiveFieldCustomProperty();
            newClone.value = this.value;
            newClone.name = this.name;

            return newClone;
        }

    }

    public class InteractiveFieldItemsList
    {
        [XmlIgnore]
        public bool itemSpecified
        {
            get
            {
                if (this.item != null && this.item.Count == 0)
                {
                    this.item = null;
                }
                return this.item != null;
            }
        }

        [XmlElement("item")]
        public List<InteractiveFieldItem> item { get; set; }

        public InteractiveFieldItemsList Clone()
        {
            InteractiveFieldItemsList newClone = new InteractiveFieldItemsList();
            if (this.item != null && this.item.Count > 0)
            {
                newClone.item = new List<InteractiveFieldItem>(this.item.Count);
                this.item.ForEach(item => { newClone.item.Add(item.Clone()); });   
            }

            return newClone;
        }
    }

    public class InteractiveFieldItem
    {
        private string value;
        private string export_value;

        public InteractiveFieldItem(string value, string export_value)
        {
            this.value = value;
            this.export_value = export_value;
        }

        public InteractiveFieldItem()
        {

        }

        [XmlElement("value")]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        [XmlElement("export_value")]
        public string Export_value
        {
            get { return export_value; }
            set { export_value = value; }
        }

        public InteractiveFieldItem Clone()
        {
            InteractiveFieldItem newClone = new InteractiveFieldItem();
            newClone.value = this.value;
            newClone.export_value = this.export_value;

            return newClone;
        }
    }

    public class InteractiveFieldCommonProperties
    {
        public InteractiveFormField form_field = InteractiveFormField.visible;
        public InteractiveOrientation orientation = InteractiveOrientation.orientation_0;
        public bool read_only = false;
        public int lock_on_step = 1;

        public InteractiveFieldCommonProperties Clone()
        {
            InteractiveFieldCommonProperties newClone = new InteractiveFieldCommonProperties();
            newClone.form_field = this.form_field;
            newClone.orientation = this.orientation;
            newClone.read_only = this.read_only;
            newClone.lock_on_step = this.lock_on_step;
            return newClone;
        }
    }

    public class InteractiveFieldBordersAndColors
    {
        public string border_color = "black";
        public string fill_color = "none";
        public string line_thickness = InteractiveLineThickness.thin.ToString();
        public string line_style = InteractiveLineStyle.solid.ToString();

        public InteractiveFieldBordersAndColors Clone()
        {
            InteractiveFieldBordersAndColors newClone = new InteractiveFieldBordersAndColors();
            newClone.border_color = this.border_color;
            newClone.fill_color = this.fill_color;
            newClone.line_thickness = this.line_thickness;
            newClone.line_style = this.line_style;

            return newClone;
        }
    }

    public class InteractiveFieldText
    {
        public float font_size = 0;
        public string text_color = "black";
        public string font_name = "Times New Roman";
        public InteractiveTextDirection direction = InteractiveTextDirection.auto;

        public InteractiveFieldText Clone()
        {
            InteractiveFieldText newClone = new InteractiveFieldText();
            newClone.font_size = this.font_size;
            newClone.text_color = this.text_color;
            newClone.font_name = this.font_name;
            newClone.direction = this.direction;

            return newClone;
        }
    }

    public class InteractiveFieldActions
    {
        [XmlIgnore]
        public bool actionSpecified
        {
            get
            {
                if (this.action != null && this.action.Count == 0)
                {
                    this.action = null;
                }
                return this.action != null;
            }
        }

        [XmlElement("action")]
        public List<InteractiveFieldAction> action;

        public InteractiveFieldActions Clone()
        {
            InteractiveFieldActions newClone = new InteractiveFieldActions();
            if (this.action != null && this.action.Count > 0)
            {
                newClone.action = new List<InteractiveFieldAction>(this.action.Count);
                this.action.ForEach(item => { newClone.action.Add(item.Clone()); });
            }

            return newClone;
        }
    }

    public class InteractiveFieldAction
    {
        public ScriptEvents event_name;
        public InteractiveScript script;

        public InteractiveFieldAction Clone()
        {
            InteractiveFieldAction newClone = new InteractiveFieldAction();
            newClone.event_name = this.event_name;
            newClone.script = this.script.Clone();

            return newClone;
        }
    }

    public class InteractiveScript
    {
        public string name;
        public string description;
        public string value;
        public bool predefined = false;
        public JavaScripts.scriptType scriptType;
        public InteractiveScriptData scriptData;
        
        public InteractiveScript Clone()
        {
            InteractiveScript newClone = new InteractiveScript();
            newClone.name = this.name;
            newClone.description = this.description;
            newClone.value = this.value;
            newClone.scriptData = this.scriptData;
            newClone.scriptType = this.scriptType;
            newClone.predefined = this.predefined;

            return newClone;
        }
    }
}
