using System;
using System.Collections.Generic;
using System.IO;
using SerjBal.Indication;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class PostsWidget : MonoBehaviour
    {
        [SerializeField] private Image timeProgress;
        [SerializeField] private RectTransform postsContainer;
        [SerializeField] private GameObject postIndicator;
        private ButtonViewModel _buttonViewModel;
        private DateTime _time;
        private IDataProvider _data;
        private IPostIndication _indication;
        private string _path;
        private readonly int _minutesPerDay = 1440;
        private IndicatorsConfig _config;
        private float _currentMinute;

        public void Initialize(Services services, ButtonViewModel buttonViewModel, IndicatorsConfig config)
        {
            _indication = services.Single<IPostIndication>();
            _buttonViewModel = buttonViewModel;
            _config = config;
            _path = Path.Combine(_buttonViewModel.Path, Const.ContentDrectory);
        }

        private void Update()
        {
            var now = DateTime.Now;
            if (now.Minute != _time.Minute)
            {
                _time = now;
                UpdateView();
            }
        }

        public void UpdateView()
        {
            UpdateTimeIndicator();
            UpdatePostsIndicator();
        }

        private void UpdateTimeIndicator()
        {
            var currentTime = DateTime.Now;
            _currentMinute = currentTime.Minute + currentTime.Hour * 60f;
            float progress =  1f/(_minutesPerDay/_currentMinute);
            timeProgress.fillAmount = progress;
            timeProgress.color = _config.timeProgressBarColor;
        }

        private void UpdatePostsIndicator()
        {
            postsContainer.Clear();
            var postsList = _indication.GetPostsStates(_path);
            foreach (var state in postsList)
            {
                var indicatorGO = Instantiate(postIndicator, postsContainer);
                var indicatorView = indicatorGO.GetComponent<PostIndicator>();
                
                SetPosition(indicatorView.rect, state.minute);
                SetState(indicatorView.image, state);
            }
        }

        private void SetState(Image indicatorView, PostState postState)
        {
            var state = postState.postType;
            bool isTime = postState.minute > _currentMinute;
            switch (state)
            {
                case PostType.Content:
                    indicatorView.color = isTime 
                        ? _config.contentPostActiveColor
                        :_config.contentPostInactiveColor;
                    break;
                case PostType.Ads:
                    indicatorView.color = isTime 
                        ? _config.adsPostActiveColor
                        :_config.adsPostInactiveColor;
                    break;
                default:
                    Debug.LogError("Unknown postType");
                    break;
            }
        }

        private void SetPosition(RectTransform rect, int minute)
        {
            var minuteUnit = postsContainer.rect.width / _minutesPerDay;
                
            var rectAnchoredPosition = rect.anchoredPosition;
            rectAnchoredPosition.x = minuteUnit * minute;
            rect.anchoredPosition = rectAnchoredPosition;
        }
    }
}
