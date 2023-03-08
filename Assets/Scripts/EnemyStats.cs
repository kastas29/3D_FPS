using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    [SerializeField] int hp;
    [SerializeField] float detectDistance;
    [SerializeField] int runSpeed;
    [SerializeField] int walkSpeed;
    [SerializeField] int crouchSpeed;
    [SerializeField] int jumpForce;
    [SerializeField] int shootDistance;


    public int Hp { get => hp; set => hp = value; }
    public float DetectDistance { get => detectDistance; set => detectDistance = value; }
    public int RunSpeed { get => runSpeed; set => runSpeed = value; }
    public int WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public int CrouchSpeed { get => crouchSpeed; set => crouchSpeed = value; }
    public int JumpForce { get => jumpForce; set => jumpForce = value; }
    public int ShootDistance { get => shootDistance; set => shootDistance = value; }
}
