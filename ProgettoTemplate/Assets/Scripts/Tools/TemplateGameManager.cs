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
    public GameObject Environment
    {
        get { return environment; }
        private set { environment = value; }
    }
    public bool GameIsInPause { get; protected set; }


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

    public virtual void ResumeGame()
    {

        PauseEvent.Invoke(false);
        GameIsInPause = false;

        foreach (IPausable pausable in environment.GetComponentsInChildren<IPausable>(true))
            pausable.Pause(false);
    }

    public virtual void PauseGame()
    {
        PauseEvent.Invoke(true);
        GameIsInPause = true;

        foreach (IPausable pausable in environment.GetComponentsInChildren<IPausable>(true))
        {
            if (pausable.IsPausable)
            {
                pausable.Pause(true);
            }              
        }
            
    }

    public void EndGame()
    {
        PauseGame();
        EndEvent.Invoke();
    }

    //
    public virtual void UpdateScoreEndGame()
    {
        throw new System.NotImplementedException();
    }

    //PAUSA
    //PREMENDO IL TASTO CHE SPEGNE LA SPADA => INPUT PAUSA ok
    //NEMICI NON VENGONO PIU' SPAWNATI ok
    //FERMARE IN ARIA I NEMICI IN SCENA ok
    //DISABILITARE RAYCAST MANO (ATTIVARE SEMPRE RECUPERO SPADA DOPO 5S RIMETTE SUL PIEDISTALLO) da fare


    //PAUSA ATTIVATA, PREMO IL TASTO CHE ACCENDA LA SPADA => INPUT RESUME
    //SPAWN NEMICI RIPRENDE DA DOVE SI ERA FERMATO ok
    //NEMICI IN ARIA RIATTIVATI ok
    //RIATTIVARE RAYCAST MANO da fare
}
