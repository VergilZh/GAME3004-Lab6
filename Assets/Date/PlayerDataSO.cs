using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName ="Date/PlayerDate")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Player Transform Properties")]
    public Vector3 playerPosition;
    public Quaternion PlayerRotation;

    [Header("Player Attributes")]
    public int playerHealth;
}
