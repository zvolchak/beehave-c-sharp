#if TOOLS
using Beehave.Nodes;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Beehave {

    [Tool]
    public partial class Blackboard : Node {

        private Dictionary<string, Dictionary<Variant, Variant>> blackboard 
            = new Dictionary<string, Dictionary<Variant, Variant>>();


        public List<string> Keys => blackboard.Keys.ToList();


        /// <summary>
        /// Set the Key-Value pair for a blackboard. If blackboard name doesn't
        /// yet exist, it will create a new one.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="blackboardName">
        /// Name of the blackboard to find the key of.
        /// </param>
        public void SetValue(
            Variant key,
            Variant value, 
            string blackboardName = "default"
        ) {
            if (!this.blackboard.ContainsKey(blackboardName))
                this.blackboard[blackboardName] = new Dictionary<Variant, Variant>();
            
            this.blackboard[blackboardName][key] = value;
        } // SetValue


        /// <summary>
        /// Get the value for the given blackboardName and Key. If nothing found,
        /// return defaultReturn value instead.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultReturn">
        /// What to return if no blackboardName or key has been found on the board.
        /// </param>
        /// <param name="blackboardName">
        /// Name of the blackboard to find the key of.
        /// </param>
        public Variant GetValue(
            Variant key,
            Variant defaultReturn, 
            string blackboardName = "default"
        ) {
            if (!HasKey(key, blackboardName))
                return defaultReturn;
            return this.blackboard[blackboardName][key];
        } // GetValue


        /// <summary>
        /// Check if Key exists on the blackboardName board.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blackboardName">
        /// Name of the blackboard to find the key of.
        /// </param>
        /// <returns>
        /// True if blackboardName AND Key exists on the board.
        /// False if no blackboardName OR Key exists on the board.
        /// </returns>
        public bool HasKey(Variant key, string blackboardName = "defalut") {
            if (!this.blackboard.ContainsKey(blackboardName))
                return false;
            if (!this.blackboard[blackboardName].ContainsKey(key))
                return false;
            return true;
        } // HasKey


        /// <summary>
        /// Remove passed key from the blackboardName board. Return 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blackboardName">
        /// Name of the blackboard to find the key of.
        /// </param>
        /// <returns>
        /// True if key existed and was successfully removed. 
        /// False - if no blackboardName or Key found on the board.
        /// </returns>
        public bool EraseValue(Variant key, string blackboardName = "default") {
            if (!(HasKey(key, blackboardName)))
                return false;
            this.blackboard[blackboardName].Remove(key);
            return true;
        } // EraseValue


    } // class
} // namespace
#endif