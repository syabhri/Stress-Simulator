using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public StringListContainer ResolutionOptions;
    public IntContainer CurrentResolutionIndex;
    public IntContainer DefaultResolutionIndex;

    public BoolContainer isFullScreen;

    private Resolution[] resolutions;

    private void OnEnable()
    {
        resolutions = Screen.resolutions;

        ResolutionOptions.Value.Clear();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            ResolutionOptions.Value.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                CurrentResolutionIndex.Value = i;
            }

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                DefaultResolutionIndex.Value = i;
            }
        }

        isFullScreen.Value = Screen.fullScreen;

        CurrentResolutionIndex.OnValueChanged += setResolution;
        isFullScreen.OnValueChanged += SetFullScreen;
    }

    private void OnDestroy()
    {
        CurrentResolutionIndex.OnValueChanged -= setResolution;
        isFullScreen.OnValueChanged -= SetFullScreen;
    }

    public void setResolution(IntContainer resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex.Value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Resolution Changed " + resolution.width + "x" + resolution.height);
    }

    public void SetFullScreen(BoolContainer isFullScreen)
    {
        Screen.fullScreen = isFullScreen.Value;
    }
}