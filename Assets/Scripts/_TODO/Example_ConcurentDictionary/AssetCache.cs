using Cysharp.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;

namespace ExampleConcurrentDictionary
{
    public interface IAssetProcessor
    {
        void ProcessAssetData(string path, byte[] data);
    }

    public interface IAssetDataLoader
    {
        byte[] Load(string path);
    }

    public interface IAssetFactory
    {
        Object Create(string path, byte[] data);
    }
    public sealed class AssetWrapper
    {
        public readonly string Path;
        public readonly byte[] RawData;

        public AssetWrapper(string path, byte[] data)
        {
            Path = path;
            RawData = data;
        }
    }

    public class SomeAssetCache
    {
        private readonly ConcurrentDictionary<string, AssetWrapper> _cache = new();
        private readonly IAssetProcessor _processor;
        private readonly IAssetDataLoader _dataLoader;

        public SomeAssetCache(IAssetProcessor processor, IAssetDataLoader dataLoader)
        {
            _processor = processor;
            _dataLoader = dataLoader;
        }

        public async UniTask LoadAssetsAsync(string[] assetPaths, CancellationToken token)
        {
            var tasks = new UniTask[assetPaths.Length];

            for (var i = 0; i < assetPaths.Length; i++)
            {
                string path = assetPaths[i];
                tasks[i] = UniTask.RunOnThreadPool(() =>
                {
                    token.ThrowIfCancellationRequested();
                    var data = _dataLoader.Load(path);
                    var wrapper = new AssetWrapper(path, data);
                    _cache.TryAdd(path, wrapper);
                    _processor.ProcessAssetData(path, data);
                });
            }

            await UniTask.WhenAll(tasks);
        }

        public AssetWrapper TryGetFromCache(string path)
        {
            _cache.TryGetValue(path, out var wrapper);
            return wrapper;
        }

        public int GetCacheSize() => _cache.Count;
    }

}
