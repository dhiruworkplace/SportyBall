using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetail : MonoBehaviour
{
    public bool me;
    public Image panel;
    public Transform treeRank;
    public TextMeshProUGUI noText;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI points;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetData(bool me, int _rank, string name, int _points)
    {
        if (_rank > 3)
        {
            noText.transform.parent.gameObject.SetActive(true);
            noText.text = _rank.ToString();
        }
        else
        {
            treeRank.gameObject.SetActive(true);
            treeRank.GetChild(_rank - 1).gameObject.SetActive(true);
        }
        playerName.text = name;
        points.text = _points.ToString();
    }
}