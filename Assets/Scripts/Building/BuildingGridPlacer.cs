using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class towerType {
    public GameObject towerPrefab;
}

public class BuildingGridPlacer : BuildingPlacer
{
    [Header("Grid Settings")]
    public float cellSize;
    public Vector2 gridOffset;

    public Renderer gridRenderer;

    [Header("Raycast Settings")]

    public new GameObject raycastOriginObject;

    // list of Towers
    public List<towerType> towers = new List<towerType>();

#if UNITY_EDITOR
    private void OnValidate() {
        UpdateGridVisual();
    }
#endif

    private void Start() {
        for (int i = 0; i < towers.Count; i++) {
            GameObject tower = towers[i].towerPrefab;
            print(towers[i].towerPrefab.name);
        }
        UpdateGridVisual();
        EnableGridVisual(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetBuildingPrefab(0);
        }

        if (_buildingPrefab != null) {
            // Right Click on Mouse exit building mode
            if (Input.GetMouseButtonDown(1)) {
                Destroy(_toBuild);
                _buildingPrefab = null;
                _toBuild = null;
                EnableGridVisual(false);
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
                _toBuild.transform.position = ClampToNearest(hitPoint, cellSize);

                // Left Click on Mouse place building
                if (Input.GetMouseButtonDown(0)) {
                    BuildingManager m = _toBuild.GetComponent<BuildingManager>();
                    if (m.hasValidPlacement) {
                        m.SetPlacementMode(PlacementMode.Fixed);
                        print("Fixed");
                        // Exit building mode
                        _buildingPrefab = null;
                        _toBuild = null;
                        EnableGridVisual(false);
                    }
                }
            } else if (_toBuild.activeSelf) {
                _toBuild.SetActive(false);
            }
        }
    }

    private void SetBuildingPrefab(int index) {
        if (index < 0 || index >= towers.Count) return;

        _buildingPrefab = towers[index].towerPrefab;
        PrepareBuilding();
        EventSystem.current.SetSelectedGameObject(null);
    }

    protected override void PrepareBuilding() {
        if (_toBuild) {
            Destroy(_toBuild);
        }

        _toBuild = Instantiate(_buildingPrefab);
        print(_toBuild.name);
        _toBuild.SetActive(false);

        BuildingManager m = _toBuild.GetComponent<BuildingManager>();

        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
        EnableGridVisual(true);
    }

    private Vector3 ClampToNearest(Vector3 position, float threshold) {
        float t = 1f / threshold;
        Vector3 v = ((Vector3)Vector3Int.FloorToInt(position * t)) / t;

        // Offset to center of cell
        float s = threshold / 2.0f;
        v.x += s + gridOffset.x;
        v.z += s + gridOffset.y;

        return v;
    }

    private void EnableGridVisual(bool on) {
        if (gridRenderer == null) return;
        gridRenderer.gameObject.SetActive(on);
    }

    private void UpdateGridVisual() {
        if (gridRenderer == null) return;
        gridRenderer.sharedMaterial.SetVector("_Cell_Size", new Vector4(cellSize, cellSize, 0, 0));
    }
}
