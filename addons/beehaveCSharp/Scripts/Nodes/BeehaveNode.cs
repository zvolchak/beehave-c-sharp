#if TOOLS
using Godot;
using Godot.Collections;
using System.Linq;

namespace Beehave.Nodes {
    [Tool]
    public abstract partial class BeehaveNode : Node {
        
        public override string[] _GetConfigurationWarnings() {
            Array<string> warnings = new Array<string>();
            foreach(Node child in GetChildren()) {
                warnings.Append("All children of this node should inherit " +
                    $"from BeehaveNode class. Got {child.GetClass()} for {child.Name}");
            }

            return warnings.ToArray<string>();
        } // _GetConfigurationWarnings


        /// <summary>
        /// Executes this node and returns a status code.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="board"></param>
        public abstract int Tick(Node actor, Blackboard board);


        /// <summary>
        /// Called when this node needs to be interrupted before it can 
        /// return FAILURE or SUCCESS. 
        /// 
        /// Base method calls RunningNode's interrupt and unsets it.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="board"></param>
        public virtual void Interrupt(Node actor, Blackboard board) {
            if (RunningNode == null)
                return;

            RunningNode?.Interrupt(actor, board);
            RunningNode = null;
        } // Interrupt


        /// <summary>
        /// Should be called before the first time it ticks by the parent.
        /// 
        /// Base method does nothing.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="board"></param>
        public virtual void BeforeRun(Node actor, Blackboard board) {
        }


        /// <summary>
        /// Should be called after the last time it ticks and returns SUCCESS or FAILURE.
        /// 
        /// Base method unsets RunningNode.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="board"></param>
        public virtual void AfterRun(Node actor, Blackboard board) {
            RunningNode = null;
        }


        /****************************** GETTERS ******************************/


        public virtual Array<StringName> GetClassNames() {
            return new Array<StringName>() {
                new StringName("BeehaveNode")
            };
        } // GetClassName


        public virtual bool CanSendMessage(Blackboard board) {
            return board.GetValue("can_send_message", false).AsBool();
        } // CanSendMessage


        public virtual BeehaveNode RunningNode { get; protected set; } = null;


    } // class

} // namespace
#endif