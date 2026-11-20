using UnityEngine;

public class Slot : MonoBehaviour
{
    private GameObject currentTower;
    private Collider2D slotCollider;

    void Start()
    {
        slotCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (BuildMenu.instance != null && BuildMenu.instance.menuUI.activeSelf)
            {
                RectTransform menuRect = BuildMenu.instance.menuUI.GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(menuRect, Input.mousePosition))
                {
                    return;
                }
            }

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (slotCollider != null && slotCollider.OverlapPoint(mouseWorldPos))
            {
                if (currentTower != null) return;
                BuildMenu.instance.OpenMenu(this);
            }
        }
    }

    public void BuildTower(GameObject towerPrefab)
    {
        if (towerPrefab == null || currentTower != null) return;
        currentTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
    }
}