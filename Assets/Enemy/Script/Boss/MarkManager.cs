using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkManager : MonoBehaviour
{
   public class Marker 
   {
        public Vector3 position;
        public Quaternion rotation;

        public Marker(Vector3 pos, Quaternion rot) 
        {
            position = pos;
            rotation = rot;
        }
   }
    public List<Marker> markerList = new List<Marker>();

    private void FixedUpdate()
    {
        UpdateLarkerList();
    }
    public void UpdateLarkerList() 
    {
        markerList.Add(new Marker(transform.position, transform.rotation));
    }
    public void ClearmMarkerList() 
    { 
        markerList.Clear();
        markerList.Add(new Marker(transform.position, transform.rotation));
    }

}
