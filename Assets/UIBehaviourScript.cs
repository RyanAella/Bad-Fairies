using _Scripts.Buildings;
using TMPro;
using UnityEngine;

public class UIBehaviourScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lineOne;
    [SerializeField] private TextMeshProUGUI lineTwo;
    [SerializeField] private TextMeshProUGUI lineThree;
    private GameObject _player;
    private int _buildingModus;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        _buildingModus = _player.GetComponent<BuildingController>().modus;
        
        lineOne.color = Color.black;
        lineTwo.color = Color.black;
        lineThree.color = Color.black;
        
        if (_buildingModus == 0)
        {
            lineOne.color = Color.red;
        } else if (_buildingModus == 1)
        {
            lineTwo.color = Color.red;
        } else if (_buildingModus == 2)
        {
            lineThree.color = Color.red;
        }
    }
}
