using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    public GameObjectContainer player;

    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (player.Value != null)
            SetFollow(player.Value);

        player.OnValueChanges += SetFollow;
    }

    private void OnDestroy()
    {
        player.OnValueChanges -= SetFollow;
    }

    public void SetFollow()
    {
        virtualCamera.Follow = player.Value.transform;
    }

    public void SetFollow(GameObject target)
    {
        virtualCamera.Follow = target.transform;
    }
}
