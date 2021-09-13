using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public delegate void Projectile_Enemy(ColliderBridge brCollider);
    public Projectile_Enemy enemyProjectile;
    public delegate void Projectile_DemoMan(ColliderBridge brCollider);
    public Projectile_DemoMan demoManProjectile;
    private void Awake()
    {
        GameManager.managerGame.managerCollider = this;
    }

    private void Start()
    {
        EnemyInteractions interactionsEnemy = FindObjectOfType<EnemyInteractions>();
        ItemsInteractions interactionItems = FindObjectOfType<ItemsInteractions>();
        ActionsInteractable interactableActions = FindObjectOfType<ActionsInteractable>();
        if (interactionsEnemy != null)
        {
            enemyProjectile = interactionsEnemy.PlayerDamageHealth_EnterCollider;
        }
        if (interactionItems != null)
        {
            //demoManProjectile -= interactionItems.DetonateEffect;
            //demoManProjectile -= interactionItems.RocketJumpForce;

            demoManProjectile += interactionItems.DetonateEffect;
        }
    }
    
    

}
