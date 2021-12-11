using System;

namespace Collision
{
    public static class GOCollisionActionFactory
    {
        public static void SetGameObjectCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionActionEnum collisionActionEnum)
        {
            if ( collisionActionEnum == ICollisionAction.CollisionActionEnum.Bounce)
            {
                controller.gameObject.AddComponent<BounceCollisionAction>();
            }
            else if ( collisionActionEnum == ICollisionAction.CollisionActionEnum.SpeedChange)
            {
                controller.gameObject.AddComponent<SpeedChangeCollisionAction>();
            } else if (collisionActionEnum == ICollisionAction.CollisionActionEnum.Death)
            {
                controller.gameObject.AddComponent<DeathCollisionAction>();
            } else if (collisionActionEnum == ICollisionAction.CollisionActionEnum.Attract)
            {
                controller.gameObject.AddComponent<AttrctCollisionAction>();
            } 
            else 
            {
                throw new NotImplementedException("Non implemented IKeyAction used in factory");
            }
        }
    }
}