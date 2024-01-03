#if TOOLS
using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Beehave.Nodes.Decorators {
    /// <summary>
    /// Decorator nodes are used to transform the result received by its child.
    /// Must only have one child.
    /// </summary>
    public partial class Decorator : BeehaveNode {


        public override int Tick(Node actor, Blackboard board) {
            throw new System.NotImplementedException();
        }


        /****************************** GETTERS ******************************/


        public override string[] _GetConfigurationWarnings() {
            List<string> warnings = base._GetConfigurationWarnings().ToList();
            if (GetChildCount() != 1)
                warnings.Add($"{GetClass()}: Decorator should have exactly one child node.");

            return warnings.ToArray();
        } // _GetConfigurationWarnings


        public override Array<StringName> GetClassNames() {
            Array<StringName> names = base.GetClassNames();
            names.Add(new StringName("Decorator"));
            return names;
        } // GetClassNames

    } // class
} // namespace
#endif