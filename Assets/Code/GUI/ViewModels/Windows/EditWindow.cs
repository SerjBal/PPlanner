using System;
using SerjBal.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class EditWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            onAccept += ()=> menuItem.ChangeKey(InputField.text);
            InputField.text = menuItem.Key;
            base.Initialize(menuItem.Parent);
        }
    }
}