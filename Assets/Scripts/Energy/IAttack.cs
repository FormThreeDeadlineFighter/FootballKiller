using UnityEngine;

public class IAttack : MonoBehaviour
{
    public float Damage;
    public Elements Elements;
    Renderer _renderer;

    private void OnEnable()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        
        switch(Elements)
        {
            case Elements.red:
                _renderer.sharedMaterial.color = Color.red;
                break;
            case Elements.green:
                _renderer.sharedMaterial.color = Color.green;
                break;
            case Elements.blue:
                _renderer.sharedMaterial.color = Color.blue;
                break;
            default:
                _renderer.sharedMaterial.color = Color.white;
                break;
        }
    }
}
