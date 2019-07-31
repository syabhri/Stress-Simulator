using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableFix : MonoBehaviour
{
    public ThingRuntimeSet timelineManager;
    public PlayableDirector director;
    public GameEvent OnPlayableFix;

    public bool fix = false;

    private void Start()
    {
        if (director == null)
        {
            director = GetComponent<PlayableDirector>();
        }
    }

    private void Update()
    {
        if (director.state != PlayState.Playing && !fix)
        {
            fix = true;
            OnPlayableFix.Raise();
        }
        if (director.state == PlayState.Playing)
        {
            fix = false;
        }
    }
}
