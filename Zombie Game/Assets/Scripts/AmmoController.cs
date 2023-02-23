using UnityEngine;
using UnityEngine.UI;

public class AmmoController : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 100;
    public Text ammoAmountText;

    private void Update()
    {
        ammoAmountText.text = GetCurrentAmmo().ToString();
    }

    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }
    
    public void ReduceCurrentAmmo()
    {
        ammoAmount--;
    }
}
