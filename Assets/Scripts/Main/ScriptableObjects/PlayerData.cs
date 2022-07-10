using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Entity Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Editor Only")]
    public float maximumHP;
    public float maximumStyle;
    public float dashCooldown;
    public float gunRollCooldown;

    [Header("Editor and Runtime")]
    public float moveSpeed;
    public float jumpVelocity;
    public float dashVelocity;
}
