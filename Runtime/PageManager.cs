using System.Collections.Generic;
using Game.UI;
using UnityEngine;

namespace Sources.Utility
{
    public class PageManager : MonoBehaviour
    {
        private Dictionary<string, IPage> _pages;
        private static PageManager _instance;

        private void Awake()
        {
            _instance = this;
            CollectPages();
        }

        private void CollectPages()
        {
            _pages = new Dictionary<string, IPage>();
            var pages = GetComponentsInChildren<IPage>();
            foreach (var page in pages)
            {
                _pages.Add(page.Name, page);
                page.Hide();
            }
        }

        public static T GetPage<T>(string pageName) where T : Component
        {
            return (T) GetPage(pageName);
        }

        public static IPage GetPage(string pageName)
        {
            if (_instance._pages.TryGetValue(pageName, out var page))
            {
                return page;
            }

            Debug.LogError($"Missing page!! name:{pageName}");
            return null;
        }

        public static void HideAllPages()
        {
            foreach (var pagePair in _instance._pages)
            {
                pagePair.Value.Hide();
            }
        }
    }
}