using UnityEngine;

public class ShooterEnergy : MonoBehaviour
{
    private float finalSize = 4f; 
    private float StartSize = 0.1f;
    private float currentSize;
    [SerializeField] GameObject energy;
    
    void OnEnable() 
    {
        currentSize = StartSize;
        ObjectSizeChange(StartSize);
    }

    void OnDisable()
    {
        currentSize = StartSize;
    }
    
    // Update is called once per frame
    void Update()
    {
         if(currentSize != finalSize)
        {
            currentSize += Time.deltaTime * 10f;
            ObjectSizeChange(currentSize);
        }
        else
        {
            currentSize = finalSize;
            ObjectSizeChange(currentSize);
        }
    } 

    void ObjectSizeChange(float value)
    {
        gameObject.transform.localScale = new Vector3(value,value,value);
        energy.gameObject.transform.localScale = new Vector3(value,value,value);
    }
}
