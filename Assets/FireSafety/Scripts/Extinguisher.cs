using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Extinguisher : MonoBehaviour
{
    [Header("Система частиц пены")]
    public ParticleSystem foamParticle;

    [Header("Чека")]
    public Transform seal;
    private Vector3 startSealPos;

    [Header("Раструб")]
    public Transform spray;
    private Vector3 startSprayPos;
    private Quaternion startSprayRot;

    [Header("Джойстики для дочерности")]
    public Transform[] grabbers;
    private int lastHand = -1;

    private void Start()
    {
        foamParticle.Stop();
    }
    public void OnGrabbed(BasicGrabbable grabbedObj)
    {
        ViveColliderButtonEventData viveEventData;
        if (!grabbedObj.grabbedEvent.TryGetViveButtonEventData(out viveEventData)) { return; }
        int currentHand = viveEventData.viveRole.ToRole<HandRole>() == HandRole.RightHand ? 0 : 1;
        if (!GameManager.IsExtinguisherInHand)
        {
            grabbedObj.transform.SetParent(grabbers[currentHand]);
            GameManager.IsExtinguisherInHand = true;
            lastHand = currentHand;
        }
        else
        {
            GameManager.IsExtinguisherInHand = false;
            if (lastHand == currentHand)
            {
                transform.parent = null;
            }
            else
            {
                OnGrabbed(grabbedObj);
            }
        }
    }
    public void OnDrop()
    {
        if (GameManager.IsExtinguisherInHand)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void OnPress()
    {
        if (GameManager.IsSealRemoved)
        {
            if (foamParticle.isPlaying)
            {
                foamParticle.Stop();
                foamParticle.transform.GetComponent<CapsuleCollider>().isTrigger = false;
                foreach (var item in FindObjectsOfType<Fire>())
                {
                    item.StopAllCoroutines();
                    Debug.Log("Stop Extingiush Global");
                }
            }
            else
            {
                foamParticle.Play();
                foamParticle.transform.GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
    }

    public void OnTakeSeal()
    {
        startSealPos = seal.position;
    }

    public void OnRemoveSeal()
    {
        if (!GameManager.IsSealRemoved && Vector3.Distance(startSealPos, seal.position) > GameManager.DistanseForSeal)
        {
            GameManager.IsSealRemoved = true;
            seal.gameObject.AddComponent<Rigidbody>();
            seal.parent = null;
        }
        else
        {
            seal.position = startSealPos;
        }
    }

    public void OnTakeSpray()
    {
        startSprayPos = spray.position;
        startSprayRot = spray.localRotation;
    }
    public void OnDropSpray()
    {
        spray.position = startSprayPos;
        spray.localRotation = startSprayRot;
    }
}
