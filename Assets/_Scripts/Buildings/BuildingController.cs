using UnityEngine;

namespace _Scripts.Buildings
{
    public class BuildingController : MonoBehaviour
    {
        // [SerializeField] private GameObject[] frames;
        // [SerializeField] private GameObject[] buildables;
        // [SerializeField] private GameObject[] buildableBlueprints;
        // [SerializeField] private GameObject[] buildablePrefab;
        // [SerializeField] private LayerMask layerMask;
        [SerializeField] private int buildingDistance = 15;

        private RaycastHit _hit;
        private Ray _ray;

        private Grid _grid;

        private GameObject _currentBuildable;
        private GameObject _frameContainer;
        private GameObject _buildable;
        private GameObject _frame;

        private Vector3 _frameVector;

        private bool _vectorSet;
        private float _mouseWheelRotation;
        private int _currentFrameIndex = -1;
        private int _currentBuildableIndex = -1;
        private int modus = 0;

        // public GameObject obstacle;

        public GameObject floor;
        public GameObject ramp;
        public GameObject wall;
        public GameObject floorFrame;
        public GameObject rampFrame;
        public GameObject wallFrame;

        private void Update()
        {
            ChooseBuildable();

            _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(_ray, out _hit))
            {
                // zu weit weg
                if (_hit.distance > buildingDistance)
                {
                    Destroy(_frameContainer);
                    _vectorSet = false;
                }

                // wenn frameVector nicht da wo wir hinschauen, aber der vector schonmal gesetzt wurde u gerüstcontainer existiert
                if (_frameVector != _hit.point && _vectorSet)
                {
                    _frameContainer.transform.position = _hit.point;
                    _frameContainer.transform.rotation = transform.rotation;
                }

                if (_hit.distance < buildingDistance && !_vectorSet)
                {
                    _frameContainer =
                        Instantiate(_frame, _hit.point, transform.rotation); // buildablePrefab[_currentPrefabIndex]
                    _frameVector = _hit.point;
                    _vectorSet = true;
                }

                if (_hit.distance < buildingDistance && _hit.collider.gameObject.CompareTag("Frame"))
                {
                    _hit.collider.gameObject.GetComponent<Frame>().tracked = true;
                    _hit.collider.gameObject.GetComponent<Frame>().bc.modus = modus;
                    // _hit.collider.gameObject.transform.parent.gameObject.GetComponent<BuildableController>().modus =
                    //     modus;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                }

                if (_hit.distance < buildingDistance && _hit.collider.gameObject.CompareTag("Buildable"))
                {
                    _hit.collider.gameObject.GetComponent<BuildableController>().modus = modus;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                }

                if (Input.GetButtonDown("Fire1") && _hit.distance < buildingDistance)
                {
                    if (_hit.collider.gameObject.CompareTag("Buildable"))
                    {
                    }
                    else if (_hit.collider.gameObject.CompareTag("Frame"))
                    {
                        GameObject container;
                        container = Instantiate(_buildable,
                            _hit.collider.gameObject.transform.position,
                            _hit.collider.gameObject.transform.rotation);
                    }
                    else
                    {
                        if (modus == 1)
                        {
                            var instance = Instantiate(_buildable, _hit.point, transform.rotation);
                            instance.transform.Rotate(0, 90, -45);
                            instance.transform.Translate(-2.5f, 0, 0);
                        }
                        else if (modus == 2)
                        {
                            var instance = Instantiate(_buildable, _hit.point, transform.rotation);
                            instance.transform.Rotate(90, 0, 0);
                            instance.transform.Translate(0, 0, -2.5f);
                        }
                        else
                        {
                            Instantiate(_buildable, _hit.point, transform.rotation); 
                        }
                        
                    }
                }

                // // BlueprintController.
                //
                // if (_currentBlueprint != null)
                // {
                //     MoveCurrentBuildableWithMouse();
                //     RotateFromMouseWheel();
                //     Build(_currentPrefabIndex);
                // }
            }
        }

        /* Choose the buildable
         * If a buildable already has been selected, it gets destroyed
        */
        private void ChooseBuildable()
        { 
            if (Input.GetKey(KeyCode.Alpha1))
            {
                Debug.Log("1 pressed");
                modus = 0;
                _buildable = floor;
                _frame = floorFrame;
                Destroy(_frameContainer);
                _vectorSet = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("2 pressed");
                modus = 1;
                _buildable = ramp;
                _frame = rampFrame;
                Destroy(_frameContainer);
                _vectorSet = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("3 pressed");
                modus = 2;
                _buildable = wall;
                _frame = wallFrame;
                Destroy(_frameContainer);
                _vectorSet = false;
            }
            
        }

        // private bool PressedKeyOfCurrentPrefab(int i)
        // {
        //     return _currentBlueprint != null && _currentBlueprintIndex == i;
        // }

        // private void Build(int prefabIndex)
        // {
        //     if (Input.GetButtonDown("Fire1"))
        //     {
        //         Instantiate(buildablePrefab[prefabIndex], _currentBlueprint.transform.position,
        //             _currentBlueprint.transform.rotation);
        //         _currentBlueprint = null;
        //         Destroy(_currentBlueprint);
        //     }
        // }

        // Rotate the buildable with the mouse wheel
        // private void RotateFromMouseWheel()
        // {
        //     var rot = _currentBlueprint.transform.rotation;
        //
        //     if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //     {
        //         _currentBlueprint.transform.Rotate(Vector3.up, -90f);
        //     }
        //     else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //     {
        //         _currentBlueprint.transform.Rotate(Vector3.up, 90f);
        //     }
        // }

        // Move the buildable to where the mouse is
        // private void MoveCurrentBuildableWithMouse()
        // {
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0) /*Input.mousePosition*/);
        //
        //     if (Physics.Raycast(ray, out hit, 10f, layerMask))
        //     {
        //         Instantiate(_currentBlueprint, hit.point, transform.rotation);
        //         // _currentBlueprint.transform.position = hit.point;
        //     }
        // }
    }
}