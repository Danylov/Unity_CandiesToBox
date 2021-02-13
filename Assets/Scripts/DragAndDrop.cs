using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private GameObject target;
    private bool isMouseDrag;
    private Vector3 screenPosition, offset;
    private GameObject heartBottom, heartTop;
    private HeartTopMove heartTopMoveScript;
    private Collider targetCollider, heartBottomCollider;
    private static int numCandies = 8;
    private static List<int> movedCandies;

    static DragAndDrop()
    {
        movedCandies = new List<int>(numCandies);
    }
    void Start()
    {
        heartBottom = GameObject.Find("HeartBottom");
        heartBottomCollider = heartBottom.GetComponent<Collider>();
        heartTopMoveScript = GameObject.Find("HeartTop").GetComponent<HeartTopMove>();
        for (int i = 0; i < numCandies; i++) movedCandies.Add(0);
    }
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
            targetCollider = target.GetComponent<Collider>();
        }
        return target;
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);
            if (target != null)
            {
                isMouseDrag = true;
                var targetPosition = target.transform.position;
                screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
                offset = targetPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
        }
       
        if (Input.GetMouseButtonUp(0))
        {
            var Sum = 0;
            isMouseDrag = false;
            var targetPosition = target.transform.position;
            var heartBottomPosition = heartBottom.transform.position;
            var targetColliderBoundsSize = targetCollider.bounds.size;
            var heartBottomColliderBoundsSize = heartBottomCollider.bounds.size;
            var targetLeft = targetPosition.x - targetColliderBoundsSize.x / 2;
            var targetRight = targetPosition.x + targetColliderBoundsSize.x / 2;
            var targetFront = targetPosition.z - targetColliderBoundsSize.z / 2;
            var targetBack = targetPosition.z + targetColliderBoundsSize.z / 2;
            var heartBottomLeft = heartBottomPosition.x - heartBottomColliderBoundsSize.x / 2;
            var heartBottomRight = heartBottomPosition.x + heartBottomColliderBoundsSize.x / 2;
            var heartBottomFront = heartBottomPosition.z - heartBottomColliderBoundsSize.z / 2;
            var heartBottomBack = heartBottomPosition.z + heartBottomColliderBoundsSize.z / 2;
            if ((heartBottomLeft < targetLeft) && (targetRight < heartBottomRight) &&
                (heartBottomFront < targetFront) && (targetBack < heartBottomBack))
            {
                if (target.name == "Candy1") movedCandies[0] = 1;
                if (target.name == "Candy2") movedCandies[1] = 1;
                if (target.name == "Candy3") movedCandies[2] = 1;
                if (target.name == "Candy4") movedCandies[3] = 1;
                if (target.name == "Candy5") movedCandies[4] = 1;
                if (target.name == "Candy6") movedCandies[5] = 1;
                if (target.name == "Candy7") movedCandies[6] = 1;
                if (target.name == "Candy8") movedCandies[7] = 1;
            }
            for (int i = 0; i < numCandies; i++) Sum += movedCandies[i];
            if (Sum == numCandies) heartTopMoveScript.MoveBoxCover();
        }
 
        if (isMouseDrag)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            Vector3 temp = new Vector3(0,0.05f,0);
            target.transform.position = currentPosition + temp;
        }
 
    }
}