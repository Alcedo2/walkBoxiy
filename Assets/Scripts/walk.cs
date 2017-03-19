using UnityEngine;
using System.Collections;

public class walk : MonoBehaviour {
    const int N = 10, O = 25,Range = 90, P = 1000;
    
    public iTween.EaseType mEaseType;
    public textMessage text;
    public UnityEngine.Object originObject;
    public int index;
    public int count;
    bool mugen = false;

    private UnityEngine.GameObject thisObjct;
    int[][] list;
    UnityEngine.GameObject[] boxiy;
    float x;
    int generation;

    // Use this for initialization
    void Start () {
        thisObjct = GameObject.Find("GameObject");
        count = 1;
        index = 0;
        generation = 1;
        x = this.transform.position.x;
        initiarize();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            mugen = !mugen;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int[] rank = evaluation();
            float first = boxiy[rank[0]].transform.localPosition.x;
            float second = boxiy[rank[1]].transform.localPosition.x;
            text.str = "第" + (count / P + 1) + "世代 \n第一位：" + first + " [" + rank[0] + "]"
                + "\n第二位：" + second + " [" + rank[1] + "]";
            crossover(rank);
            mutation();
            destroyObjects();
            createObjects();
            count++;
        }

        //StartCoroutine("sleep");
        movement();
        if (isfinish() && !mugen)
        {
            generation++;
            int[] rank = evaluation();
            float first = boxiy[rank[0]].transform.localPosition.x;
            float second = boxiy[rank[1]].transform.localPosition.x;
            text.str = "第" + generation + "世代 \n第一位：" + first + " [" + rank[0] + "]"
                + "\n第二位：" + second + " [" + rank[1] + "]";
            crossover(rank);
            mutation();
            destroyObjects();
            createObjects();
            count++;
        }
    }

    int calcd(int reg, int n)
    {
        int d = 0;
        if (reg < n)
            d = 1;
        else if (reg > n)
            d = -1;
        return d;
    }

    void initiarize()
    {
        boxiy = new UnityEngine.GameObject[25];
        createObjects();

        list = new int[O][];
        for (int i = 0; i < list.Length; i++)
            list[i] = new int[N];
        for(int i=0;i<list.Length;i++)
            for(int j=0; j< list[i].Length; j++)
                list[i][j] = Random.Range(-Range, Range);
    }

    void movement()
    {
        Transform a, b, c, d;
        //Transform RightReg;
        for (int i = 0; i < list.Length; i++)
        {
            a = boxiy[i].GetComponentInChildren<Transform>();
            b = a.GetChild(0);
            c = b.GetChild(0);
            if (c.gameObject.GetComponent<ChildColliderTrigger>().hit)
            {
                continue;
            }
            if (index % 2 == 0)
            {
                d = c.GetChild(0);
            }
            else
            {
                d = c.GetChild(1);   
            }
            iTween.RotateTo(d.gameObject, iTween.Hash("z", list[i][index], "speed", 30));
        }
        if (index < N - 1)
            index++;
        else
        {
            index = 0;
            count++;
        }
    }

    bool isfinish()
    {
        for (int i = 0; i < list.Length; i++)
        {
            Transform a, b, c;
            a = boxiy[i].GetComponentInChildren<Transform>();
            b = a.GetChild(0);
            c = b.GetChild(0);
            if (!c.gameObject.GetComponent<ChildColliderTrigger>().hit)
                return false;
        }

        return true;
    }

    int[] evaluation()
    {
        float max1, max2;
        int maxN1=0, maxN2=1;
        max1 = boxiy[0].transform.localPosition.x;
        max2 = boxiy[1].transform.localPosition.x;
        if ( max1 < max2)
        {
            float temp = max1;
            max1 = max2;
            max2 = temp;
            maxN1 = 1;
            maxN2 = 0;
        }

        for(int i = 2; i<boxiy.Length; i++)
        {
            float locx = boxiy[i].transform.localPosition.x;
            if(max1 < locx)
            {
                max2 = max1;
                max1 = locx;
                maxN2 = maxN1;
                maxN1 = i;
            } else if(max2 < locx)
            {
                max2 = locx;
                maxN2 = i;
            }
        }
        int[] rank = { maxN1, maxN2 };
        return rank;
    }

    void crossover(int[] rank)
    {
        int[][] newList = new int[O][];
        for (int i = 0; i < list.Length; i++)
            newList[i] = new int[N];

        for(int i=0; i< newList.Length; i++)
        {
            for(int j=0; j<newList[i].Length; j++)
            {
                newList[i][j] = list[rank[(Random.Range(0, 2))]][j];
            }
        }
        list = newList;
    }

    void mutation()
    {
        for(int i = 0; i< list.Length; i ++ )
        {
            if(5 < Random.Range(0,100))
            {
                list[i][Random.Range(0, N)] = Random.Range(-Range, Range);
            }
        }
    }

    void destroyObjects()
    {
        for (int i = 0; i < boxiy.Length; i++)
            Destroy(boxiy[i]);
    }

    void createObjects()
    {
        for (int i = 0; i < boxiy.Length; i++)
        {
            boxiy[i] = Instantiate(originObject, new Vector3(0, 5, i * 10), transform.rotation) as GameObject;
            boxiy[i].transform.parent = thisObjct.transform;
            Transform Armature = boxiy[i].GetComponentInChildren<Transform>().GetChild(0);
            Transform ArmatureBone = Armature.GetChild(0);
            ArmatureBone.gameObject.AddComponent<ChildColliderTrigger>();
        }
    }

    IEnumerator sleep()
    {
        yield return new WaitForSeconds(1f);  //1秒待つ
        //Debug.Log("5秒経ちました");
    }
}
