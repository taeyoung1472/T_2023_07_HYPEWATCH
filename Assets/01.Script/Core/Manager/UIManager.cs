using TMPro;
using UnityEngine;

public class UIManager : MonoSingleTon<UIManager>
{
    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI curAmmoText;

    #region Weapon
    public void SetBulletUI(int curAmmoAmount, int maxAmmoAmount)
    {
        if(maxAmmoAmount == 0)
        {
            curAmmoText.SetText($"-- / --");
        }
        curAmmoText.SetText($"{curAmmoAmount} / {maxAmmoAmount}");
    }
    public void SetWeaponUI(string weaponName)
    {
        weaponNameText.SetText(weaponName);
    }
    #endregion
}
