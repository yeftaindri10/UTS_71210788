using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballController : MonoBehaviour
{
public int force;
int scoreKuning;
int scoreHijau;
Text scoreUIKuningA;
Text scoreUIKuningB;
Text scoreUIHijauA;
Text scoreUIHijauB;
Rigidbody2D rigid;
GameObject panelSelesai;
Text txPemenang;
    // Start is called before the first frame update
    void Start()
    {
    rigid = GetComponent<Rigidbody2D>();
    Vector2 arah = new Vector2(0,2).normalized;
    rigid.AddForce(arah*force);
    scoreHijau=0;
    scoreKuning=0;
    scoreUIHijauA = GameObject.Find("scoreHijauA").GetComponent<Text>();
    scoreUIHijauB = GameObject.Find("scoreHijauB").GetComponent<Text>();
    scoreUIKuningA = GameObject.Find("scoreKuningA").GetComponent<Text>();
    scoreUIKuningB = GameObject.Find("scoreKuningB").GetComponent<Text>();
    panelSelesai = GameObject.Find("panelSelesai");
    panelSelesai.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void tampilScore(){
        Debug.Log("Score Kuning: "+ scoreKuning + "Score Hijau: "+ scoreHijau);
        scoreUIHijauA.text = scoreHijau + "";
        scoreUIHijauB.text = scoreHijau + "";
        scoreUIKuningA.text = scoreKuning + "";
        scoreUIKuningB.text = scoreKuning + "";
    }

    private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.name == "PointAtas"){
            scoreHijau += 1;
            tampilScore();
            if(scoreHijau == 5){
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("winInfo").GetComponent<Text>();
                txPemenang.text = "Player Hijau Menang!";
                Destroy(gameObject);
                return;
            }
            resetBall();
            Vector2 arah = new Vector2(0,2).normalized;
            rigid.AddForce(arah*force);
        }
        if(coll.gameObject.name == "PointBawah"){
            scoreKuning += 1;
            tampilScore();
            if(scoreKuning == 5){
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("winInfo").GetComponent<Text>();
                txPemenang.text = "Player Kuning Menang!";
                Destroy(gameObject);
                return;
            }
            resetBall();
            Vector2 arah = new Vector2(0,-2).normalized;
            rigid.AddForce(arah*force);
        }
        if(coll.gameObject.name == "PaddlerKuning" || coll.gameObject.name == "PaddlerHijau"){
            float sudut = (transform.position.x - coll.transform.position.x)*5f;
            Vector2 arah = new Vector2(sudut, rigid.velocity.y).normalized;
            rigid.velocity = new Vector2(0,0);
            rigid.AddForce(arah * force * 2);
        }
    }

    void resetBall(){
        transform.localPosition = new Vector2(0,0);
        rigid.velocity = new Vector2(0,0);
    }
}
