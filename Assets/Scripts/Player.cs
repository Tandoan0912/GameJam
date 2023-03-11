using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class Player : MonoBehaviour
{

    public List<Transform> wayPoint = new List<Transform>();
    public List<Vector2> _wayPoint;

    public GameObject Dog1, Dog2;

    public Transform groundCheck;
    public Transform effectGroup;

    public float speed;
    public float time;
    public float _time;

    public int count;
    public bool isMove;
    public bool isCreateNewPath;

    public float acceleration =1.0f;
    public float maxSpeed = 60.0f;
 
    private float curSpeed = 0.0f;

    private Color _color;

    public ParticleSystem effect;

    void Start()
    {
        count = 0;
        time = _time;
        _color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        curSpeed = 5;
        GetComponent<Renderer>().material.color = _color;
        Dog1.transform.DOScaleY(1, 0.5f);
        Idle(Dog1);
        Idle(Dog2);
    }

    void Update()
    {
        Move();
    }

    public void Idle(GameObject Dog)
    {
        Dog.transform.DOScaleY(.56f, 0.25f).OnComplete(() =>
        {
            Dog1.transform.DOScaleY(.6f, 0.25f);
        }).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    public void GetWayPoint(List<Transform> list)
    {
        if (!isCreateNewPath)
            return;
        wayPoint.Clear();
        _wayPoint.Clear();
        wayPoint = list;
        foreach(var i in wayPoint)
        {
            _wayPoint.Add(new Vector2(i.transform.position.x, i.transform.position.z));
        }
        if (_wayPoint.Count == 0)
            return;
        isMove = true;
    }

    void Move()
    {
        if (!isMove)
            return;
        isCreateNewPath = false;

        //Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, 0.1f);
        //foreach(var i in hitColliders)
        //{
        //    if(i.tag == "Tile")
        //    {
        //        i.GetComponent<Tile>().tileChild.GetComponent<Renderer>().material.color = _color;
        //        i.GetComponent<Tile>().tileChild.gameObject.SetActive(true);
        //    }
        //}

        curSpeed += acceleration * Time.deltaTime;

        if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;

        var target = new Vector2(_wayPoint.Last().x, _wayPoint.Last().y) - new Vector2(effectGroup.transform.position.x, effectGroup.transform.position.z);
        var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        effectGroup.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_wayPoint.Last().x, transform.position.y, _wayPoint.Last().y), curSpeed * Time.deltaTime);
        //effect.gameObject.SetActive(true);
        if (transform.position == new Vector3(_wayPoint.Last().x, transform.position.y, _wayPoint.Last().y))
        {
            isMove = false;
            isCreateNewPath = true;
            curSpeed = 5;
            acceleration = speed;
            //effect.gameObject.SetActive(false);
        }
    }
}
