using UnityEngine;

public class Leaf : MonoBehaviour
{
    
    [SerializeField] Renderer renderer;


    public void Initialize(float scale, ColorPack colorPack, bool isFocused)
    {
        renderer.material = colorPack.LeafMaterial;
        transform.localScale = Vector3.one * scale;
        renderer.material.SetFloat("_IsGlowing", isFocused ? 1 : 0);
    }
    
    
    

    
    
    
    

}