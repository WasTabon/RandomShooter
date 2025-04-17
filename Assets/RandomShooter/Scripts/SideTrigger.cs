using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomShooter.Scripts
{
    public class SideTrigger : MonoBehaviour
    {
        public UIController uiController;
        public GameObject particleEffect;
        public GameObject dice;
        public DiceShoot diceShoot;

        public SideTrigger[] sideTriggers;

        public bool wasTriggered;
        
        [SerializeField] private int _side;

        [SerializeField] private float _searchRadius = 5f;

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.TryGetComponent(out Chip sideTrigger))
            {
                if (!wasTriggered)
                {
                    wasTriggered = true;
                    foreach (SideTrigger trigger in sideTriggers)
                    {
                        trigger.wasTriggered = true;
                    }
                    Instantiate(particleEffect, coll.transform.position, Quaternion.identity);
                    ExplodeNearbyChips();
                    uiController.lastSide = _side;
                    uiController.AnimateText();
                    diceShoot.ResetState();
                    foreach (SideTrigger trigger in sideTriggers)
                    {
                        trigger.wasTriggered = false;
                    }
                    wasTriggered = false;
                }
            }

            if (coll.CompareTag("Platform"))
            {
                Vector3 contactPoint = coll.ClosestPoint(transform.position);
                Instantiate(particleEffect, contactPoint, Quaternion.identity);
                diceShoot.ResetState();
            }
        }

        private void ExplodeNearbyChips()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius);
            
            List<Chip> chips = hits
                .Select(hit => hit.GetComponent<Chip>())
                .Where(chip => chip != null)
                .OrderBy(chip => Vector3.Distance(transform.position, chip.transform.position))
                .Take(_side)
                .ToList();
            
            foreach (var chip in chips)
            {
                chip.Explode();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}