using UnityEngine;

namespace ExampleSemaphoreSlim
{
    public class SomeGameUI : MonoBehaviour
    {
        [SerializeField] private int currentLevel = 1;
        private float levelStartTime;

        public void OnPlayButtonPressed()
        {
            levelStartTime = Time.time;
            AnalyticsService.Instance.TrackLevelStart(currentLevel);

            // Start level...
        }

        public void OnLevelCompleteButtonPressed()
        {
            var completionTime = Time.time - levelStartTime;
            AnalyticsService.Instance.TrackLevelComplete(currentLevel, completionTime);

            // Next level transition...
            currentLevel++;
        }

        public void OnBuyItemButtonPressed()
        {
            AnalyticsService.Instance.TrackPurchase("item0001", 0.99m);

            // Purchase logic...
        }
    }
}