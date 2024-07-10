using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileRaycaster : MonoBehaviour
{
    // This script casts a ray from the mouse and whenever the mouse hover over a tile, the coordinates are displayed 
    // in the TextMeshPro inside the canvas.

    Camera mainCamera;
    public TextMeshProUGUI positionText;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                Vector2 position = tileInfo.GetPosition();
                positionText.text = $"{position.x}, {position.y}";
            }
        }
    }
}
