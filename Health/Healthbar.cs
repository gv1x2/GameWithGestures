using UnityEngine;
using UnityEngine.UI;

public class  Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 9.09f;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth /9.09f;
    }
}
