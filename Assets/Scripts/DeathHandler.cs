using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void HandleDeath()
    {
        Debug.Log("You died.");
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        FindObjectOfType<Weapon>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
