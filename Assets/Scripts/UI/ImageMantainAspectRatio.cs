using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageMantainAspectRatio : MonoBehaviour
{
    public enum ImageScale
    {
        Width, Height
    }

    public ImageScale scaleWith = ImageScale.Height;

    private void Start()
    {
        UpdateImageBasedOnSpriteAspectRatio();
    }

    [EasyButtons.Button]
    public void UpdateImageBasedOnSpriteAspectRatio()
    {
        Image img = GetComponent<Image>();

        if (img.sprite == null) return;

        Sprite spr = img.sprite;

        float aspect = scaleWith == ImageScale.Width ? spr.rect.height / spr.rect.width : spr.rect.width / spr.rect.height;


        Vector2 imgSize = img.rectTransform.sizeDelta;

        if (scaleWith == ImageScale.Width) imgSize.y = imgSize.x * aspect;
        else imgSize.x = imgSize.y * aspect;

        img.rectTransform.sizeDelta = imgSize;

    }
}
