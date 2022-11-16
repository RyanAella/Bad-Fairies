using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Buildings
{
    public class BuildingController : MonoBehaviour
    {
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
        public bool buildingSelected = false;

        private float _mouseWheelRotation;

        public GameObject floor;
        public GameObject ramp;
        public GameObject wall;
        public GameObject floorFrame;
        public GameObject rampFrame;
        public GameObject wallFrame;

        public int mode;

        private void Update()
        {
            if (gameObject.GetComponent<PlayerController>().GetCurrentPlayerMode() == 1)
            {
                ChooseBuildable();

                _ray = Camera.main!.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

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
                        if (_frameContainer != null)
                        {
                            _frameContainer.transform.position = _hit.point;
                            _frameContainer.transform.rotation = transform.rotation;
                        }
                    }

                    if (_hit.distance < buildingDistance && !_vectorSet)
                    {
                        if (_frame != null)
                            _frameContainer =
                                Instantiate(_frame, _hit.point, transform.rotation);
                        _frameVector = _hit.point;
                        _vectorSet = true;
                    }

                    if (_hit.distance < buildingDistance && _hit.collider.gameObject.CompareTag("Frame"))
                    {
                        _hit.collider.gameObject.GetComponent<Frame>().tracked = true;
                        _hit.collider.gameObject.GetComponent<Frame>().bc.mode = mode;
                        Destroy(_frameContainer);
                        _vectorSet = false;
                    }

                    if (_hit.distance < buildingDistance && _hit.collider.gameObject.CompareTag("Buildable"))
                    {
                        _hit.collider.gameObject.GetComponent<Buildable>().mode = mode;
                        Destroy(_frameContainer);
                        _vectorSet = false;
                    }

                    if (Input.GetButtonDown("Fire1") && _hit.distance < buildingDistance)
                    {
                        var hitGameObject = _hit.collider.gameObject;

                        if (hitGameObject.CompareTag("Buildable"))
                        {
                        }
                        else if (hitGameObject.CompareTag("Frame"))
                        {
                            GameObject container;
                            if (mode == 2)
                            {
                                container = Instantiate(_buildable, hitGameObject.transform.position,
                                    hitGameObject.transform.rotation);
                                if (!_hit.collider.gameObject.transform.parent.gameObject.name.Equals("Wall"))
                                {
                                    container.transform.Rotate(0, 90, 0);
                                }
                            }
                            else
                            {
                                container = Instantiate(_buildable, hitGameObject.transform.position,
                                    hitGameObject.transform.rotation);
                            }

                            container.transform.SetParent(hitGameObject.transform.parent);
                        }
                        // neither "buildable" nor "frame"
                        else
                        {
                            if (mode == 0)
                            {
                                var instance = Instantiate(_buildable, _hit.point, transform.rotation);
                            }
                            // ramp
                            else if (mode == 1)
                            {
                                var instance = Instantiate(_buildable, _hit.point, transform.rotation);
                                instance.transform.Rotate(0, 90, -45);
                                instance.transform.Translate(-2.5f, 0, 0);
                            }
                            // wall
                            else if (mode == 2)
                            {
                                var instance = Instantiate(_buildable, _hit.point, transform.rotation);
                                instance.transform.Rotate(-90, 0, 0);
                                instance.transform.Translate(0, 0, 2.5f);
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
                    // }
                }
            }
        }

        public int GetCurrentBuildMode()
        {
            return mode;
        }

        /* Choose the buildable
         * If a buildable already has been selected, it gets destroyed
        */
        private void ChooseBuildable()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!buildingSelected)
                {
                    Debug.Log("1 pressed");
                    mode = 0;
                    _buildable = floor;
                    _frame = floorFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                else if (buildingSelected && mode != 0)
                {
                    Debug.Log("1 pressed");
                    mode = 0;
                    _buildable = floor;
                    _frame = floorFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                else
                {
                    mode = 3;
                    _buildable = null;
                    _frame = null;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                    buildingSelected = false;
                }
            }


            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!buildingSelected)
                {
                    Debug.Log("2 pressed");
                    mode = 1;
                    _buildable = ramp;
                    _frame = rampFrame;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                    buildingSelected = true;
                }
                else if (buildingSelected && mode != 1)
                {
                    Debug.Log("2 pressed");
                    mode = 1;
                    _buildable = ramp;
                    _frame = rampFrame;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                    buildingSelected = true;
                }
                else
                {
                    mode = 3;
                    _buildable = null;
                    _frame = null;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                    buildingSelected = false;
                }
            }


            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (!buildingSelected)
                {
                    Debug.Log("3 pressed");
                    mode = 2;
                    _buildable = wall;
                    _frame = wallFrame;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                    buildingSelected = true;
                }
                else if (buildingSelected && mode != 2)
                {
                    Debug.Log("3 pressed");
                    mode = 2;
                    _buildable = wall;
                    _frame = wallFrame;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                    buildingSelected = true;
                }
                else
                {
                    mode = 3;
                    _buildable = null;
                    _frame = null;
                    Destroy(_frameContainer);
                    _vectorSet = false;
                    buildingSelected = false;
                }
            }
        }

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
    }
}