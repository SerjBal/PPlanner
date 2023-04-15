using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ButtonView : MonoBehaviour, IView
    {
        public Canvas canvas;
        public SwipeController controller;
        public Button editButton;
        public Button removeButton;
        public TMP_Text nameText;
        public Transform contentContainer;
        public ButtonAnimator animator;
    }
}