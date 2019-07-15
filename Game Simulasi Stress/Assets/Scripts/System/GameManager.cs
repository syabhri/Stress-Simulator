using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    #region Variables and Class Reverences
    [Header("Properties")]
    public Vector2Variable DefaultSpawnPoint;
    public TimeContainer GameTimeUp;

    [Header("Reference")]
    public PlayerData playerData;
    public TimeContainer CurrentTime;

    [Header("Conditions")]
    public BoolVariable IsPlaying;

    #endregion

    #region Unity Event Function
    private void Start()
    {
        if (IsPlaying.value)
        {
            //SpawnPlayer(playerData.playerPosition.position);
        }
    }

    private void Update()
    {
        if (IsPlaying.value)
        {
            CheckEndGame();
        }
    }

    private void OnApplicationQuit()
    {
        IsPlaying.value = false;
    }
    #endregion

    #region GameManager function
    public void PlayGame()
    {
        IsPlaying.value = true;
        ChangeScene("GameplayScene");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public static void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void SpawnPlayer(Vector2 position)
    {
        Instantiate<GameObject>(playerData.avatar, new
            Vector3(position.x, position.y, 0),
            Quaternion.identity);
        Debug.Log("player spawed");
    }

    public void CheckEndGame()
    {
        if (CurrentTime.time.days >= GameTimeUp.time.days)
        {
            if (CurrentTime.time.hours >= GameTimeUp.time.hours)
            {
                if (CurrentTime.time.minutes >= GameTimeUp.time.minutes)
                {
                    IsPlaying.value = false;
                    ChangeScene(2);
                }
            }
        }
    }
    #endregion
}
