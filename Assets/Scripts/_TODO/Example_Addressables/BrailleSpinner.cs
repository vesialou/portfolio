namespace ExampleAddressables
{
    using UnityEngine;
    using TMPro;

    public class BrailleSpinner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _target;

        private readonly float _interval = 0.08f;
        private readonly string _prefix = "Loading";
        private int _currentIndex = 0;
        private float _timer = 0f;
        private readonly string[] frames = new string[]
        {
            "[ ⣾ ]", "[ ⣽ ]", "[ ⣻ ]", "[ ⢿ ]", "[ ⡿ ]", "[ ⣟ ]", "[ ⣯ ]", "[ ⣷ ]"
        };


        private void Update()
        {
            _timer += Time.unscaledDeltaTime;
            if (_timer >= _interval)
            {
                _target.text = $"{_prefix} {frames[_currentIndex]}";
                _currentIndex = (_currentIndex + 1) % frames.Length;
                _timer = 0f;
            }
        }
    }

}
