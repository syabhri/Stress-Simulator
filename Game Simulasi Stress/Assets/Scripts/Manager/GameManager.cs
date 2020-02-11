using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Properties")]
    public bool IsPlaying;
    public TimeFormat startTime;
    public TimeFormat endTime;

    [Header("Data Container")]
    public PlayerData playerData;
    public GameData gameData;
    public Vector2Container DefaultSpawnPoint;
    public FloatContainer LoadingProgress;
    public TimeContainer TimePasser;

    [Header("Event")]
    public GameEvent SceneTransition;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        // set input group to 0;
        if (IsPlaying)
        {
            //AssignAvatar();
            SpawnPlayer(playerData.playerPosition.Value);
        }
    }
    #endregion

    #region SceneManager
    public void LoadScene(int sceneIndex)
    { 
        StartCoroutine(LoadAsynchronously(sceneIndex));
        SceneTransition.Raise();
    }

    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            Debug.Log(progress);

            LoadingProgress.Value = progress;

            yield return null;
        }
    }
    #endregion

    #region Menu Function
    public void NewGame() 
    {
        // Set Starting Stat
        playerData.stressLevel.Value = 0;
        playerData.energy.Value = 100;
        playerData.coins.Value = 30;

        //set Starting Knowlage knowlage
        foreach (FloatContainer knowlege in playerData.knowleges)
        {
            knowlege.Value = 0;
        }

        // set player default spawn position
        playerData.playerPosition.Value = DefaultSpawnPoint.Value;

        // set start time
        TimePasser.Value = startTime;
        
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SaveGame(StringContainer saveName)
    {
        //TimeFormat currentTime = new TimeFormat(TimeManager.currentDay)
        //SaveData saveData = new SaveData(playerData, new TimeFormat());
        //SaveManager.Save<SaveData>(saveData, saveName.Value);
    }

    public void LoadGame(StringContainer saveName)
    {
        SaveData saveData = SaveManager.Load<SaveData>(saveName.Value);

        TimePasser.Value.minutes = saveData.current_time[0];
        TimePasser.Value.hours = saveData.current_time[1];
        TimePasser.Value.days = saveData.current_time[2];

        playerData.characterName.Value = saveData.character_name;

        foreach (GameObject avatar in gameData.Avatars)
        {
            if (avatar.name == saveData.avatar)
                playerData.avatar = avatar;
        }

        playerData.playerPosition.SetPosition
            (saveData.player_position[0], saveData.player_position[1]);

        playerData.stressLevel.Value = saveData.stress_level;
        playerData.energy.Value = saveData.energy;
        playerData.coins.Value = saveData.coins;

        StartCoroutine(LoadAsynchronously(1));
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("GamePaused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("GamePaused");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitToMenu()
    {
        LoadScene(0);
    }
    #endregion

    #region Misc
    public void SpawnPlayer(Vector2 position)
    {
        Instantiate(playerData.avatar, new
            Vector3(position.x, position.y, 0),
            Quaternion.identity);
        Debug.Log("player spawed");
    }

    public void CheckEndGame()
    {
        if (TimeManager.currentDay >= endTime.days)
        {
            LoadNextScene();
        }
    }
    #endregion
}
