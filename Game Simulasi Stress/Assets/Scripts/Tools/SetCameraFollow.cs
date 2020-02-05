using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    public ThingRuntimeSet player;
    public GameObjectContainer playerr;

    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (playerr.Value != null)
            SetFollow(playerr.Value);

        playerr.OnValueChanges += SetFollow;

        SetFollow();

        GameObjectContainer gameObjectContainer = new GameObjectContainer();

        playerr.ValueExt = gameObjectContainer;
    }

    public void SetFollow()
    {
        virtualCamera.Follow = player.Item.transform;
    }

    public void SetFollow(GameObject target)
    {
        virtualCamera.Follow = target.transform;
    }
}
