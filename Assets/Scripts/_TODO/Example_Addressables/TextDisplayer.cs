using TMPro;

namespace ExampleAddressables
{
    public class TextDisplayer
    {
        private readonly TextMeshProUGUI _target;

        public TextDisplayer(TextMeshProUGUI target)
        {
            _target = target;
        }

        public void Display(string text)
        {
            _target.text = text;
        }
    }
}