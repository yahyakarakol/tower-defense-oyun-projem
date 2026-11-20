using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public static BuildMenu instance;

    [Header("UI Penceresi")]
    public GameObject menuUI; // TowerSelectUI paneli
    public Vector3 offset = new Vector3(0f, 1.2f, 0f); // Slotun ne kadar üstünde açýlsýn?

    [Header("Kule Kalýplarý (Prefabs)")]
    public GameObject standardTowerPrefab;
    public GameObject laserTowerPrefab;
    public GameObject archerTowerPrefab;
    private Slot selectedSlot;

    void Awake()
    {
        instance = this;
    }

    public void OpenMenu(Slot slot)
    {
        // Zaten açýk olan ayný slota tekrar týklandýysa pencereyi kapat
        if (selectedSlot == slot && menuUI.activeSelf)
        {
            CloseMenu();
            return;
        }

        selectedSlot = slot;
        menuUI.SetActive(true);

        // Slotun dünya koordinatýný ekran (UI) koordinatýna çevirip pencereyi oraya taþý
        Vector3 screenPos = Camera.main.WorldToScreenPoint(slot.transform.position + offset);
        menuUI.transform.position = screenPos;
    }

    public void CloseMenu()
    {
        menuUI.SetActive(false);
        selectedSlot = null;
    }

    public void BuildStandardTower()
    {
        if (selectedSlot != null)
        {
            selectedSlot.BuildTower(standardTowerPrefab);
            CloseMenu();
        }
    }

    public void BuildLaserTower()
    {
        if (selectedSlot != null)
        {
            selectedSlot.BuildTower(laserTowerPrefab);
            CloseMenu();
        }
    }
    public void BuildArcherTower()
    {
        if (selectedSlot != null)
        {
            selectedSlot.BuildTower(archerTowerPrefab);
            CloseMenu();
        }
    }
}