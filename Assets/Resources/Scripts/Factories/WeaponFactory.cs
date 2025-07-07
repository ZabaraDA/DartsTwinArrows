using UnityEngine;

public class WeaponFactory : IWeaponFactory
{
    private IWeaponLifeCycleManager _manager; // ������ �� ��������
    private IProjectileFactory _projectileFactory; // ������ �� ��������

    public WeaponFactory(IWeaponLifeCycleManager manager)
    {
        _manager = manager;
    }

    public IWeaponPresenter Create(IWeaponModel model)
    {
        float angle = Mathf.Atan2(model.Direction.y, model.Direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Projectile");
        GameObject projectile = Object.Instantiate(projectilePrefab, model.Position, quaternion);

        IWeaponView view = projectile.GetComponent<IWeaponView>(); // ���������, ��� ProjectileView ���������� � �������

        if (view == null)
        {
            Debug.LogError("������ ������� �� �������� ��������� ProjectileView ��� �� �� ��������� IProjectileView!");
            Object.Destroy(projectile);
            return null;
        }

        // 3. ������� Presenter, ��������� ��� View � Model, � �������� ���������� �����
        IWeaponPresenter presenter = new WeaponPresenter(view, model, _manager, _projectileFactory);
        presenter.Initialize(); // �������������� ���������

        return presenter;
    }
    public IWeaponPresenter Create(int id, string name, Vector2 position, Vector2 direction, IWeaponTypeModel type)
    {
        IWeaponModel model = new WeaponModel(id, name, position, direction, type);
        return Create(model);
    }
}
