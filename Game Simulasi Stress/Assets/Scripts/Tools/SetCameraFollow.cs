using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    public ThingRuntimeSet player;

    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
        SetFollow();
    }

    public void SetFollow()
    {
        virtualCamera.Follow = player.Item.transform;
    }
}
