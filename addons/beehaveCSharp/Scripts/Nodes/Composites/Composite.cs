#if TOOLS
using Beehave.Nodes;
using Godot;
using Godot.Collections;

namespace Beehave.Nodes.Composites {
    /// <summary>
    /// A Composite node controls the flow of execution of its children in a 
    /// specific manner.
    /// </summary>
    public partial class Composite : BeehaveNode {


        public override int Tick(Node actor, Blackboard board) {
            throw new System.NotImplementedException();
        } // Tick


        /****************************** GETTERS ******************************/

        public override Array<StringName> GetClassNames() {
            Array<StringName> names = base.GetClassNames();
            names.Add(new StringName("Composite"));
            return names;
        } // GetClassNames

    } // class
} // namespace
#endif