using UnityEngine;

public class TextureExample : MonoBehaviour
{
    // Reference to the material that will hold our texture
    public Material targetMaterial;

    // Reference to the texture (you can assign this in the Inspector)
    public Texture2D imageTexture;

    void Start()
    {
        // Make sure the target material and texture are assigned
        if (targetMaterial == null || imageTexture == null)
        {
            Debug.LogError("Please assign the material and texture!");
            return;
        }

        // Set the texture in the material
        targetMaterial.mainTexture = imageTexture;
    }
}
