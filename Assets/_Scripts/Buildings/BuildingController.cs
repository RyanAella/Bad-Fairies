using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Buildings
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private int buildingDistance = 15;

        private RaycastHit _hit;
        private Ray _ray;

        //private Grid _grid;

        //private GameObject _currentBuildable;
        private GameObject _frameContainer;
        private GameObject _buildable;
        private GameObject _frame;

        private Vector3 _frameVector;

        private bool _vectorSet;
        public bool buildingSelected = false;

        //private float _mouseWheelRotation;

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

                Build();
            }
        }

        /* Choose the buildable
         * If a buildable already has been selected, it gets destroyed
        */
        private void ChooseBuildable()
        {
            // If 1 is pressed (Floor Mode)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // If no buildable is selected
                // Set mode on 0 and activate floor
                if (!buildingSelected)
                {
                    //Debug.Log("1 pressed");
                    mode = 0;
                    _buildable = floor;
                    _frame = floorFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                // If a different buildable is selected
                // Change the buidable to floor
                else if (buildingSelected && mode != 0)
                {
                    //Debug.Log("1 pressed");
                    mode = 0;
                    _buildable = floor;
                    _frame = floorFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                // If the same buildable is selected
                // Deselect it
                else
                {
                    mode = 3;
                    _buildable = null;
                    _frame = null;
                    _vectorSet = false;
                    buildingSelected = false;
                    Destroy(_frameContainer);
                }
            }

            // If 2 is pressed (Ramp Mode)
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // If no buildable is selected
                // Set mode on 1 and activate ramp
                if (!buildingSelected)
                {
                    //Debug.Log("2 pressed");
                    mode = 1;
                    _buildable = ramp;
                    _frame = rampFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                // If a different buildable is selected
                // Change the buidable to ramp
                else if (buildingSelected && mode != 1)
                {
                    //Debug.Log("2 pressed");
                    mode = 1;
                    _buildable = ramp;
                    _frame = rampFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                // If the same buildable is selected
                // Deselect it
                else
                {
                    mode = 3;
                    _buildable = null;
                    _frame = null;
                    _vectorSet = false;
                    buildingSelected = false;
                    Destroy(_frameContainer);
                }
            }

            // If 3 is pressed (Wall Mode)
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // If no buildable is selected
                // Set mode on 2 and activate wall
                if (!buildingSelected)
                {
                    //Debug.Log("3 pressed");
                    mode = 2;
                    _buildable = wall;
                    _frame = wallFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                // If a different buildable is selected
                // Change the buidable to wall
                else if (buildingSelected && mode != 2)
                {
                    //Debug.Log("3 pressed");
                    mode = 2;
                    _buildable = wall;
                    _frame = wallFrame;
                    _vectorSet = false;
                    buildingSelected = true;
                    Destroy(_frameContainer);
                }
                // If the same buildable is selected
                // Deselect it
                else
                {
                    mode = 3;
                    _buildable = null;
                    _frame = null;
                    _vectorSet = false;
                    buildingSelected = false;
                    Destroy(_frameContainer);
                }
            }
        }

        private void Build()
        {
            _ray = Camera.main!.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // If the Ray "hits" an object
            if (Physics.Raycast(_ray, out _hit))
            {
                // zu weit weg
                if (_hit.distance > buildingDistance)
                {
                    Destroy(_frameContainer);
                    _vectorSet = false;
                }


                // If hit collider is not an object with Tag Environment the frame is shown
                if (_hit.distance < buildingDistance && !_vectorSet && !_hit.collider.CompareTag("Environment"))
                {
                    if (_frame != null) _frameContainer = Instantiate(_frame, SnapToGrid(_hit.point), transform.rotation);
                    _frameVector = SnapToGrid(_hit.point);
                    _vectorSet = true;
                }

                // Frame moves with mouse
                if (_frameVector != _hit.point && _vectorSet)
                {
                    if (_frameContainer != null)
                    {
                        _frameContainer.transform.position = SnapToGrid(_hit.point);
                        _frameContainer.transform.rotation = transform.rotation;
                    }
                }

                // If hit collider is an object with Tag Frame show the frame and set the correct buildingMode
                if (_hit.distance < buildingDistance && _hit.collider.gameObject.CompareTag("Frame"))
                {
                    _hit.collider.gameObject.GetComponent<Frame>().tracked = true;
                    _hit.collider.gameObject.GetComponent<Frame>().bc.mode = mode;
                    _vectorSet = false; 
                    Destroy(_frameContainer);
                }

                // If hit collider is an object with Tag Buildable set the correct buildingMode
                if (_hit.distance < buildingDistance && _hit.collider.gameObject.CompareTag("Buildable"))
                {
                    _hit.collider.gameObject.GetComponent<Buildable>().mode = mode;
                    _vectorSet = false;
                    Destroy(_frameContainer);
                }

                // If hit collider is not an object with Tag Environment
                if (Input.GetButtonDown("Fire1") && _hit.distance < buildingDistance && !_hit.collider.CompareTag("Environment"))
                {
                    var hitGameObject = _hit.collider.gameObject;

                    // If hit collider is an object with Tag Buildable nothing happens (otherwise multiple objects on same location)
                    if (hitGameObject.CompareTag("Buildable"))
                    {
                    }
                    // If hit collider is an object with Tag Frame place the "buildable" at the position of the frame
                    else if (hitGameObject.CompareTag("Frame"))
                    {
                        GameObject container;

                        // If the current mode is Floor Mode
                        if (mode == 0)
                        {
                            container = Instantiate(_buildable, hitGameObject.transform.position,
                                hitGameObject.transform.rotation);
                            if (!_hit.collider.gameObject.transform.parent.gameObject.name.Equals("Floor"))
                            {
                                container.transform.Rotate(0, 180, 0);
                            }
                            container.transform.SetParent(hitGameObject.transform.parent);
                        }
                        // If current Mode is Ramp Mode
                        else if (mode == 1)
                        {
                            container = Instantiate(_buildable, hitGameObject.transform.position,
                                hitGameObject.transform.rotation);
                            if (_hit.collider.gameObject.transform.parent.gameObject.name.Equals("Ramp"))
                            {
                                container.transform.Rotate(0, 180, 0);

                            }
                            container.transform.SetParent(hitGameObject.transform.parent);
                        }
                        // If current Mode is Wall Mode
                        else if (mode == 2)
                        {
                            container = Instantiate(_buildable, hitGameObject.transform.position,
                                hitGameObject.transform.rotation);
                            if (!_hit.collider.gameObject.transform.parent.gameObject.name.Equals("Wall"))
                            {
                                container.transform.Rotate(0, 90, 0);
                            }
                            container.transform.SetParent(hitGameObject.transform.parent);
                        }
                        else
                        {
                            container = Instantiate(_buildable, hitGameObject.transform.position,
                                hitGameObject.transform.rotation);
                            container.transform.SetParent(hitGameObject.transform.parent);
                        }
                    }
                    // neither "buildable" nor "frame", "buildable" is placed
                    else
                    {
                        // If floor is active
                        if (mode == 0)
                        {
                            var instance = Instantiate(_buildable, SnapToGrid(_hit.point), transform.rotation);
                        }
                        // If ramp is active
                        else if (mode == 1)
                        {
                            var instance = Instantiate(_buildable, SnapToGrid(_hit.point), transform.rotation);
                            instance.transform.Rotate(0, 90, -45);
                            instance.transform.Translate(-2.5f, 0, 0);
                        }
                        // If wall is active
                        else if (mode == 2)
                        {
                            var instance = Instantiate(_buildable, SnapToGrid(_hit.point), transform.rotation);
                            instance.transform.Rotate(-90, 0, 0);
                            instance.transform.Translate(0, 0, 2.5f);
                        }
                        else
                        {
                            Instantiate(_buildable, SnapToGrid(_hit.point), transform.rotation);
                        }
                    }
                }
                else
                {
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


        // Returns the current BuildMode (which buildable is selected)
        public int GetCurrentBuildMode()
        {
            return mode;
        }

        private Vector3 SnapToGrid(Vector3 position)
        {
            var newX = Mathf.Round(position.x);
            var newY = Mathf.Round(position.y);
            var newZ = Mathf.Round(position.z);

            return new Vector3(newX, newY, newZ);
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