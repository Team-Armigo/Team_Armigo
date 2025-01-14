using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.Collections;
using UnityEngine.UI;

public class ARFaceDetector1 : MonoBehaviour
{
    NativeArray<ARCoreFaceRegionData> regionData;

    public ARFaceManager afm;

    [SerializeField]
    private float moveForce = 200f;

    private float movementX;

    bool isJump = false;
    bool isTop = false;
    public float jumpHeight = 0;
    public float jumpSpeed = 0;
    Vector2 startPosition;
    Animator animator;

    public GameObject gameObject;

    private float _xCor;
    public float xCor
    {
        get { return _xCor; }
        set { _xCor = value; }
    }

    private float _yCor;
    public float yCor
    {
        get { return _yCor; }
        set { _yCor = value; }
    }

    ARCoreFaceSubsystem subSys;

    public Text yCorText;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        animator = gameObject.GetComponent<Animator>();

        // AR Face Manager가 얼굴을 인식할 때 실행할 함수를 연결한다.
        afm.facesChanged += OnDetectFaceAll;

        // AR Foundation의 XRFaceSubsystem 클래스 변수를 AR Core의 ARCoreFaceSubsytem 클래스 변수로 캐스팅한다.
        subSys = (ARCoreFaceSubsystem)afm.subsystem;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Animator>().SetBool("Run", true);

    }

    // 얼굴 메시 데이터를 이용한 방식
    void OnDetectFaceAll(ARFacesChangedEventArgs args)
    {
        // 얼굴을 인식했을 때는...
        if (args.updated.Count > 0)
        {
            // 얼굴 정점 배열에서 지정한 인덱스에 해당하는 좌표를 가져온다.
            Vector3 vertPosition = args.updated[0].vertices[4];
            //Vector3 vertPosition = args.updated[0].vertices[num];

            // 정점 좌표를 월드 좌표로 변환한다.
            vertPosition = args.updated[0].transform.TransformPoint(vertPosition);

            // x, y 좌표를 가져와서 저장
            //xCor = vertPosition.x;
            yCorText.text = vertPosition.y.ToString();
            yCor = vertPosition.y;

            if (yCor >= 0.01)
            {
                isJump = true;
            }

            else if (gameObject.transform.position.y >= startPosition.y)
            {
                isJump = false;
                isTop = false;
                gameObject.transform.position = startPosition;
            }

            if (isJump)
            {
                movementY = 10f;
                gameObject.transform.position += new Vector3(0f, movementY, 0f) * Time.deltaTime * moveForce;

/*                if (gameObject.transform.position.y <= jumpHeight - 0.1f && !isTop)
                {
                    gameObject.transform.position = Vector2.Lerp(transform.position,
                    new Vector2(transform.position.x, jumpHeight), jumpSpeed * Time.deltaTime);
                }

                else
                {
                    isTop = true;
                }

                if (transform.position.y > startPosition.y && isTop)
                {
                    transform.position = Vector2.MoveTowards(transform.position, startPosition, jumpSpeed * Time.deltaTime);
                }*/
            }
        }

        // 얼굴을 인식하지 못했을 때는...
        else if (args.removed.Count > 0)
        {

        }

    }
}
