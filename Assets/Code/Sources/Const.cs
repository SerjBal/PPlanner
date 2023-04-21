using UnityEngine;

namespace SerjBal
{
    public static class Const
    {
        public static readonly string DataPath = Application.persistentDataPath;
        public const int MenuWindowSortingOrder = 10;
        public const int WarningWindowSortingOrder = 11;
        public const string TextFileName = "text.json";
        public const string TypeFileName = "state.json";
        
        public const string GUITag = "GUI";

        public const string AddItemButtonPath = "add-item-button";
        public const string GUIPath = "canvas";
        public const string DateItemPath = "date-button";
        public const string ChannelItemPath = "channel-button";
        public const string TimeItemPath = "time-button";
        public const string TextItemPath = "text-item";
        public const string SearchResultItemPath = "searchResult-button";
        public const string CommentsButtonPath = "comments-button";
        public const string TemplatesItemPath = "templates-button";
        public const string TemplateItemPath = "template-button";
        public const string ColorSettingsButtonPath = "colorsSettings-button";

        public const string NewChannelWindowFormatText = "Channel Name";
        public const string NewChannelWindowButtonText = "Create";
        public const string ChannelDefaultKey = "New Channel";

        public const string NewTimeWindowFormatText = "Post at hh:mm";
        public const string NewTimeWindowButtonText = "Create";
        public const string TimeDefaultKey = "00:00";

        public const string EditDateWindowFormatText = "Day.Month.Year";
        public const string EditDateWindowButtonText = "Change Date";

        public const string EditChannelWindowFormatText = "Change Name";

        public const string EditTimeWindowFormatText = "Change Time";
        public const string EditWindowButtonText = "Ok";

        public const string NewTemplateWindowFormatText = "Enter template name";
        public const string EditTemplateWindowFormatText = "Change Name";

        public const string EditDateWindowPath = "dateEdit";
        public const string ItemNamingWindowPath = "window-generic";
        public const string WarningWindowPath = "warning";
        public const string EditTimeWindowPath = "timeEdit";
        public const string ColorSelectorWindowPath = "colorSelector";
        public const string ReplaceWarningText = "The data already exists! Replace?";
        
        public const string CommentsName = "Comments";
        public const string DefaultTemplateName = "New template";
        public const string TemplatesButtonName = "Templates";

        public const string ContentDirectory = "Content";
        public const string TemplatesDirectory = "Templates";

        public static readonly string[] MonthEnglishNames = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        
        public const string SettingsDirectoryName = "Settings";
        public const string PostsColorFileName = "posts";
        public const string ColorsSettingsFile = "colors.json";
        public const string ColosSettingsName = "Colors settings";
        public const string EditColorWindowFormatText = "Select a color";
    }
}