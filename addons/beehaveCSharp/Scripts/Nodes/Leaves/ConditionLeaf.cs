#if TOOLS
using Godot;
using Godot.Collections;

namespace Beehave.Nodes.Leaves {
    /// <summary>
    /// Conditions are leaf nodes that either return SUCCESS or FAILURE depending 
    /// on a single simple condition. They should never return `RUNNING`.
    /// </summary>
    public partial class ConditionLeaf : Leaf {

        public override Array<StringName> GetClassNames() {
            Array<StringName> names = base.GetClassNames();
            names.Add(new StringName("ConditionLeaf"));
            return names;
        } // GetClassNames

    } // class
} // namespace
#endif
