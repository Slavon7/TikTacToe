using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasObject;

    // Метод для открытия канваса
    public void OpenCanvas()
    {
        canvasObject.SetActive(true);
    }

    // Метод для закрытия канваса
    public void CloseCanvas()
    {
        canvasObject.SetActive(false);
    }
}
