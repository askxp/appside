using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Appside.Scripts
{
    public class InputHandler : 
        MonoBehaviour, 
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private RectTransform area;
        [SerializeField] private float doubleTapTime = 0.5f;

        private float _lastTapTime;
    
        // private bool _isPointerDown = false;
        public Action onJump = delegate {  };

        public Vector2 GetMoveVector()
        {
            return joystick.Coordinate();
        }
    

        // public float GetMovePower {
        //     get => joystick.Coordinate().magnitude;
        // }

        //REFACTORING: move to event
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onJump.Invoke();
            }
        }

  
    
        //TODO: remove below methods
    
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerEnter == area.gameObject)
            {
                joystick.gameObject.SetActive(true);
                joystick.GetComponent<RectTransform>().position = eventData.pressPosition;
                joystick.OnPointerDown(eventData);
            }
        
            float time = Time.time;
            if ((time - _lastTapTime) < doubleTapTime)
            {
                onJump.Invoke();
            }
            _lastTapTime = time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.rawPointerPress == area.gameObject)
            {
                joystick.gameObject.SetActive(false);
                joystick.OnPointerUp(eventData);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.rawPointerPress == area.gameObject)
            {
                joystick.OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.rawPointerPress == area.gameObject)
            {
                joystick.OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.rawPointerPress == area.gameObject)
            {
                joystick.OnEndDrag(eventData);
            }
        }
    }
}
