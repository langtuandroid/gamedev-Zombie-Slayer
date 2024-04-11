using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuHomeSceneZS mainMenuHomeSceneZs;
        
    public override void InstallBindings()
    {
        Container.Bind<MainMenuHomeSceneZS>().FromInstance(mainMenuHomeSceneZs).AsSingle().NonLazy();
    }
}
