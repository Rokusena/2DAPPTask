using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public TextMeshProUGUI descriptioText;
    public TextMeshProUGUI dateText;
    public Image checkboxImagine;

    public Sprite doneSprite;
    public Sprite notDoneSprite;

    [HideInInspector]
    public Task task;

    public void SetTask(Task task)
    {
        this.task = task;

        descriptioText.text = task.Description;
        dateText.text = task.Date;
        checkboxImagine.sprite = task.Finished ? doneSprite : notDoneSprite;
    }

    public void ChangeStatus()
    {
        task.Finished = !task.Finished;

        checkboxImagine.sprite = task.Finished ? doneSprite : notDoneSprite;
    }
}
