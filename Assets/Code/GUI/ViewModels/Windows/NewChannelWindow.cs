using System.Collections.Generic;
using SerjBal.Windows;
using UnityEngine;

namespace SerjBal
{
    public class NewChannelWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            InputField.text = "New Channel";
            base.Initialize(menuItem);
        }
    }
}