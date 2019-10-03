using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
public class MySlicer : MonoBehaviour
{
    public GameObject curTarget;
    public Material applyMat;

    // Start is called before the first frame update



    public void ApplySlice() {
        var finalHull = SliceObject(curTarget, applyMat);
        if (finalHull != null) {
            GameObject lowerParent = finalHull.CreateLowerHull(curTarget, applyMat);
            GameObject upperParent = finalHull.CreateUpperHull(curTarget, applyMat);
            lowerParent.AddComponent<MeshCollider>().convex = true;
            upperParent.AddComponent<MeshCollider>().convex = true;
            lowerParent.AddComponent<Rigidbody>().AddForceAtPosition(200f * -transform.right,transform.position);
            upperParent.AddComponent<Rigidbody>().AddForceAtPosition(200f * transform.right, transform.position);

            curTarget.SetActive(false);
            Destroy(upperParent,3f);
            Destroy(lowerParent, 3f);
            Destroy(gameObject,1f);
        }
    }


    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
