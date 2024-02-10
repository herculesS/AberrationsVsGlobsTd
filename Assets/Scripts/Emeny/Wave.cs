using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObject/Wave", order = 0)]
public class Wave : ScriptableObject
{
    public float WaveLength ;
    public List<Spawn> SpawnPoints ;

}