using System.Collections;
using UnityEngine;

public class PlayerCanon : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private GameObject _canon;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _fireRateMultiplier;
    [SerializeField] private float _zRotation;
    [SerializeField] private Transform _fire;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] Texture2D _cursorTexture;
    [SerializeField] GameObject _aimPoint;
    private void Start()
    {
        //Cursor.SetCursor(_cursorTexture, transform.position, CursorMode.Auto);
        Cursor.visible = false;
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        CanonRotation();
        Fire();
    }
    private void OnMouseEnter()
    {
        Cursor.visible=false;
    }
    private void CanonRotation()
    {
        // Convert mouse position to world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y,
        Input.mousePosition.z - Camera.main.transform.position.z));
        _aimPoint.transform.position = new Vector2(mousePosWorld.x, mousePosWorld.y);
        Vector3 direction = (mousePosWorld - transform.position);

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        _zRotation = Mathf.Clamp(-angle, -80, 80);

        Quaternion targetRotation =Quaternion.Euler(0,0,_zRotation);
            //Quaternion.AngleAxis(_zRotation, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.localRotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            _animator.SetTrigger("Fire");
        }
    }
    public void FireEvent()
    {
        AudioManager.Instance.PlayerGunAudio();
        GameObject bullet = Instantiate(_bulletPrefab, _fire.transform.position, transform.rotation);
    }
}
