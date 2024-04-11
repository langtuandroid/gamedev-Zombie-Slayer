using UnityEngine;
using Zenject;

namespace Script.Installers
{
    public class DontDestroyOnLoadInstaller : MonoInstaller
    {
        [SerializeField] private GameObject gameModePref;
        public override void InstallBindings()
        {
            var gameMode = Instantiate(gameModePref);
            Container.Bind<GameModeZS>().FromComponentOn(gameMode).AsSingle().NonLazy();
        }
    }
}
