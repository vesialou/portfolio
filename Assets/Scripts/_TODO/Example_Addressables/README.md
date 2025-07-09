# Addressables Harry Scene

## 📍 Scene Path
`Assets/Scenes/AddressablesHarryScene.scene`

This scene allows reading a chapter from a book (e.g. Harry Potter) by loading it via Unity Addressables.

---

## ✨ Features

- **Read** button triggers async text loading.
- Text is fetched via Addressables from `HarryPotter_Chapter1`.
- Loading indicator uses a Braille-style spinner.
- UI respects safe screen areas.

---

## 🔧 Key Components

### `HarryPotterLoader.cs`
- Handles button logic and UI toggles.
- Loads text using `AddressableTextLoader`.
- Displays it using `TextDisplayer`.

### `AddressableTextLoader.cs`
- Loads a `TextAsset` from Addressables.
- Handles cancelation and release.

### `TextDisplayer.cs`
- Sets text to a `TextMeshProUGUI`.

### `BrailleSpinner.cs`
- Shows animated "[ ⣷ ]"-style loading effect.

### `SafeAreaHandler.cs`
- Adjusts UI to device safe area.

---

## 🧪 Usage

1. On launch, the intro panel is shown.
2. Clicking **Read**:
   - Hides intro, shows spinner,
   - Loads text from Addressables,
   - Displays text in a scrollable area.
3. Errors are shown as `[Error loading text]`.

---

## 📁 Requirements

- Unity Addressables system
- TextMeshPro
- Cysharp's UniTask

---

## 🗂 Address Used
```csharp
[SerializeField] private string address = "HarryPotter_Chapter1";
