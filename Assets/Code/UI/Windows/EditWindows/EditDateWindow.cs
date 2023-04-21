using System;
using System.Collections.Generic;
using System.IO;
using SerjBal.Windows;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SerjBal
{
    public class EditDateWindow : EditWindow
    {
        public Action<int> OnMonthChanged { get; set; }
        private int _month;
        public int Month
        {
            get => _month;
            set
            {
                _month = value;
                OnMonthChanged?.Invoke(value);
            }
        }

        public override void Initialize(IHierarchical splitButton, Services services, WindowView view)
        {
            CheckCmd = new DateCheckNameCmd(this, splitButton.Parent.Path, services);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new DateOverrideCmd(this, services, splitButton);
            FormatCmd = new DataFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditDateWindowFormatText;
            AcceptButtonText = Const.EditDateWindowButtonText;

            SetInput(splitButton);
            InitializeView(view);
            InitializeDateEditView(view);
        }

        private void InitializeDateEditView(WindowView view)
        {
            var dateView = (DateWindowView)view;
            dateView.typeDropdown.options = GetMonthOptions();
            dateView.typeDropdown.value = Month;
            dateView.typeDropdown.onValueChanged.AddListener(delegate { Month = dateView.typeDropdown.value; });
            OnMonthChanged = value => dateView.typeDropdown.value = value;
        }
        
        private List<TMP_Dropdown.OptionData> GetMonthOptions()
        {
            var options = new List<TMP_Dropdown.OptionData>();
            
            for (int i = 0; i < 12; i++) 
                options.Add(new TMP_Dropdown.OptionData { text = Const.MonthEnglishNames[i] });
            return options;
        }
        
        private void SetInput(IHierarchical splitButton)
        {
            var split = Path.GetFileName(splitButton.Path).Split('-');
            Month = int.Parse(split[1]);
            var day = int.Parse(split[2]);
            InputString = day.ToString("D2");
        }
    }
}