using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    #region Variables
    [Header("Properties")]
    public bool IsPlaying;
    public TimeFormat starTime;
    public TimeFormat endTime;

    [Header("External Variables")]
    public PlayerData playerData;
    public GameData gameData;
    public TimeContainer StarTime;
    public TimeContainer EndTime;
    public TimeContainer CurrentTime;
    public Vector2Variable DefaultSpawnPoint;

    [Header("Reference")]
    public ThingRuntimeSet noticePanel;
    public ThingRuntimeSet LoadingPanel;
    public ThingRuntimeSet Player;

    [Header("UI Output")]
    public StringVariable NoticeText;
    public FloatVariable LoadingProgress;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        // set input group to 0;

        //playerSprite = Player.Item.GetComponent<SpriteRenderer>();
        //playerAnimator = Player.Item.GetComponent<Animator>();
        if (IsPlaying)
        {
            //AssignAvatar();
            SpawnPlayer(playerData.playerPosition.position);
        }

    }

    private void Update()
    {
        if (IsPlaying)
        {
            CheckEndGame();
        }
    }
    #endregion

    #region GameManager function
    public void PlayGame(int sceneIndex)
    { 
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingPanel.Item.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            Debug.Log(progress);

            LoadingProgress.value = progress;

            yield return null;
        }
    }

    public void NewGame(int sceneIndex) 
    {
        //set Starting Knowlage knowlage
        foreach (FloatVariable knowlege in playerData.knowleges)
        {
            knowlege.SetValue(0);
        }

        // Set Starting Stat
        playerData.stressLevel.SetValue(0);
        playerData.energy.SetValue(100);
        playerData.coin.SetValue(30);

        // set player default spawn position
        playerData.playerPosition.position = DefaultSpawnPoint.position;

        // set game duration
        StarTime.time.SetValue(starTime);
        EndTime.time.SetValue(endTime);
        
        PlayGame(sceneIndex);
    }

    public void LoadGame()
    {
        Debug.LogWarning("Function not implemented yet");
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

    public void QuitToMEnu()
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
        Instantiate<GameObject>(playerData.Avatar, new
            Vector3(position.x, position.y, 0),
            Quaternion.identity);
        Debug.Log("player spawed");
    }

    public void CheckEndGame()
    {
        if (CurrentTime.time.days >= EndTime.time.days)
        {
            if (CurrentTime.time.hours >= EndTime.time.hours)
            {
                if (CurrentTime.time.minutes >= EndTime.time.minutes)
                {
                    ChangeScene(2);
                }
            }
        }
    }
    #endregion
}
