using UnityEngine;

public class WeaponFactory : IWeaponFactory
{
    private IWeaponLifeCycleManager _manager; // ������ �� ��������
    private IProjectileFactory _projectileFactory; // ������ �� ��������

    public WeaponFactory(IWeaponLifeCycleManager manager, IProjectileFactory projectileFactory)
    {
        _manager = manager;
        _projectileFactory = projectileFactory;
    }

    public IWeaponPresenter Create(IWeaponModel model)
    {
        //float angle = Mathf.Atan2(model.Direction.y, model.Direction.x) * Mathf.Rad2Deg;
        //Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        GameObject weaponPrefab = Resources.Load<GameObject>("Prefabs/Weapon");
        GameObject weapon = Object.Instantiate(weaponPrefab, model.Position, Quaternion.identity);

        IWeaponView view = weapon.GetComponent<IWeaponView>();

        if (view == null)
        {
            Object.Destroy(weapon);
            return null;
        }

        // 3. ������� Presenter, ��������� ��� View � Model, � �������� ���������� �����
        IWeaponPresenter presenter = new WeaponPresenter(view, model, _manager, _projectileFactory);
        presenter.Initialize(); // �������������� ���������

        return presenter;
    }
    public IWeaponPresenter Create(int id, Vector2 position, IWeaponTypeModel type)
    {
        IWeaponModel model = new WeaponModel(id, position, type);
        return Create(model);
    }
}
