using System;
using System.Collections.Generic;

namespace SerjBal.Indication
{
    public interface IPostIndication : IService
    {
        bool IsUpdateable { get; }
        void Initialize(DateTime dateTime);
        List<PostState> GetPostsStates(string path);
        void CreateDefaultState(string path);
    }
}