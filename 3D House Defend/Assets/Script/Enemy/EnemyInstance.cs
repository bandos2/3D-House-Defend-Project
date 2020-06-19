using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    #region
    public static EnemyInstance instance;
    private void Awake()
    {
        instance = this;
    }



    #endregion

    public GameObject Enemy;

}