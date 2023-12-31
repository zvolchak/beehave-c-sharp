#if TOOLS
using Godot;
using Godot.Collections;

namespace Beehave.Nodes.Leaves {
    /// <summary>
    /// Actions are leaf nodes that define a task to be performed by an actor.
    /// Their execution can be long running, potentially being called across multiple
    /// frame executions. In this case, the node should return `RUNNING` until the
    /// action is completed.
    /// </summary>
    public partial class ActionLeaf : Leaf {

        public override Array<StringName> GetClassNames() {
            Array<StringName> names = base.GetClassNames();
            names.Add(new StringName("ActionLeaf"));
            return names;
        } // GetClassNames

    } // class
} // namespace
#endif
