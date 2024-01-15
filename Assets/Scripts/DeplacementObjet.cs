using UnityEngine;
using UnityEngine.InputSystem;

public class DeplacementObjet : MonoBehaviour
{
    private Vector2 deplacementInput;

    public float vitesse = 5f;

    void Update()
    {
        DeplacerObjet();
    }

    void DeplacerObjet()
    {
        Vector3 deplacement = new Vector3(deplacementInput.x, 0, deplacementInput.y) * vitesse * Time.deltaTime;
        transform.Translate(deplacement);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        deplacementInput = context.ReadValue<Vector2>();
    }
}
