using System.Net.NetworkInformation;
using UnityEngine;

namespace _Scripts.Buildings
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private GameObject[] buildableBlueprints;
        [SerializeField] private GameObject[] buildablePrefab;
        
        [SerializeField] private LayerMask layerMask;

        private Grid _grid;

        private GameObject _currentBlueprint;

        private float _mouseWheelRotation;
        private int _currentBlueprintIndex = -1;
        private int _currentPrefabIndex = -1;

        public Camera cam;

        private void Awake()
        {
            // _grid = FindObjectOfType<Grid>();
            // var blueprint = GetComponent<BlueprintController>();
        }

        private void Update()
        {
            HandleNewBuildable();
            
            // BlueprintController.

            if (_currentBlueprint != null)
            {
                MoveCurrentBuildableWithMouse();
                RotateFromMouseWheel();
                Build(_currentPrefabIndex);
            }
        }

        private void Build(int prefabIndex)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(buildablePrefab[prefabIndex], _currentBlueprint.transform.position, _currentBlueprint.transform.rotation);
                _currentBlueprint = null;
                Destroy(_currentBlueprint);
            }
        }

        // Rotate the buildable with the mouse wheel
        private void RotateFromMouseWheel()
        {
            var rot = _currentBlueprint.transform.rotation;
            
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _currentBlueprint.transform.Rotate(Vector3.up, -90f);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                _currentBlueprint.transform.Rotate(Vector3.up, 90f);
            }
        }

        // Move the buildable to where the mouse is
        private void MoveCurrentBuildableWithMouse()
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                _currentBlueprint.transform.position = hit.point;
            }
        }

        /* Choose the buildable
         * If a buildable already has been selected, it gets destroyed
        */
        private void HandleNewBuildable()
        {
            for (int i = 0; i < buildableBlueprints.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
                {
                    if (PressedKeyOfCurrentPrefab(i))
                    {
                        Destroy(_currentBlueprint);
                        _currentBlueprintIndex = -1;
                        _currentPrefabIndex = -1;
                    }
                    else
                    {
                        if (_currentBlueprint != null)
                        {
                            Destroy(_currentBlueprint);
                        }

                        _currentBlueprint = Instantiate(buildableBlueprints[i]);
                        _currentBlueprintIndex = i;
                        _currentPrefabIndex = i;
                        Debug.Log(_currentBlueprint.transform.position);
                    }

                    break;
                }
            }
        }

        private bool PressedKeyOfCurrentPrefab(int i)
        {
            return _currentBlueprint != null && _currentBlueprintIndex == i;
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(transform.position, Input.mousePosition);
        // }
    }
}