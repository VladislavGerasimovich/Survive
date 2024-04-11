using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class zmbs : MonoBehaviour
{
    Button bttn_1, bttn_2, bttn_3; 
    Box box;
    float t;
    UIDocument uid;
    float scrn_wdth, scrn_hght, left, top, alpha;
   
    
   


    // Start is called before the first frame update
    void Start()
    {
        uid = gameObject.GetComponent<UIDocument>();
        VisualElement root = uid.rootVisualElement;
        box = root.Q<Box>("box");
        bttn_1 = root.Q<Button>("play");
        bttn_2 = root.Q<Button>("options");
        bttn_3 = root.Q<Button>("exit");
        t = 6f;
        bttn_1.clicked += () => bttn_1_action();
        scrn_wdth = Screen.width;
        scrn_hght = Screen.height;
        left=0;
        top=0;
        alpha = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
    
       
       t += Time.deltaTime;
      
      if ( t < 5) {
          
      
       scrn_wdth -= 400*Time.deltaTime;
       scrn_hght -= 300*Time.deltaTime;
       left += 400*Time.deltaTime*0.5f;
       top += 300*Time.deltaTime*0.5f;
       box.style.width = new Length(scrn_wdth);
       box.style.height = new Length(scrn_hght);
       box.style.left = new Length(left);
       box.style.top = new Length(top);
       alpha -= 0.5f*Time.deltaTime;
       box.style.opacity = alpha;
       if (t > 2) {bttn_1.style.fontSize = new Length(0f);bttn_2.style.fontSize = new Length(0f);bttn_3.style.fontSize = new Length(0f);}
       // box.transform.position = box.transform.position + new Vector3(0f,500f,0f)*Time.deltaTime;
       //box.transform.position = box.transform.position + new Vector3(700f,0f,0f)*Time.deltaTime;
       //bttn_1.transform.position = bttn_1.transform.position + new Vector3(0f,500f,0f)*Time.deltaTime;
       //bttn_2.transform.position = bttn_2.transform.position + new Vector3(0f,500f,0f)*Time.deltaTime;
       //bttn_3.transform.position = bttn_3.transform.position + new Vector3(0f,500f,0f)*Time.deltaTime;
                    }
         
    } //end of update
    
     void bttn_1_action() {t=0; print("OK");SceneManager.LoadScene("Demo");}
}
