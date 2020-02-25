using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Properties")]
    public bool IsPlaying;
    public TimeFormat StartTime;
    public int EndDay;

    [Header("Data Container")]
    public PlayerData playerData;
    public GameData gameData;

    [Header("Time Manager")]
    public TimeContainer CurrentTime;

    [Header("Scene Manager")]
    public GameEvent SceneTransition;
    public FloatContainer LoadingProgress;

    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (IsPlaying)
        {
            //AssignAvatar();
            SpawnPlayer(playerData.playerPosition.Value);
        }
        Time.timeScale = 1f;
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

            //Debug.Log(progress);

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
        playerData.playerPosition.Value = gameData.DefaultSpawnPoint.Value;

        // set start time
        CurrentTime.Value.SetValue(StartTime);
        
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SaveGame(StringContainer saveName)
    {
        SaveData saveData = new SaveData(playerData);
        SaveManager.Save(saveData, saveName.Value);
    }

    public void LoadGame(StringContainer saveName)
    {
        SaveData saveData = SaveManager.Load<SaveData>(saveName.Value);

        //load time
        CurrentTime.Value = saveData.play_time;

        // load Charater Name
        playerData.characterName.Value = saveData.character_name;

        //Load Character Avatar
        foreach (GameObject avatar in gameData.Avatars)
        {
            if (avatar.name == saveData.avatar)
                playerData.avatar = avatar;
        }

        //Load Position
        playerData.playerPosition.SetPosition
            (saveData.player_position.x, saveData.player_position.y);

        //Load Stats
        playerData.stressLevel.Value = saveData.stress_level;
        playerData.energy.Value = saveData.energy;
        playerData.coins.Value = saveData.coins;

        //Load Ability
        playerData.ability = gameData.Abilities.FirstOrDefault(a => a.name == saveData.ability);

        //Load Interest
        playerData.interest = gameData.Insterest.Where(i => saveData.interest.Any(l => i.name == l)).ToList();

        //load knowlage
        foreach (FloatContainer knowlage in playerData.knowleges)
        {
            knowlage.Value = saveData.knowleges.FirstOrDefault(k => k.name == knowlage.name).value;
        }

        LoadScene(1);

        //StartCoroutine(LoadAsynchronously(1));
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
        if (CurrentTime.Value.days >= EndDay)
        {
            LoadNextScene();
        }
    }
    #endregion
}
