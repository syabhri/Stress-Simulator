using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    public PlayerData playerData;

    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        SetFollow();
    }

    public void SetFollow()
    {
        virtualCamera.Follow = playerData.avatar.transform;
    }
}
