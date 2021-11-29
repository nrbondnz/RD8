using System;

namespace Collision
{
    public static class GOCollisionActionFactory
    {
        public static void setGameObjectCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionActionEnum collisionActionEnum)
        {
            if ( collisionActionEnum == ICollisionAction.CollisionActionEnum.Bounce)
            {
                controller.gameObject.AddComponent<BounceCollisionAction>();
            }
            else if ( collisionActionEnum == ICollisionAction.CollisionActionEnum.SpeedChange)
            {
                controller.gameObject.AddComponent<SpeedChangeCollisionAction>();
            } else 
            {
                throw new NotImplementedException("Non implemented IKeyAction used in factory");
            }
        }
    }
}