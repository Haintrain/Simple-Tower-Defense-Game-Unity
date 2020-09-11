#pragma warning disable 0649 //this disables variable never assigned to warnings
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Towers")]
public class Tower : ScriptableObject
{
    
    [SerializeField]
    private float fireCooldown, damage, range, aoeRadius, disable;
    [Tooltip("1 first\n2 closest\n3last")]
    [SerializeField]
    private int targeting;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private bool percentageDamage, aoe;

    public float FireCooldown { get { return fireCooldown; } set {; } }
    public float Damage { get { return damage; } set {; } }
    public float Range { get { return range; } set {; } }
    public float AoERadius { get { return aoeRadius; } set {; } }
    public float Disable { get { return disable; } set {; } }

    public int Targeting { get { return targeting; } set {; } }

    
    public GameObject Projectile { get { return projectile; } set {; } }

    public bool PercentageDamage { get { return percentageDamage; } set {; } }
    public bool AoE { get { return aoe; } set {; } }

    public string towerName;
}
