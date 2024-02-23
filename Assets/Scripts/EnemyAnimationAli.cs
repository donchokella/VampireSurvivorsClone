using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationAli : EnemyAnimation
{

    public float aliMoveSpeedMultiplier = 0.9f;

    public EnemyController controllerAli;

    private new void Start()
    {
        base.Start();
        controllerAli = GetComponent<EnemyController>();

        controllerAli.moveSpeed = PlayerController.instance.moveSpeed * aliMoveSpeedMultiplier;
    }
}
