using UnityEngine;
using UnityEngine.Events;

namespace ldjam_hellevator
{
    [CreateAssetMenu(fileName = "UiManagerChannel", menuName = "Scriptable Objects/UiManagerChannel")]
    public class UiManagerChannel : ScriptableObject
    {
        public UnityAction<int> OnUpdateHearts;
        public UnityAction<float> OnDashCooldown;
       
        public void UpdateHearts(int hp)
        {
            OnUpdateHearts?.Invoke(hp);
        }
        
        public void DashCooldown(float cooldown)
        {
            OnDashCooldown?.Invoke(cooldown);
        }
    }
}
