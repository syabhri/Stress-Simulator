using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public StringListVariable ResolutionOptions;
    public IntVariable CurrentResolutionIndex;
    public IntVariable DefaultResolutionIndex;

    public BoolVariable isFullScreen;

    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionOptions.Values.Clear();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            ResolutionOptions.Values.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                CurrentResolutionIndex.SetValue(i);
            }

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                DefaultResolutionIndex.SetValue(i);
            }
        }

        isFullScreen.SetBool(Screen.fullScreen);

        CurrentResolutionIndex.OnValueChange += setResolution;
        isFullScreen.OnValueChange += SetFullScreen;
    }

    private void OnDestroy()
    {
        CurrentResolutionIndex.OnValueChange -= setResolution;
        isFullScreen.OnValueChange -= SetFullScreen;
    }

    public void setResolution(IntVariable resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Resolution Changed " + resolution.width + "x" + resolution.height);
    }

    public void SetFullScreen(BoolVariable isFullScreen)
    {
        Screen.fullScreen = isFullScreen.value;
    }
}
