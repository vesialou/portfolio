using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace ExampleSemaphoreSlim
{
    public interface IAnalyticsSender
    {
        UniTask SendAsync(string data);
    }

    public interface IJsonSerializer
    {
        string Serialize<T>(T obj);
    }

    public class AnalyticsService
    {
        public static AnalyticsService Instance { get; private set; }

        private readonly SemaphoreSlim _semaphore = new(2, 2);
        private readonly IAnalyticsSender _sender;
        private readonly IJsonSerializer _serializer;


        public AnalyticsService(IAnalyticsSender sender, IJsonSerializer serializer)
        {
            if (Instance != null)
                throw new InvalidOperationException("AnalyticsService already initialized");

            Instance = this;
            _sender = sender;
            _serializer = serializer;
        }

        public void TrackLevelStart(int level)
        {
            _ = TrackInternal("level_start", new { level });
        }

        public void TrackLevelComplete(int level, float time)
        {
            _ = TrackInternal("level_complete", new { level, time });
        }

        public void TrackPurchase(string itemId, decimal price)
        {
            _ = TrackInternal("purchase", new { itemId, price });
        }


        private async UniTask TrackInternal(string eventName, object data)
        {
            await _semaphore.WaitAsync();

            try
            {
                var payload = new
                {
                    eventName,
                    data,
                    timestamp = DateTime.UtcNow.ToString("O"),
                    sessionId = SystemInfo.deviceUniqueIdentifier
                };

                string json = _serializer.Serialize(payload);

                await _sender.SendAsync(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"AnalyticsService: Error sending {eventName} — {ex}");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}