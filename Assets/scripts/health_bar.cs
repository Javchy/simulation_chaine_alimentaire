using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.UI;

public class health_bar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxhealth = 2f;
    private float currenthealth;

    void Start()

    {
        currenthealth = maxhealth;
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxhealth;
        healthSlider.value = maxhealth;
    }

    public void TakeDamage (float damage)

    {
        currenthealth -= damage;
        if (currenthealth <= 0)

        {
            Destroy(gameObject);
            currenthealth = 0;
        }

        healthSlider.value = currenthealth;
    }
}