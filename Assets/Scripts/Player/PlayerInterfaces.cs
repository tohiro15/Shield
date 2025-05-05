using UnityEngine;

public interface IMovable
{
    void Movement();
}

public interface IShield
{
    void Initialize();
    void RotateAroundPlayer();
    void SetDefaultMaterial();
    void LoadBullet();
}

public interface IFire
{
    void Fire();
}
