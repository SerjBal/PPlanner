using UnityEngine;

namespace SerjBal
{
    public struct PostState
    {
        public int minute;
        public PostType postType;
    }
    
    public enum PostType
    {
        Content, Ads
    }
}