
using System.Collections.Generic;
using SerjBal.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerjBal
{
    public class NewPostWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            InputField.text = "0:0";
            base.Initialize(menuItem);
        }
    }
}

