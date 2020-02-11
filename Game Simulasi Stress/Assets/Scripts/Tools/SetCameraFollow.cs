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
            SetFollow(player);

        player.OnValueChanged += SetFollow;
    }

    private void OnDestroy()
    {
        player.OnValueChanged -= SetFollow;
    }

    public void SetFollow()
    {
        virtualCamera.Follow = player.Value.transform;
    }

    public void SetFollow(VariableContainer<GameObject> target)
    {
        virtualCamera.Follow = target.Value.transform;
    }
}
