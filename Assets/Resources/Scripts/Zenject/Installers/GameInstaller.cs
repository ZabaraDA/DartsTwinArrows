using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private GameView _view;

    public override void InstallBindings()
    {
        Container.Bind<ICollection<IWeaponTypeModel>>().FromInstance(GetWeaponTypeModel());
        Container.Bind<ICollection<ILevelModel>>().FromInstance(GetLevelModels());

        Container.Bind<IGameView>().FromInstance(_view).AsSingle();
        Container.Bind<IGameModel>().To<GameModel>().AsSingle();
        Container.Bind<IGamePresenter>().To<GamePresenter>().AsSingle();
    }

    private ICollection<ILevelModel> GetLevelModels()
    {
        ICollection<ILevelModel> levelModels = new List<ILevelModel>();

        foreach (var levelData in JsonReaderService.ReadJsonInResources<ICollection<LevelData>>("Json/Levels"))
        {
            ILevelModel levelModel = new LevelModel();
        }

        return levelModels;
    }
    private ICollection<IWeaponTypeModel> GetWeaponTypeModel()
    {
        ICollection<IWeaponTypeModel> weaponTypeModels = new List<IWeaponTypeModel>();
        


        return weaponTypeModels;
    }
}