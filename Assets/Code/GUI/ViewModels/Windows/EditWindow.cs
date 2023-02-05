using System;
using SerjBal.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class EditWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItemViewModel menuItem)
        {
            InputField.text = menuItem.Key;
        }
    }
}