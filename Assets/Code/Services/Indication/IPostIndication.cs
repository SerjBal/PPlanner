using System;
using System.Collections.Generic;

namespace SerjBal.Indication
{
    public interface IPostIndication : IService
    {
        void Initialize(DateTime dateTime);
        List<PostState> GetPostsStates(string path);
        void SavePostType(string path, PostType state);
    }
}