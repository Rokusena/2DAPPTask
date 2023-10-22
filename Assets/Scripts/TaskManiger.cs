using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using System.Linq;

public class TaskManiger : MonoBehaviour
{
   public TaskUI prefab;
    public Transform taskParent;

    public TMP_InputField descriptionInput;
    public Toggle one;
    public Toggle two;
    public Toggle thrid;

    private List<Task> tasks = new List<Task>();


    private void Start()
    {
        LoadFromJson();
    }
    public void GetInput()
    {

        if (string.IsNullOrWhiteSpace(descriptionInput.text)) return;

        string description = descriptionInput.text;
   
        int priority;
        string date = DateTime.Now.ToString("yyyy- MM - dd");
        if(one.isOn)
        {
            priority = 1;    
        }
        else if(two.isOn)
        {
            priority = 2;
        }
        else if (thrid.isOn)
        {
            priority = 3;
        }
        else
        {
            priority = 4;
        }

        Task task = new Task()
        {
            Description = description,
            Date = date,
            Finished = false,
            Priority = priority

        };
      
        AddTask(task);
        descriptionInput.text = "";
            

    }
    public void AddTask(Task task)
    {
        tasks.Add(task);
        TaskUI taskUi = Instantiate(prefab, taskParent);
        taskUi.SetTask(task);
        ReloadTasksByPriority();
    }

    public void SaveToJason()
    {
        if (tasks.Count == 0) return;
        List<Task> list = new();
        foreach(var taskUI in tasks)
        {
            //list.Add(taskUI.task);
           
        }

       string json = JsonUtility.ToJson(list);
       
        
        string path = Path.Combine(Application.persistentDataPath, "tasks.json");
        File.WriteAllText(path, json);


    }
    public void LoadFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "tasks.json");

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var taskList = JsonUtility.FromJson<List<Task>>(json);

            foreach(var task in taskList)
            {
                AddTask(task);
            }
               
        }
    }
    public void SortTasksByPriority()
    {
        tasks = tasks.OrderBy(task => task.Priority).ToList();
    }
    private void OnApplicationQuit()
    {
        SaveToJason();
    }
    public void ReloadTasksByPriority()
    {
       
        SortTasksByPriority();

       
        foreach (Transform child in taskParent)
        {
            Destroy(child.gameObject);
        }

        
        foreach (var task in tasks)
        {
            TaskUI taskUi = Instantiate(prefab, taskParent);
            taskUi.SetTask(task);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        SaveToJason();
    }
}
