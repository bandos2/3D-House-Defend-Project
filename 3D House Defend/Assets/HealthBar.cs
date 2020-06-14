using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Owner;
    public Image image;

    private float CurrentHealth;
    private float MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = Owner.GetComponentInChildren<EnemyStats>().MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = Owner.GetComponentInChildren<EnemyStats>().CurrentHealth;
        float num = (CurrentHealth / MaxHealth);
        image.fillAmount = (num);
    }
}
