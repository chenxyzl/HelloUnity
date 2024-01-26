using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTest : MonoBehaviour
{
    public void BeginDrag(GameObject target)
    {
        print("BeginDrag"+ target); 
    }
    public void OnDrag(GameObject target)
    {
        print("OnDrag"+ target);
    }
    public void EndDrag(GameObject target)
    {
        print("EndDrag"+target);

    }
    public void Drop(GameObject target)
    {
        print("Drop"+target);

    }
}
