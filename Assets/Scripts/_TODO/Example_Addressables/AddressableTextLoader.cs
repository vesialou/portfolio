using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ExampleAddressables
{
    public class AddressableTextLoader
    {
        private AsyncOperationHandle<TextAsset>? currentHandle;

        public async UniTask<string> LoadTextAsync(string address, CancellationToken cancellationToken)
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(address);
            currentHandle = handle;

            try
            {
                await handle.ToUniTask(cancellationToken: cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("[AddressableTextLoader] Загрузка отменена");
                return null;
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result.text;
            }

            Debug.LogError($"[Addressables] Не удалось загрузить текст по адресу: {address}");
            return null;
        }

        public void Release()
        {
            if (currentHandle.HasValue)
            {
                Addressables.Release(currentHandle.Value);
                currentHandle = null;
            }
        }
    }
}
