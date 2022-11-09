using _Scripts.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private PlayerStats _stats;
    

    private void Start()
    {
        _stats = new PlayerStats(10, 10, 10, 3);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
