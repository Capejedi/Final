using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melonpickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(5);
            Destroy(gameObject);
        }
    }
}