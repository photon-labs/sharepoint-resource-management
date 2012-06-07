using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phresco.XMLtoList
{
    public class Constants
    {
        public const string CONFIG_PATH = @"lists.xml";
        public const string DROPDOWNMENU = "dropdownmenu";
        public const string RADIOBUTTONS = "radiobuttons";
        public const string MULTICHECKBOX = "multicheckbox";
        public const string RICHTEXT = "rich text";
        public const string YES = "yes";
        public const string LIST_NAME_ERROR = "'name' Attribute not specified in List Element.";
        public const string FIELD_NAME_ERROR = "'name' Attribute not specified in Fields Element in the List ";
        public const string SINGLE_LINE_TEXT_TYPE = "single line of text";
        public const string MULTIPLE_LINE_TEXT_TYPE = "multiple lines of text";
        public const string DATETIME_TYPE = "datetime";
        public const string NOTES_TYPE = "notes";
        public const string NUMBER_TYPE = "number";
        public const string CURRENCY_TYPE = "currency";
        public const string YESNO_TYPE = "yesno";
        public const string PERSON_GROUP_TYPE = "person or group";
        public const string HYPERLINK_PICTURE_TYPE = "hyperlink or picture";
        public const string CHOICE_TYPE = "choice";
        public const string LOOKUP_TYPE = "lookup";

        public const string GROUP_XML_FILENAME = @"Groups.xml";
        public const string GROUP = "Group";
        public const string NAME = "Name";
        public const string DESCRIPTION = "Description";
        public const string PERMISSIONLEVEL = "PermissionLevel";
    }
}
