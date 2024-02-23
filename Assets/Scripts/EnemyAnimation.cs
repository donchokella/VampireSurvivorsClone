using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Transform sprite, nameEntered;
    private float activeSize;

    [SerializeField] private float speed;
    [SerializeField] private float minSize, maxSize;
    private float fixedMinSize, fixedMaxSize;

    protected virtual void Start()
    {
        nameEntered = transform.GetChild(0);
        sprite = transform.GetChild(1);

        fixedMaxSize = sprite.localScale.x * maxSize;
        fixedMinSize = sprite.localScale.x * minSize;

        if(sprite.gameObject.activeSelf)
        {
            activeSize = fixedMaxSize;
        }
        else
        {
            activeSize = maxSize;
        }

        speed = speed*Random.Range(0.5f, 1.5f);
    }

    private void Update()
    {
        if (sprite.gameObject.activeSelf)
        {
            sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed * Time.deltaTime);

            if (sprite.localScale.x == fixedMaxSize)
            {
                activeSize = fixedMinSize;
            }
            else if (sprite.localScale.x == fixedMinSize)
            {
                activeSize = fixedMaxSize;
            }
        }
        else
        {
            nameEntered.localScale = Vector3.MoveTowards(nameEntered.localScale, Vector3.one * activeSize, speed * Time.deltaTime);

            if (nameEntered.localScale.x == maxSize)
            {
                activeSize = minSize;
            }
            else if (nameEntered.localScale.x == minSize)
            {
                activeSize = maxSize;
            }
        }
    }
}
