using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour {
    public CanvasScaler canvasScaler;

    void Start() {
        float screenRatio = (float)Screen.width / Screen.height;
        float referenceRatio = canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;

        if (screenRatio > referenceRatio)
            canvasScaler.matchWidthOrHeight = 1;  
            canvasScaler.matchWidthOrHeight = 0;  
    }
}