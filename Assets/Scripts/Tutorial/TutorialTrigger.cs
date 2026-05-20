using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TutorialTrigger: MonoBehaviour{
    [SerializeField] TutorialContextSO context;
    TutorialInstaller installer;
    PlayerController player;
    Collider _collider;

    void Start()
    {
        player = Registry<PlayerController>.GetFirst();
        installer = new TutorialInstaller(player);
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            installer.Install(context);
            Destroy(gameObject);
        }
    }


}