using UnityEngine;

public class Loby : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.Play(3,"bgm_mianground",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
