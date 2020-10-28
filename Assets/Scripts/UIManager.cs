using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasScaler mainCanvas;
    [SerializeField] private float canvasScale = 2f;
    [SerializeField] private AnimationClip endAnimation;
    
    private bool isClosingGUI = false;
    public bool IsGUIClosed { get; private set; } = false;


    private void Start()
    {
        var xScale = 1920 / canvasScale;
        var yScale = 1080 / canvasScale;
        mainCanvas.referenceResolution = new Vector2(xScale, yScale);
    }

    private void Update()
    {
        CloseGUIOnAnimationFinish();
    }

    void CloseGUIOnAnimationFinish()
    {
        var animation = GetComponent<Animation>();
        if (isClosingGUI && !animation.isPlaying)
        {
            IsGUIClosed = true;
        }
    }

    public void StartCloseGUI()
    {
        var animation = GetComponent<Animation>();
        animation.Play("Canvas End Animation");
        isClosingGUI = true;
    }
}
