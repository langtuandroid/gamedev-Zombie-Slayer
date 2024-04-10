using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public enum AffectZoneType { Lighting, Frozen, Poison}
    public class AffectZoneManagerZS : MonoBehaviour
    {
        public static AffectZoneManagerZS Instance;
        [FormerlySerializedAs("affectZoneList")] public AffectZoneZS[] affectZoneListT
            ;
        private AffectZoneZS pickedZoneZsS;
        private AffectZoneType affectTypeE;
    
        [ReadOnly] public bool isChecking = false;
        [ReadOnly] public bool isAffectZoneWorking = false;

        private AffectZoneButton pickedBtnN;
    
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            foreach (var zone in affectZoneListT)
            {
                zone.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (isChecking)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D hit = Physics2D.CircleCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.01f, Vector2.zero);
                    if (hit)
                    {
                        var isZone = hit.collider.gameObject.GetComponent<AffectZoneZS>();
                        if (isZone)
                        {
                            foreach(var zone in affectZoneListT)
                            {
                                zone.gameObject.SetActive(false);
                            }

                            isZone.gameObject.SetActive(true);
                            isZone.ActiveE(affectTypeE);
                            pickedBtnN.StartCountingDown();
                            isChecking = false;
                            isAffectZoneWorking = true;
                        }
                    }
                }
            }
        }
        public void ActiveZoneE(AffectZoneType _type, AffectZoneButton _pickedBtn)
        {
            if (isChecking)
                return;

            pickedBtnN = _pickedBtn;
            affectTypeE = _type;
            isChecking = true;

            foreach (var zone in affectZoneListT)
            {
                zone.gameObject.SetActive(true);
            }
        }

        public void FinishAffectT()
        {
            isAffectZoneWorking = false;
        }
    }
}