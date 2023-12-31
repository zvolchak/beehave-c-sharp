#if TOOLS
using Beehave.Nodes.Leaves;
using Godot;

namespace Beehave.Nodes.Composites {
    public partial class SelectorReactiveComposite : Composite {

        public override int Tick(Node actor, Blackboard board) {
            foreach (Node child in GetChildren()) {
                BeehaveNode beeNode = child as BeehaveNode;

                if (beeNode == null) {
                    // this should never happened.. so scream.
                    GD.PushError($"{GetClass()}: Has none 'BeehaveNode' type: {child.GetClass()}.");
                    continue;
                }

                var status = (BeehaveStatus)tickANode(beeNode, actor, board);

                if (status == BeehaveStatus.FAILURE) {
                    beeNode.AfterRun(actor, board);
                    continue;
                }

                // Interrupt any child that was RUNNING before.
                if (beeNode != this.RunningNode)
                    Interrupt(actor, board);

                if (status == BeehaveStatus.SUCCESS) {
                    beeNode.AfterRun(actor, board);
                    return (int)BeehaveStatus.SUCCESS;
                }

                if (status == BeehaveStatus.RUNNING) {
                    this.RunningNode = beeNode;
                    if ((beeNode as ActionLeaf) != null) {
                        board.SetValue(
                                Constants.RUNNING_ACTION,
                                beeNode,
                                actor.GetInstanceId().ToString()
                            );
                    }

                    return (int)BeehaveStatus.RUNNING;
                }
            } // foreach

            return (int)BeehaveStatus.FAILURE;
        } // Tick


        protected virtual int tickANode(BeehaveNode target, Node actor, Blackboard board) {
            if (target != this.RunningNode)
                target.BeforeRun(actor, board);

            int actorStatus = target.Tick(actor, board);
            
            // TODO: send debugger messages

            if ((target as ConditionLeaf) != null) {
                board.SetValue(
                        Constants.LAST_CONDITION, 
                        target, 
                        actor.GetInstanceId().ToString()
                    );
                board.SetValue(
                        Constants.LAST_CONDITION_STATUS,
                        actorStatus,
                        actor.GetInstanceId().ToString()
                    );
            }

            return -1;
        } // tickANode

    } // class
} // namespace
#endif