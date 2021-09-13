using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    public int medkitsPlayer;
    public int lockPick;
    public int ammoPistol;
    public int ammoRifle;
    public int ammoShotgun;
    public int ammoSubmachine;
    public int ammoGrenade;

    private void Awake()
    {
        GameManager.managerGame.playerInventory = this;
    }
}
public enum AmmoKind
{
    pistolAmmo,
    shotgunAmmo,
    rifleAmmo,
    submachineAmmo,
    specialAmmo
}