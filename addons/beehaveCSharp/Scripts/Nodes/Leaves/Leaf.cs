#if TOOLS
using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Beehave.Nodes.Leaves {
    [Tool]
    public partial class Leaf : BeehaveNode {

        public override string[] _GetConfigurationWarnings() {
            Array<string> warnings = new Array<string>();

            Array<Node> children = GetChildren();
            if (children.Count > 0) {
                warnings.Add("Leaf nodes should not have any child nodes. " +
                    "They won't be ticked.");
            }

            foreach(string source in GetExpressionSources()) {
                string errorMsg = this.parseExpression(source).GetErrorText();
                if (errorMsg.Length > 0)
                    warnings.Add($"[Leaf][{GetClass()}]:: Expression " +
                        $"{source} is invalid! Error text: '{errorMsg}'");
            }

            return warnings.ToArray<string>();
        } // _GetConfigurationWarnings


        public override void Interrupt(Node actor, Blackboard board) {
            throw new NotImplementedException();
        }


        public override int Tick(Node actor, Blackboard board) {
            throw new NotImplementedException();
        }


        protected virtual Expression parseExpression(string source) {
            Expression result = new Expression();
            Error error = result.Parse(source);

            if (Engine.IsEditorHint() && error != Error.Ok) {
                GD.PushError($"[Leaf][{GetClass()}]:: Couldn't parse expression!\n" +
                    $" - source: {source} \n" +
                    $" - Error text: {result.GetErrorText()}");
            }

            return result;
        } // parseExpression


        /****************************** GETTERS ******************************/


        public virtual Array<string> GetExpressionSources() {
            return new Array<string>();
        } // GetExpressionSources


        public override Array<StringName> GetClassNames() {
            Array<StringName> names = base.GetClassNames();
            names.Add(new StringName("Leaf"));
            return names;
        } // GetClassNames

    } // class
} // namespace
#endif
