using UnityEngine;

[CreateAssetMenu(fileName="DoorManager", menuName="Door Manager")]
public class DoorManager : ScriptableObject {
    public Vector3[] level1Destinations;
    public int[] level1Conditions;
}