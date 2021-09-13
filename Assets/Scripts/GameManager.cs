using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager managerGame;
    [SerializeField] private GameObject leaderObject;
    [HideInInspector] public WeaponPlayerManager managerPlayerWeapon;
    [HideInInspector] public WeaponManager managerWeapon;
    [HideInInspector] public PoolManager managerPool;
    [HideInInspector] public InventoryPlayer playerInventory;
    [HideInInspector] public InputManager managerInput;
    [HideInInspector] public AnimationManager managerAnimation;
    [HideInInspector] public ShootsMode modeShoots;
    [HideInInspector] public ItemsInteractions interactionItems;
   
    [HideInInspector] public TimeManager managerTime;
    [HideInInspector] public EndGameState stateGameEnd;
    [HideInInspector] public DoTweenManager managerDoTween;
    [HideInInspector] public UniqueGunBehaviour behaviourGunUnique;
    [HideInInspector] public RigidbodyConstraints constraintRigidbody;
    [HideInInspector] public SettingsManager managerSettings;
    [HideInInspector] public SoundManager managerSound;
    [HideInInspector] public PopUpGlobalManager managerGlobalPopUp;
    [HideInInspector] public ColliderManager managerCollider;
    [HideInInspector] public FiniteStateManager stateFiniteManager;
    [HideInInspector] public CursorHidden hiddenCursor;
    [HideInInspector] public PersistentDataSystem systemDataPersistent;
    [HideInInspector] public FootStepSound stepSoundFoot;
    [HideInInspector] public MatrixCombatResolution resolutionCombatMatrix;
    [HideInInspector] public PathfindingImprovement improvementPathfiding;

    private void Awake()
    {
        if (managerGame == null)
            managerGame = this;
        else
        {
            Destroy(leaderObject);
            Destroy(this.gameObject);
        }

        

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChargeTheCurrentInfo;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChargeTheCurrentInfo;
    }
    private void ChargeTheCurrentInfo(Scene scene, LoadSceneMode modeLoadScene)
    {
        managerGlobalPopUp = GetComponent<PopUpGlobalManager>();

    }
}
