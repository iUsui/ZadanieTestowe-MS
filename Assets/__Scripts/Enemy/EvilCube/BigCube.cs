using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCube : Cube
{
    public override void HandleOnDie()
    {
        enemyManager.BigCubeDied();
    }
}
