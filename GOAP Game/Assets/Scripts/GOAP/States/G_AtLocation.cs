using UnityEngine;
using GOAP;

namespace GOAP
{
    public class G_AtLocation : G_State
    {
        [SerializeField]
        LocationType value;

        #region Basic Controls
        public override void Construct(string name, object value)
        {
            this.name = name;
            SetValue(value);
        }

        public override void SetValue(object value)
        {
            this.value = (LocationType)value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override G_State Clone()
        {
            return An.AtLocation().WithName(name).WithLocationType(value);
        }

        #endregion

        #region Testing Controls
        /// <summary>
        /// Tests the given state agaisnt the expectedValue using the chosen comparison, returning true if the comparison
        /// is correct and false if not
        /// </summary>
        /// <param name="state"></param>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public override bool TestState(G_State state, G_StateComparison comparison, object expectedValue)
        {
            LocationType stateLocation = state.GetValue() as LocationType;
            LocationType expectedLocation = expectedValue as LocationType;
            bool success = false;

            if(StateSupportsComparion(comparison))
            {
                success = CompareLocations(stateLocation, expectedLocation);
            }
            return success;
        }

        /// <summary>
        /// This function returns true if the two given conditions match their states, expected values, and comparisons
        /// </summary>
        /// <returns></returns>
        public override bool TestStateConditionMatch(G_Condition precondition, G_Condition effect)
        {
            bool success = false;
            LocationType preLocation = precondition.ExpectedValue as LocationType;
            LocationType effectLocation = effect.ExpectedValue as LocationType;

            if (StateSupportsComparion(precondition.Comparison) && StateSupportsComparion(effect.Comparison))
            {
                success = CompareLocations(preLocation, effectLocation);
            }

            return success;
        }

        /// <summary>
        /// Returns true if the state type has an implementation for the given comparison type
        /// </summary>
        /// <returns></returns>
        public override bool StateSupportsComparion(G_StateComparison comparison)
        {
            return comparison == G_StateComparison.equal;
        }

        /// <summary>
        /// Tests if the given value is of the same type as the value stored in this state and returns true if it is
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public override bool TestValueMatch(object testValue)
        {
            return testValue is null || testValue is LocationType;
        }

        #endregion

        #region Conditions

        bool CompareLocations(LocationType locationOne, LocationType locationTwo)
        {
            return BothLocationsNull(locationOne, locationTwo) || LocationsMatch(locationOne, locationTwo);
        }

        bool BothLocationsNull(LocationType locationOne, LocationType locationTwo)
        {
            return locationOne == null && locationTwo == null;
        }

        bool LocationsMatch(LocationType locationOne, LocationType locationTwo)
        {
            return locationOne != null && locationTwo != null && locationOne == locationTwo;
        }

        #endregion
    }
}
