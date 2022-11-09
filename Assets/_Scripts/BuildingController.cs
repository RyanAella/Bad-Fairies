using System;
using UnityEngine;
using UnityEngine.UI;
using Grid = _Scripts.Grid;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private Canvas buildMenu;
    [SerializeField] private GameObject wallIndicatorPrefab;
    [SerializeField] private GameObject stairsIndicatorPrefab;
    [SerializeField] private GameObject rampIndicatorPrefab;

    private Grid _grid;
    
    private GameObject _currentBuildableObject;

    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {

        HandleNewBuildable();

        if (_currentBuildableObject != null)
        {
            MoveCurrentBuildableObjectWithMouse();
        }


        // if (Input.GetButtonDown("Fire1"))
        // {
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         PlaceBuildableNear(hit.point, _currentBuildableObject);
        //     }
        // }
    }

    private void MoveCurrentBuildableObjectWithMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            _currentBuildableObject.transform.position = hit.point;
            _currentBuildableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void HandleNewBuildable()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_currentBuildableObject == null)
            {
                Debug.Log("Build Wall");
                _currentBuildableObject = Instantiate(wallIndicatorPrefab);
                // Instantiate(wallPrefab, new Vector3(0, 0.25f, -4.1f), Quaternion.identity,
                //     GameObject.Find("=== Buildables ===").transform);
            }
            else
            {
                Destroy(_currentBuildableObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_currentBuildableObject == null)
            {
                Debug.Log("Build Stairs");
                _currentBuildableObject = Instantiate(stairsIndicatorPrefab);
                // Instantiate(wallPrefab, new Vector3(0, 0.25f, -4.1f), Quaternion.identity,
                //     GameObject.Find("=== Buildables ===").transform);
            }
            else
            {
                Destroy(_currentBuildableObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_currentBuildableObject == null)
            {

                Debug.Log("Build Ramp");
                _currentBuildableObject = Instantiate(rampIndicatorPrefab);
                // Instantiate(wallPrefab, new Vector3(0, 0.25f, -4.1f), Quaternion.identity,
                //     GameObject.Find("=== Buildables ===").transform);
            }
            else
            {
                Destroy(_currentBuildableObject);
            }
        }
    }

    void PlaceBuildableNear(Vector3 clickPoint, GameObject buildable)
    {
        var finalPosition = _grid.getNearestPointOnGrid(clickPoint);
        // Instantiate(buildable, finalPosition, Quaternion.identity, GameObject.Find("=== Buildables ===").transform);
    }
}
