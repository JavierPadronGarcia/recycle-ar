using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int points;

    public TextMeshProUGUI TextMeshProUGUI;

    public void Update()
    {
        TextMeshProUGUI.SetText(points.ToString());
    }

}
