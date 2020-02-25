using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class SaveSlot : MonoBehaviour
{
    public string saveName;
    public GameData gameData;

    public Image image;
    public TextMeshProUGUI charaterName;
    public TextMeshProUGUI days;

    [Space]
    public UnityEvent OnSaveExist;
    public UnityEvent OnSaveEmpty;

    public SaveData saveData;

    private void OnEnable()
    {
        updateInformation();
    }

    public void updateInformation()
    {
        if (!SaveManager.SaveExists(saveName))
        {
            OnSaveEmpty.Invoke();
            return;
        }

        saveData = SaveManager.Load<SaveData>(saveName);

        foreach (GameObject avatar in gameData.Avatars)
        {
            if (avatar.name == saveData.avatar)
            {
                image.sprite = avatar.GetComponent<SpriteRenderer>().sprite;
            }
        }

        charaterName.SetText(saveData.character_name);
        days.SetText("Hari Ke " + saveData.play_time.days.ToString());

        OnSaveExist.Invoke();
    }
}
