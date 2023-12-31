#if TOOLS
using Beehave.Nodes;
using Godot;
using System;

namespace Beehave {
    public partial class BeehaveTree : Node {

        [Export]
        public bool Enabled = true;
		[Export]
		public Blackboard BlackboardNode;
        [Export]
        public ProcessThreadType ProcessThread = ProcessThreadType.PHYSICS;
        [Export]
        public int TickRate = 1;
        [Export]
        public Node Actor { get; protected set; }

        private int lastTick = 0;
        private BeehaveStatus status = BeehaveStatus.NONE;


        public override void _Ready() {
            SetPhysicsProcess(Enabled && ProcessThread == ProcessThreadType.PHYSICS);
            SetProcess(Enabled && ProcessThread == ProcessThreadType.IDLE);

            // Randomize at what frames tick() will happen to avoid stutters
            this.lastTick = GD.RandRange(0, TickRate - 1);

            if (this.Actor == null)
                this.Actor = GetParent();
        } // Ready


        public override void _PhysicsProcess(double delta) {
            this.processInternally();   
        }


        public override void _Process(double delta) {
            this.processInternally();
        }


        public virtual int Tick() {
            if (this.Actor == null || GetChildCount() == 0)
                return (int)BeehaveStatus.FAILURE;

            BeehaveNode child = GetChild(0) as BeehaveNode;

            if (this.status != BeehaveStatus.RUNNING)
                child.BeforeRun(this.Actor, BlackboardNode);

            this.status = (BeehaveStatus) child.Tick(this.Actor, BlackboardNode);

            if (this.status != BeehaveStatus.RUNNING) {
                string boardName = this.Actor.GetInstanceId().ToString();
                BlackboardNode.SetValue(Constants.RUNNING_ACTION, "", boardName);
                child.AfterRun(this.Actor, BlackboardNode);
            }

            return -1;
        } // Tick


        protected virtual void processInternally() {
            if (this.lastTick < this.TickRate - 1) {
                this.lastTick++;
                return;
            }

            this.lastTick = 0;

            // TODO: tie _can_send_message with debugger
            BlackboardNode.SetValue(Constants.CAN_SEND_MESSAGE, false);

            if (GetChildCount() == 1)
                Tick();
        } // processInternally


    } // class
} // namespace
#endif