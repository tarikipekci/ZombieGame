using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5;
    [SerializeField] private AmmoType _ammoType;    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AmmoController>().IncreaseCurrentAmmo(_ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
