using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class TemplateStartEvent : UnityEvent{ }
public class TemplatePauseEvent : UnityEvent<bool>{ }
public class TemplateEndEvent : UnityEvent { }


/// <summary>
/// Template di un GameManager per la gestione degli eventi del gioco
/// </summary>
public class TemplateGameManager : Singleton<TemplateGameManager>
{
   
    [SerializeField]
    protected GameObject environment;


    public TemplateStartEvent StartEvent { get; protected set; } = new TemplateStartEvent();
    public TemplatePauseEvent PauseEvent { get; protected set; } = new TemplatePauseEvent();
    public TemplateEndEvent EndEvent { get; protected set; } = new TemplateEndEvent();

   

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        StartEvent.Invoke();
    }

    public void ResumeGame()
    {
        PauseEvent.Invoke(false);
        foreach (IPausable pausable in environment.GetComponentsInChildren<IPausable>(true))
            pausable.Pause(false);
    }

    public void PauseGame()
    {
        PauseEvent.Invoke(true);
        foreach (IPausable pausable in environment.GetComponentsInChildren<IPausable>(true))
            if(pausable.IsPausable) pausable.Pause(true);
    }

    public void EndGame()
    {
        PauseGame();
        EndEvent.Invoke();
    }

}
