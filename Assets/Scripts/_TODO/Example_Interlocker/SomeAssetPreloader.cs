using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ExampleInterlocker
{
    public interface IAssetLoader
    {
        UniTask LoadAsync(string assetName, CancellationToken token);
    }
    public interface IPreloadProgressReceiver
    {
        void OnProgressUpdated(float progress);
        void OnAllAssetsLoaded();
    }
    public class SomeAssetPreloader
    {
        public static SomeAssetPreloader Instance { get; private set; }

        public event Action<float> OnProgressUpdated;
        public event Action OnAllAssetsLoaded;

        private readonly IAssetLoader _assetLoader;
        private int _totalAssets = 0;
        private int _loadedAssets = 0;

        public SomeAssetPreloader(IAssetLoader assetLoader)
        {
            Instance = this;
            _assetLoader = assetLoader;
        }

        public async UniTask PreloadAllAsync(CancellationToken token)
        {
            _totalAssets = 6;
            _loadedAssets = 0;
            ReportProgress(0f);

            try
            {
                await UniTask.WhenAll(
                    LoadAndTrackAsync("hero_texture", token),
                    LoadAndTrackAsync("enemy_texture", token),
                    LoadAndTrackAsync("config_json", token),
                    LoadAndTrackAsync("levels_json", token),
                    LoadAndTrackAsync("bullet_prefab", token),
                    LoadAndTrackAsync("explosion_prefab", token)
                );

                ReportProgress(1f);
                OnAllAssetsLoaded?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Preload failed: {ex.Message}");
            }
        }

        private async UniTask LoadAndTrackAsync(string assetName, CancellationToken token)
        {
            await _assetLoader.LoadAsync(assetName, token);

            int currentLoaded = Interlocked.Increment(ref _loadedAssets);
            float progress = (float)currentLoaded / _totalAssets;

            UniTask.Post(() => ReportProgress(progress), PlayerLoopTiming.Update);

            token.ThrowIfCancellationRequested();
        }

        private void ReportProgress(float progress)
        {
            OnProgressUpdated?.Invoke(progress);
            if (progress >= 1f)
            {
                OnAllAssetsLoaded?.Invoke();
            }
        }
    }
}