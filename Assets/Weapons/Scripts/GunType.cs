using UnityEngine;

[CreateAssetMenu(fileName = "GunType", menuName = "ScriptableObjects/Create a gun", order = 1)]
public class GunType : ScriptableObject
{
    public AnimationClip clip;
    public Transform gunModel;
    public float rateOfFire;
    public float speed = 10;
    public float deathTime = 5;

}
