using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level", fileName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] public GameObject[] waves;
}
