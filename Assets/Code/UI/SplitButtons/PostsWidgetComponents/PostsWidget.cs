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
        private SplitButtonPresenter _splitButtonPresenter;
        private DateTime _time;
        private IDataProvider _data;
        private IPostIndicator _indicator;
        private string _path;
        private readonly int _minutesPerDay = 1440;
        private IndicatorsConfig _config;
        private float _currentMinute;
        private ISettingsProvider _settings;

        public void Initialize(Services services, SplitButtonPresenter splitButtonPresenter, IndicatorsConfig config)
        {
            _settings = services.Single<ISettingsProvider>();
            _indicator = services.Single<IPostIndicator>();
            _splitButtonPresenter = splitButtonPresenter;
            _config = config;
            _path = Path.Combine(_splitButtonPresenter.Path, Const.ContentDirectory);
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
            timeProgress.color = _settings.LoadColorSettings(ColorSettingType.TimeProgress);
        }

        private void UpdatePostsIndicator()
        {
            postsContainer.Clear();
            var postsList = _indicator.GetPostsStates(_path);
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
                        ? _settings.LoadColorSettings(ColorSettingType.ContentUndone)
                        : _settings.LoadColorSettings(ColorSettingType.ContentDone);
                    ;
                    break;
                case PostType.Ads:
                    indicatorView.color = isTime
                        ? _settings.LoadColorSettings(ColorSettingType.AdsUndone)
                        : _settings.LoadColorSettings(ColorSettingType.AdsDone);
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
