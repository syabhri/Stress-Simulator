using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Properties")]
    public bool IsPlaying;
    public TimeFormat startTime;
    public TimeFormat endTime;

    [Header("External Variables")]
    public PlayerData playerData;
    public GameData gameData;
    public TimeContainer StartTime;
    public TimeContainer EndTime;
    public TimeContainer CurrentTime;
    public Vector2Variable DefaultSpawnPoint;

    [Header("UI Output")]
    public StringVariable NoticeText;
    public FloatVariable LoadingProgress;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        // set input group to 0;
        if (IsPlaying)
        {
            //AssignAvatar();
            SpawnPlayer(playerData.playerPosition.position);
        }
    }
    #endregion

    #region GameManager function
    public void LoadScene(int sceneIndex)
    { 
        StartCoroutine(LoadAsynchronously(sceneIndex));
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

            LoadingProgress.SetValue(progress);

            yield return null;
        }
    }

    public void NewGame() 
    {
        // Set Starting Stat
        playerData.stressLevel.SetValue(0);
        playerData.energy.SetValue(100);
        playerData.coins.SetValue(30);

        //set Starting Knowlage knowlage
        foreach (FloatVariable knowlege in playerData.knowleges)
        {
            knowlege.SetValue(0);
        }

        // set player default spawn position
        playerData.playerPosition.position = DefaultSpawnPoint.position;

        // set game duration
        StartTime.time.SetValue(startTime);
        EndTime.time.SetValue(endTime);
        
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SaveGame(StringVariable saveName)
    {
        SaveData saveData = new SaveData(playerData, CurrentTime.time);
        SaveManager.Save<SaveData>(saveData, saveName.Value);
    }

    public void LoadGame(StringVariable saveName)
    {
        SaveData saveData = SaveManager.Load<SaveData>(saveName.Value);

        StartTime.time.minutes = saveData.current_time[0];
        StartTime.time.hours = saveData.current_time[1];
        StartTime.time.days = saveData.current_time[2];

        playerData.characterName.Value = saveData.character_name;

        foreach (GameObject avatar in gameData.Avatars)
        {
            if (avatar.name == saveData.avatar)
                playerData.avatar = avatar;
        }

        playerData.playerPosition.position.x = saveData.player_position[0];
        playerData.playerPosition.position.y = saveData.player_position[1];

        playerData.stressLevel.value = saveData.stress_level;
        playerData.energy.value = saveData.energy;
        playerData.coins.value = saveData.coins;

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
        ChangeScene(0);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void SpawnPlayer(Vector2 position)
    {
        Instantiate(playerData.avatar, new
            Vector3(position.x, position.y, 0),
            Quaternion.identity);
        Debug.Log("player spawed");
    }
    #endregion
}
