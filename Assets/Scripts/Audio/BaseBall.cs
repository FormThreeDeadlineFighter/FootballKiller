using UnityEngine;

public class BaseBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.Play(4,"bgm_boss_1",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
