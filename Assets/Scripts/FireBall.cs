using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

    public GameObject target;
    public int idPlayer;

	// Use this for initialization
	public void SetTarget(GameObject go, int id)
    {
        target = go;
        idPlayer = id;

        if(idPlayer == 2)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.3f);
            if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
            {
                if (target.GetComponent<MonsterScript>())
                {
                    target.GetComponent<MonsterScript>().receiveDamage(SpellManager.Instance.damageFireBall);
                    SoundManager.Instance.playSound(SoundManager.Instance.fireBall, 1f, target.GetComponent<MonsterScript>().currentTile);
                }
                else
                {
                    PlayerManager.Instance.playerList[idPlayer % 2].GetComponent<PlayerScript>().receiveDamage(SpellManager.Instance.damageFireBall);
                    SoundManager.Instance.playSound(SoundManager.Instance.fireBall, 1f, target);
                }

                GameObject explosion = Instantiate(SpellManager.Instance.spells[1], target.transform.position + Vector3.up / 2, Quaternion.identity) as GameObject;
                Destroy(explosion, 2f);
                explosion.transform.Rotate(new Vector3(-90, 0, 0));
                Destroy(gameObject);
            }
        }
	
	}
}
