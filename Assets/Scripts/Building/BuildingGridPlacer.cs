using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingGridPlacer : BuildingPlacer
{
    [Header("Grid Settings")]
    public float cellSize;
    public Vector2 gridOffset;

    public Renderer gridRenderer;

    [Header("Raycast Settings")]

    public new GameObject raycastOriginObject;

#if UNITY_EDITOR
    private void OnValidate() {
        UpdateGridVisual();
    }
#endif

    private void Start() {
        UpdateGridVisual();
        EnableGridVisual(false);
    }

    private void Update() {
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

    protected override void PrepareBuilding() {
        base.PrepareBuilding();
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
