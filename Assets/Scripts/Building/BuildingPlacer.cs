using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance;

    //Layer
    public LayerMask groundLayer;

    //Building
    protected GameObject _buildingPrefab;
    protected GameObject _toBuild;

    protected GameObject raycastOriginObject;

    // Raycast
    protected Ray _ray;
    protected RaycastHit _hit;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
        _buildingPrefab = null;
    }

    private void Update() {
        if (_buildingPrefab != null) {

            // Right Click on Mouse exit building mode
            if (Input.GetMouseButtonDown(1)) {
                Destroy(_toBuild);
                _buildingPrefab = null;
                _toBuild = null;
                return;
            }

            // Rotate preview with Spacebar
            if (Input.GetKeyDown(KeyCode.Space)) {
                _toBuild.transform.Rotate(Vector3.up, 90f);
            }

            _ray = new Ray(raycastOriginObject.transform.position, Vector3.down);
            if (Physics.Raycast(_ray, out _hit, 1000f, groundLayer)) {
                if (!_toBuild.activeSelf) {
                    _toBuild.SetActive(true);
                }
                Vector3 hitPoint = _hit.point;
                hitPoint.y = 0;
                _toBuild.transform.position = hitPoint;

                // Left Click on Mouse place building
                if (Input.GetMouseButtonDown(0)) {
                    BuildingManager m = _toBuild.GetComponent<BuildingManager>();
                    if (m.hasValidPlacement) {
                        m.SetPlacementMode(PlacementMode.Fixed);
                    
                        // Exit building mode
                        _buildingPrefab = null;
                        _toBuild = null;
                    }
                }
            } else if (_toBuild.activeSelf) {
                _toBuild.SetActive(false);
            }
        }
    }

    public void SetBuildingPrefab(GameObject building) {
        _buildingPrefab = building;
        PrepareBuilding();
        EventSystem.current.SetSelectedGameObject(null);
    }   

    protected virtual void PrepareBuilding() {
        if (_toBuild) {
            Destroy(_toBuild);
        }

        _toBuild = Instantiate(_buildingPrefab);
        _toBuild.SetActive(false);

        BuildingManager m = _toBuild.GetComponent<BuildingManager>();

        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }
}
