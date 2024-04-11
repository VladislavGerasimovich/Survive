using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class options : MonoBehaviour
{
     Button bttn;

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uid = gameObject.GetComponent<UIDocument>();
        VisualElement root = uid.rootVisualElement;
        bttn = root.Q<Button>("bttn_1");
        bttn.clicked += () => print_message(bttn); // assign an Action to perform when clicking the button
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    
        
    } // end of Update()

    void print_message(Button bttn) {
    
    GameObject myPrefab_1 = GameObject.Find("SM_Env_Tree_Orange_01");
    GameObject myPrefab_2 = GameObject.Find("SM_Env_Tree_Lemon_Grown_01");
    
    Instantiate(myPrefab_1,GameObject.Find("Camera").transform.position + new Vector3(0f,-5f,-15f), Quaternion.identity);
    
    } // define the action to perform by means of a function


}
