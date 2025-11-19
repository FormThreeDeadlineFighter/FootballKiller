using UnityEngine;

public class IAttack : MonoBehaviour
{
    public float Damage;
    public Elements Elements;
    Renderer _renderer;
    
    private void OnEnable()
    {
        /*_renderer = gameObject.GetComponent<Renderer>();
        
        switch(Elements)
        {
            case Elements.red:
                _renderer.material.color = Color.red;
                break;
            case Elements.green:
                _renderer.material.color = Color.green;
                break;
            case Elements.blue:
                _renderer.material.color = Color.blue;
                break;
            default:
                _renderer.material.color = Color.white;
                break;
        }*/
    }
}
