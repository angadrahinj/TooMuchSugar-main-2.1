using UnityEngine;
using UnityEngine.UI;

public class BlurEffect : MonoBehaviour
{
    public Material blurMaterial;
    public RawImage rawImage;

    [Range(0, 10)]
    public int blurAmount = 2;

    void Start()
    {
        // Create a new material to avoid modifying the original material
        blurMaterial = new Material(blurMaterial);
        rawImage.material = blurMaterial;
    }

    void Update()
    {
        // Update the blur amount in the shader
        blurMaterial.SetInt("_BlurAmount", blurAmount);
    }
}
