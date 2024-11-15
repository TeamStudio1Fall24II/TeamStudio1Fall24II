using UnityEngine;

public class SFXWeaponSlot : MonoBehaviour
{
    [Header("Weapon Slot")]
    [SerializeField] ProjectileLauncher wp;
    [SerializeField] SFXMaker reloading;
    [SerializeField] SFXMaker reloaded;
    [SerializeField] SFXMaker cannotFire;

    public int previous = 0;
    public bool init = false;

    private void Start()
    {
        if (wp != null) 
        {
            wp.AmmoChangeEvent += AmmoChanged;
            wp.CannotFireEvent += CannotFire;
        }
    }

    private void AmmoChanged()
    {
        if(!init)
        {
            init = true;
        }
        else
        {
            if (previous < wp.MaxAmmo && wp.CurrentAmmo == wp.MaxAmmo)
            {
                reloaded.MakeSound();
            }
            else if (previous == 0 && wp.CurrentAmmo > 0)
            {
                reloading.MakeSound();
            }
            previous = wp.CurrentAmmo;
        }
    }

    private void CannotFire()
    {
        cannotFire.MakeSound();
    }
}
