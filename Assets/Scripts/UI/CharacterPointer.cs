using UnityEngine;

public class CharacterPointer : MonoBehaviour {
    [SerializeField] private GameObject pointer;
    [SerializeField] private Transform person;
    [SerializeField] private Transform dog;
    private Vector3 targetPosition;
    private RectTransform pointerTransform;

    void Start() {
        pointerTransform = GetComponentInChildren<RectTransform>();
    }

    void Update() {
        pointer.SetActive(Input.GetKey(KeyCode.Tab));

        ChangeTarget();
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) { angle += 360; }
        pointerTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    void ChangeTarget() {
        if (CharacterSwitcher.personEnabled == true) {
            targetPosition = dog.position;
        } else {
            targetPosition = person.position;
        }
    }
}