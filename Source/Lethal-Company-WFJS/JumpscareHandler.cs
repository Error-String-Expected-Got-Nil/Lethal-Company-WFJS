using UnityEngine;
using UnityEngine.UI;

namespace Lethal_Company_WFJS;

public class JumpscareHandler
{
    private const int Frames = 14;
    private const int Framerate = 60;
    // Number of screen heights to offset the image downwards to align it such that its initial position shows frame 1.
    private const float Offset = 6.0f;
    
    private readonly GameObject _canvasContainer;
    private readonly GameObject _imageContainer;
    private readonly RectTransform _imageTransform;
    private readonly float _baseWidth;
    private readonly float _baseHeight;

    private float _stopwatch = 0f;
    
    public JumpscareHandler()
    {
        _canvasContainer = new GameObject { name = "JumpscareCanvasContainer" };
        var canvas = _canvasContainer.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        _imageContainer = new GameObject
        {
            name = "JumpscareImageContainer",
            transform = { parent = _canvasContainer.transform }
        };

        var image = _imageContainer.AddComponent<RawImage>();
        image.texture = WFJS_Main.JumpscareTexture;

        _imageTransform = image.rectTransform;
        
        _imageTransform.sizeDelta = new Vector2(Screen.width, Screen.height * Frames);
        _baseWidth = Screen.width / 2.0f;
        _baseHeight = -Screen.height * Offset;
        _imageTransform.position = new Vector3(_baseWidth, _baseHeight);
        
        // TODO: Also play jumpscare sound effect
    }

    // Returns false if the jumpscare is over.
    public bool Tick(float deltaTime)
    {
        _stopwatch += deltaTime;

        var frameIndex = (int)(_stopwatch / (1.0f / Framerate));

        _imageTransform.position = new Vector3(_baseWidth, _baseHeight + Screen.height * frameIndex);

        return frameIndex < Frames;
        
        // TODO: Hold on final frame for a bit before ending jumpscare
    }

    public void Destroy()
    {
        // Destroying an object also destroys all children, so we don't need to explicitly destroy ImageContainer.
        Object.Destroy(_canvasContainer);
    }
}