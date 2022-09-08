namespace Collision
{
   
    public interface ICollisionAction
    {
        public enum CollisionActionEnum
        {
            Bounce = 0,
            SpeedChange = 1,
            Death = 2,
            Attract = 3
        }

        public enum CollisionEffectStrengthEnum
        {
            Full = 15,
            Strong = 10,
            MedStrong = 7,
            Normal = 5,
            Half = 3,
            Low = 2,
            Lowest = 1
        }

        public void DoCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum);
        
        public void DoCollisionAreaExitAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum);
    }
}