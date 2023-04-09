using System.IO;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class SearchResultButtonView : ButtonView
    {
        private protected override string GetName(string path) => 
            nameText.text =  Path.GetRelativePath(Const.DataPath, path);
        
    }
}