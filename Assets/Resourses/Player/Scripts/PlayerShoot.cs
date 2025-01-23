using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _firePoint;
    [SerializeField] float _bulletSpeed = 10.0f;
    [SerializeField] float _bulletTTL = 3.0f;

    [SerializeField] Text _ammoText;
    [SerializeField] int _currentAmmo = 30;
    [SerializeField] int _maxAmmo = 30;
    [SerializeField] float _reloadTime = 2.0f;
    private bool _isReloading = false;

    public int CurrentAmmo
    {
        get => _currentAmmo;
        set => _currentAmmo = value;
    }
    public int MaxAmmo => _maxAmmo;
    public bool IsReloading
    {
        set => _isReloading = value;
    }


    void Start()
    {
        UpdateAmmoUI();
    }

    public void Shoot()
    {
        if (_isReloading)
            return;

        if (_currentAmmo > 0)
        {

            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = _firePoint.right * _bulletSpeed;
            }
            
            Destroy(bullet, _bulletTTL);

            _currentAmmo--;
            UpdateAmmoUI();

        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    public void UpdateAmmoUI()
    {
        _ammoText.text = "Пули: " + _currentAmmo.ToString() + "/" + _maxAmmo.ToString();
    }

    IEnumerator Reload()
    {
        if (_isReloading) yield break;

        _isReloading = true;

        yield return new WaitForSeconds(_reloadTime);

        _currentAmmo = _maxAmmo;
        UpdateAmmoUI();
        _isReloading = false;
    }

    public void ReloadAfterDeath()
    {
        StartCoroutine(Reload());
    }
}
