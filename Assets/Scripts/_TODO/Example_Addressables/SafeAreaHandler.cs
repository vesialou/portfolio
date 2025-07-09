using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleAddressables
{
    [ExecuteAlways]
    public class SafeAreaHandler : MonoBehaviour
    {
        private RectTransform rectTransform;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        void ApplySafeArea()
        {
            if (rectTransform == null) return;

            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        void OnValidate()
        {
            ApplySafeArea();
        }

        void OnEnable()
        {
            ApplySafeArea();
        }
    }
}

