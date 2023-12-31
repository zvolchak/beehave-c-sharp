#if TOOLS
using Beehave.Nodes.Leaves;
using Godot;
using System;

namespace Beehave.Nodes.Composites {
    /// <summary>
    /// Sequence nodes will attempt to execute all of its children and report
    /// `SUCCESS` in case all of the children report a `SUCCESS` status code.
    /// If at least one child reports a `FAILURE` status code, this node will also
    /// return `FAILURE` and restart.
    /// </summary>
    public partial class Sequence : Composite {

        public override int Tick(Node actor, Blackboard board) {
            foreach(Node child in GetChildren()) {
                if (child.GetIndex() < CurrentSuccessIndex)
                    continue;

                BeehaveNode beeNode = child as BeehaveNode;
                if (beeNode == null) {
                    // this should probably never happened.. so scream.
                    GD.PushError($"{GetClass()}: Has none 'BeehaveNode' type: {child.GetClass()}.");
                    continue;
                }

                if (beeNode != this.RunningNode)
                    beeNode.BeforeRun(actor, board);

                var actorStatus = (BeehaveStatus)this.tickANode(beeNode, actor, board);

                // TODO: send Debugger Message

                if (actorStatus == BeehaveStatus.RUNNING) {
                    if (this.RunningNode != beeNode) {
                        if (this.RunningNode != null)
                            this.RunningNode.Interrupt(actor, board);
                        this.RunningNode = beeNode;
                    }
                } // Running

                this.cleanupRunningTask(beeNode, actor, board);

                if (actorStatus == BeehaveStatus.FAILURE) {
                    Interrupt(actor, board);
                    beeNode.AfterRun(actor, board);
                    return (int)BeehaveStatus.FAILURE;
                } // Failure

                if (actorStatus == BeehaveStatus.SUCCESS) {
                    CurrentSuccessIndex++;
                    beeNode.AfterRun(actor, board);
                } // Success
            } // foreach

            this.reset();
            return (int) BeehaveStatus.SUCCESS;
        } // Tick


        public override void Interrupt(Node actor, Blackboard board) {
            this.reset();
            base.Interrupt(actor, board);
        }


        private void reset() {
            CurrentSuccessIndex = 0;
        }


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


        private void cleanupRunningTask(BeehaveNode finishedAction, Node actor, Blackboard board) {
            var boardId = actor.GetInstanceId().ToString();
            if (finishedAction != this.RunningNode)
                return;

            this.RunningNode = null;
            if (finishedAction.Equals(board.GetValue(Constants.RUNNING_ACTION, "", boardId))) {
                board.SetValue(Constants.RUNNING_ACTION, "", boardId);
            }
        } // cleanupRunningTask


        /****************************** GETTERS ******************************/


        public int CurrentSuccessIndex { get; protected set; } = 0;

    } // class
} // namespace
#endif