using UnityEngine;

namespace SerjBal
{
    public static class Const
    {
        public const string GUITag = "GUI";

        public const string AddItemButtonPath = "add-item-button";
        public const string GUIPath = "canvas";
        public const string DateItemPath = "date-item";
        public const string ChannelItemPath = "channel-item";
        public const string TimeItemPath = "time-item";
        public const string TextItemPath = "text-item";
        public const string SearchResultItemPath = "search-item";
        public const string ItemNamingWindowPath = "window-generic";

        public const string NewChannelWindowFormatText = "Channel Name";
        public const string NewChannelWindowButtonText = "Create";
        public const string ChannelDefaultKey = "New Channel";

        public const string NewTimeWindowFormatText = "Post at hh:mm";
        public const string NewTimeWindowButtonText = "Create";
        public const string TimeDefaultKey = "00:00";

        public const string EditDateWindowFormatText = "Day.Month.Year";
        public const string EditDateWindowButtonText = "Change Date";

        public const string EditChannelWindowFormatText = "Change Name";
        public const string EditChannelWindowButtonText = "Ok";

        public const string EditTimeWindowFormatText = "Change Time";
        public const string EditTimeWindowButtonText = "Ok";

        public const string WarningWindowPath = "warning-window";
        public const string ReplaceWarningText = "The data already exists! Replace?";
        public const string ReplaceButtonText = "Ok";

        public const string CommentsButtonPath = "comments-item";

        public const int MenuWindowSortingOrder = 10;
        public const int WarningWindowSortingOrder = 11;
       
        public static readonly string DataPath = Application.persistentDataPath;
        public const string TextFileName = "text.json";
        public const string CommentsName = "Comments";
        public const string ContentDrectory = "content";
    }
}