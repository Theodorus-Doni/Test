using System;
using UnityEngine;

public class CameraService : MonoBehaviour
{
    private Camera _mainCamera;

    private Func<Vector3> _getPlayerPosition;

    private void LateUpdate() =>
        _mainCamera.transform.position = _getPlayerPosition();

    public void Init(Func<Vector3> getPlayerPosition)
    {
        _mainCamera = Camera.main;
        _getPlayerPosition = getPlayerPosition;
    }
}
