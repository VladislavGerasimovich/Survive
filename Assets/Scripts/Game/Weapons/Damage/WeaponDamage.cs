using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] protected int Damage;

    public int Harm { get; private set; }

    private void Awake()
    {
        Harm = Damage;
    }

    public void SetDamage(int damage)
    {
        Harm = damage;
    }
}
