using UnityEngine;

namespace SerjBal.Code.Sources
{
    public static class Const{
        public static readonly string DataPath = Application.persistentDataPath + "/data.json";
        
        public const string GUIPath = "canvas";
        public const string DateItemPath = "date-item";
        public const string ChannelItemPath = "channel-item";
        public const string TimeItemPath = "time-item";
        public const string TextItemPath = "text-item";

        public const string EditWindowPath = "edit-window";
        public const string EditDateWindowFormatText = "Day.Month.Year";
        public const string EditDateWindowButtonText = "Change Date";
        
        public const string NewChannelWindowPath = "new-channel-window";
        public const string NewChannelWindowFormatText = "Channel Name";
        public const string NewChannelWindowButtonText = "Create";

        public const string NewPostWindowPath = "new-post-window";
        public const string NewPostWindowFormatText = "Post at hh:mm";
        public const string NewPostlWindowButtonText = "Create";
    };
}