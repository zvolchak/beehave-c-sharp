using Beehave.Nodes.Leaves;
using Godot;

namespace Beehave.Examples {
    public partial class HasPositivePosition : ConditionLeaf {

        public override int Tick(Node actor, Blackboard board) {
            Sprite2D s = actor as Sprite2D;
            if (s.Position.X > 0.0 && s.Position.Y > 0.0)
                return (int)BeehaveStatus.SUCCESS;
            else
                return (int)BeehaveStatus.FAILURE;
        } // Tick

    } // class
} // namespace
