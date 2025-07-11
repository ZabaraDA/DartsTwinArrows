using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private GameView _view;

    public override void InstallBindings()
    {
        //Container.Bind<ICollection<IWeaponTypeModel>>().FromInstance(GetWeaponTypeModel());
        Container.Bind<ICollection<ILevelModel>>().FromInstance(GetLevelModels());

        Container.Bind<IGameView>().FromInstance(_view).AsSingle();
        Container.Bind<IGameModel>().To<GameModel>().AsSingle();
        Container.Bind<IGamePresenter>().To<GamePresenter>().AsSingle();
    }

    private ICollection<ILevelModel> GetLevelModels()
    {
        List<IWeaponTypeModel> weaponTypeModels = GetWeaponTypeModels();
        List<IEnemyTypeModel> enemyTypeModels = GetEnemyTypeModels();
        List<ILevelModel> levelModels = new List<ILevelModel>();

        foreach (var levelData in JsonReaderService.ReadJsonInResources<ICollection<LevelData>>("Json/Levels"))
        {
            ILevelModel levelModel = new LevelModel(levelData.Number, _view.GetWeaponSpawnPosition().position, weaponTypeModels[levelData.WeaponId - 1], enemyTypeModels.FirstOrDefault(x => x.Number == levelData.EnemyId), levelData.EnemyCount, levelData.SpawnAtCenter, levelData.SpawnCount);
            levelModels.Add(levelModel);
        }

        return levelModels;
    }
    private List<IWeaponTypeModel> GetWeaponTypeModels()
    {
        Sprite[] weaponSpritesInAtlas = Resources.LoadAll<Sprite>("Images/Weapons");
        List<IProjectileTypeModel> projectileTypeModels = GetProjectileTypeModels();
        List<IWeaponTypeModel> weaponTypeModels = new List<IWeaponTypeModel>();

        foreach (var weaponData in JsonReaderService.ReadJsonInResources<ICollection<WeaponData>>("Json/WeaponTypes"))
        {
            IWeaponTypeModel weaponTypeModel = new WeaponTypeModel(weaponData.Number, weaponData.Name, weaponData.ProjectileSpawnCount, weaponData.ProjectileLaunchDelay, weaponSpritesInAtlas[weaponData.SpriteNumber - 1], projectileTypeModels[weaponData.ProjectileNumber - 1]);
            weaponTypeModels.Add(weaponTypeModel);
        }

        return weaponTypeModels;
    }

    private List<IProjectileTypeModel> GetProjectileTypeModels()
    {
        Sprite[] projectileSpritesInAtlas = Resources.LoadAll<Sprite>("Images/Projectiles");
        List<IProjectileTypeModel> projectileTypeModels = new List<IProjectileTypeModel>();

        foreach (var projectileData in JsonReaderService.ReadJsonInResources<ICollection<ProjectileData>>("Json/ProjectileTypes"))
        {
            IProjectileTypeModel projectileTypeModel = new ProjectileTypeModel(projectileData.Number, projectileData.Name, projectileData.Speed, projectileSpritesInAtlas[projectileData.SpriteNumber - 1], projectileData.Damage);
            projectileTypeModels.Add(projectileTypeModel);
        }

        return projectileTypeModels;
    }
    private List<IEnemyTypeModel> GetEnemyTypeModels()
    {
        Sprite[] enemiesSpritesInAtlas = Resources.LoadAll<Sprite>("Images/Enemies");
        List<IEnemyTypeModel> enemyTypeModels = new List<IEnemyTypeModel>();

        foreach (var enemyData in JsonReaderService.ReadJsonInResources<ICollection<EnemyData>>("Json/EnemyTypes"))
        {
            IEnemyTypeModel projectileTypeModel = new EnemyTypeModel(enemyData.Number, enemyData.Name, enemyData.Healts, enemyData.Speed, enemyData.IsStatic, enemyData.Points, enemiesSpritesInAtlas[enemyData.SpriteNumber - 1], enemyData.SizeMultiplier);
            enemyTypeModels.Add(projectileTypeModel);
        }

        return enemyTypeModels;
    }
}