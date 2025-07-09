using Cysharp.Threading.Tasks;
using System;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExampleAddressables
{
    public class HarryPotterLoader : MonoBehaviour
    {
        [Header("Addressables")]
        [SerializeField] private string address = "HarryPotter_Chapter1";

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI textDisplay;
        [SerializeField] private Button startReadingButton;
        [SerializeField] private GameObject readingIntroPanel;
        [SerializeField] private GameObject scrollViewText; 
        [SerializeField] private GameObject loadingTextObject;

        private AddressableTextLoader loader;
        private TextDisplayer displayer;
        private CancellationTokenSource cts;

        private void Awake()
        {
            loader = new AddressableTextLoader();
            displayer = new TextDisplayer(textDisplay);
            cts = new CancellationTokenSource();

            textDisplay.text = "";
            scrollViewText.SetActive(false);
            readingIntroPanel.SetActive(true);

            startReadingButton.onClick.AddListener(OnStartReadingClicked);
        }

        private async void OnStartReadingClicked()
        {
            ToggleLoading(true);
            startReadingButton.interactable = false;

            readingIntroPanel.SetActive(false);
            scrollViewText.SetActive(true);

            try
            {
                string text = await loader.LoadTextAsync(address, cts.Token);
                await UniTask.Yield();
                if (text != null)
                {
                    displayer.Display(text);
                }
            }
            catch (System.Exception ex)
            {
                textDisplay.text = "[Error loading text]";
                Debug.LogError(ex);
            }
            finally
            {
                ToggleLoading(false);
            }
        }

        private void ToggleLoading(bool isLoading)
        {
            if (loadingTextObject != null)
            {
                loadingTextObject.SetActive(isLoading);
            }
        }

        private void OnDestroy()
        {
            cts.Cancel();
            cts.Dispose();

            loader?.Release();
        }
    }
}
