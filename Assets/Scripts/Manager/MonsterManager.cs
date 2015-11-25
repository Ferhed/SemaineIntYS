using UnityEngine;
using System.Collections;

public class MonsterManager : MonoBehaviour {

    static MonsterManager instance;
    public static MonsterManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }

    public int normalLife = 1;
    public int normalAttack = 1;
    public int normalMove = 1;

    public int littleLife = 1;
    public int littleAttack = 1;
    public int littleMove = 1;

    public int fatLife = 1;
    public int fatAttack = 1;
    public int fatMove = 1;

    public int oldLife = 1;
    public int oldAttack = 1;
    public int oldMove = 1;
}
