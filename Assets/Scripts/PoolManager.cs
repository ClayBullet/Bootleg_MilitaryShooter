using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("ENEMIES INSTANTIATOR")]
    [Space]
    [SerializeField] private GameObject enemyMercenaryGameObject;

    [Header("PARTICLES")]
    [Space]
    [SerializeField] private GameObject bloodVfx;
    [SerializeField] private GameObject floorVfx;
    [SerializeField] private GameObject explossionVfx;
    [HideInInspector] public PoolObjects objectsForPool;

    [Header("PROJECTILES")]
    [Space]
    [SerializeField] private GameObject syringeProjectile;
    [SerializeField] private GameObject enemy_BasicProjectile;
    [SerializeField] private GameObject enemy_ImproveProjectile;

    [HideInInspector] public List<GameObject> syringeProjectiles = new List<GameObject>();
    [HideInInspector] public List<GameObject> enemyBasicsProjectiles = new List<GameObject>();
    [HideInInspector] public List<GameObject> enemyImproveProjectiles = new List<GameObject>();
    [HideInInspector] public List<GameObject> explossionBasics = new List<GameObject>();
    
    #region PoolingLists
    [HideInInspector] public List<GameObject> enemyMercenaryList = new List<GameObject>();
    [HideInInspector] public List<GameObject> bloodVfxList = new List<GameObject>();
    [HideInInspector] public List<GameObject> floorVfxList = new List<GameObject>();
    #endregion
  
    public Dictionary<GameObject, List<GameObject>> dictionaryPool = new Dictionary<GameObject, List<GameObject>>();

    [Header("EMPTY SHELLS")]
    [Space]
    [SerializeField] private GameObject emptyPistolShell;
    [SerializeField] private GameObject emptyRocketCellShell;
    [HideInInspector] public List<GameObject> emptyPistolList = new List<GameObject>();
    [HideInInspector] public List<GameObject> emptyRocketCellList = new List<GameObject>();

    public Dictionary<GameObject, List<GameObject>> dictionaryPistolPool = new Dictionary<GameObject, List<GameObject>>();
    private void Awake()
    {
        GameManager.managerGame.managerPool = this;
        objectsForPool = GetComponent<PoolObjects>();
    }

    private void Start()
    {
        PoolingObjects();
    }
    private void PoolingObjects()
    {
        objectsForPool.CreatePool(syringeProjectile, 10, " SYRINGE KEEPER", ref syringeProjectiles);

        dictionaryPool.Add(syringeProjectile, syringeProjectiles);

        objectsForPool.CreatePool(enemy_BasicProjectile, 100, " ENEMY BASIC PROJECTILE", ref enemyBasicsProjectiles);

        dictionaryPool.Add(enemy_BasicProjectile, enemyBasicsProjectiles);

        objectsForPool.CreatePool(explossionVfx, 45, " EXPLOSSION VFX ", ref explossionBasics);

        objectsForPool.CreatePool(enemy_ImproveProjectile, 50, "INCREASE BULLETS", ref enemyImproveProjectiles);

        dictionaryPool.Add(enemy_ImproveProjectile, enemyImproveProjectiles);

        dictionaryPool.Add(explossionVfx, explossionBasics);

        objectsForPool.CreatePool(bloodVfx, 15, " BLOOD VFX ", ref bloodVfxList);


        objectsForPool.CreatePool(floorVfx, 15, " VFX  EFFECT ", ref floorVfxList);

        dictionaryPool.Add(floorVfx, floorVfxList);

        dictionaryPool.Add(bloodVfx, bloodVfxList);

        //objectsForPool.CreatePool(hologramCharacter, 15, " HOLOGRAM CHARACTERS ", ref hologramCharacters);

        //dictionaryPool.Add(hologramCharacter, hologramCharacters);

        objectsForPool.CreatePool(emptyPistolShell, 15, " EMPTY PISTOL SHELL ", ref emptyPistolList);

        dictionaryPistolPool.Add(emptyPistolShell, emptyPistolList);

        objectsForPool.CreatePool(emptyRocketCellShell, 15, "EMPTY ROCKET CELL SHELL ", ref emptyRocketCellList);

        dictionaryPistolPool.Add(emptyRocketCellShell, emptyRocketCellList);
        //  dictionaryPool.Add(bloodVfx, floorVfxList);

    }
}
