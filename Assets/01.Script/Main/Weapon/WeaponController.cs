using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon curWeapon;
    public Weapon CurWeapon
    {
        get { return curWeapon; }
        set
        {
            curWeapon = value;
            UIManager.Instance.SetWeaponUI(curWeapon.WeaponData.weaponName);
        }
    }
    private bool canReadInput = true;

    public void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (canReadInput)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                curWeapon.Reload(() => canReadInput = true);
                canReadInput = false;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                curWeapon.Attack(() => canReadInput = true, 0);
                canReadInput = false;
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                curWeapon.Attack(() => canReadInput = true, 1);
                canReadInput = false;
            }
        }
    }
}
