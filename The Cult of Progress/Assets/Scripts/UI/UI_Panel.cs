using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Collections.Generic;

[ExecuteAlways]
public class UI_Panel : DEBUGMonoBehaviour
{
    [SerializeField][Label("Allow Open Panel Requests?")] private bool AllowOpenPanelRequests;
    [SerializeField][Label("Allow Close Panel Requests?")] private bool AllowClosePanelRequests;

    [Space(10)]

    [ShowIf(EConditionOperator.Or, "debug", "AllowOpenPanelRequests")]
    public UnityEvent OnPanelOpenRequest;

    [Space(10)]

    [ShowIf(EConditionOperator.Or, "debug", "AllowClosePanelRequests")]
    public UnityEvent OnPanelCloseRequest;

    private static List<UI_Panel> allPanels = new List<UI_Panel>(){};

    [Label("Does Panel Start Open?")] public bool isPanelOpen;

    public void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        if (!allPanels.Contains(this)){ allPanels.Add(this); }
    }

    [Button]
    public void OpenPanel()
    {
        if (!AllowOpenPanelRequests && !debug){ return; }

        if (debug){ Debug.Log(name + " told to Open its Panel!"); }

        OnPanelOpenRequest.Invoke();

        isPanelOpen = true;
    }

    [Button]
    public void ClosePanel()
    {
        if (!AllowClosePanelRequests && !debug){ return; }

        if (debug){ Debug.Log(name + " told to Close its Panel!"); }

        OnPanelCloseRequest.Invoke();

        isPanelOpen = false;
    }

    public static void CloseAllPanels()
    {
        foreach (var instance in UI_Panel.allPanels)
        {
            instance.ClosePanel();
        }
    }

    public static void OpenAllPanels()
    {
        foreach (var instance in UI_Panel.allPanels)
        {
            instance.OpenPanel();
        }
    }

    public static bool QueryIsAnyPanelOpen()
    {
        foreach (var instance in UI_Panel.allPanels)
        {
            if (instance.isPanelOpen){ return true; }
        }

        return false;
    } 
}