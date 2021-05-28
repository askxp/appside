using UnityEngine;
using UnityEngine.EventSystems;

namespace Appside.Scripts
{
    [RequireComponent(typeof(UnityEngine.UI.AspectRatioFitter))]
    public class Joystick : 
        MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IPointerDownHandler,
        IPointerUpHandler {

        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform knob;
        // [Header("Input Values")]
        // [SerializeField] private float horizontal;
        // [SerializeField] private float vertical;


        public float offset;
        private Vector2 _pointPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
        
        }

        public void OnDrag(PointerEventData eventData)
        {
            var rectBg = background.rect;
            var rectKnob = knob.rect;
            var bgPosition = background.position;
            _pointPosition = new Vector2(
                (eventData.position.x - bgPosition.x) / ((rectBg.size.x - rectKnob.size.x) / 2), 
                (eventData.position.y - bgPosition.y) / ((rectBg.size.y - rectKnob.size.y) / 2));
        
            _pointPosition = (_pointPosition.magnitude>2.0f)?_pointPosition.normalized*2f : _pointPosition;
        
            knob.transform.position = new Vector2(
                (_pointPosition.x*((rectBg.size.x-rectKnob.size.x)/2)*offset) + bgPosition.x, 
                (_pointPosition.y*((rectBg.size.y-rectKnob.size.y)/2)*offset) + bgPosition.y);
        
        
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _pointPosition = new Vector2(0f,0f);
            knob.transform.position = background.position;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
       
        }

        public void OnPointerUp(PointerEventData eventData) {
            OnEndDrag(eventData);
        }

        public Vector2 Coordinate()
        {
            return _pointPosition;
        }
    }
}