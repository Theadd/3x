using System;

namespace CharacterControllers
{
    public interface IProps { }
    
    [Serializable]
    public class SpaceshipProps
    {
        public float moveIncreaseSmooth = 5f;
        public float thrust = 650f;
        public float thrustGlideReduction = .85f;
        public float upThrust = 75f;
        public float upDownGlideReduction = .85f;
        public float strafeThrust = 650f;
        public float leftRightGlideReduction = .85f;
        
        public float pitchSensitivity = 3f;
        public float yawSensitivity = 3f;
        public float pitchSensitivityReduction = .25f;
        public float yawSensitivityReduction = .25f;

//        private (float, float, float) AllProps(SpaceshipProps props)
//        {
//            return (props.moveIncreaseSmooth, props.thrust, props.thrustGlideReduction);
//        }
    }

//    public static class SpaceshipPropsExtensions
//    {
//        public static (float, float, float) AllProps(this SpaceshipProps props) => 
//            (props.MoveIncreaseSmooth, props.thrust, props.thrustGlideReduction);
//    }
    
    public interface IAllSpaceshipProps : IProps
    {
        protected SpaceshipProps allProps { get; set; }

        public virtual (float, float, float, float, float, float, float) AllMoveProps() => 
            (allProps.moveIncreaseSmooth, allProps.thrust, allProps.thrustGlideReduction, allProps.upThrust,
                allProps.upDownGlideReduction, allProps.strafeThrust, allProps.leftRightGlideReduction);
        
        public virtual (float, float, float, float) AllLookProps() => 
            (allProps.pitchSensitivity, allProps.yawSensitivity, allProps.pitchSensitivityReduction, allProps.yawSensitivityReduction);
    }

//    // SAMPLE IMPLEMENTATION //
//    // TODO: [assembly: GeneratePropertyBagsForAssembly]
//    [Serializable, GeneratePropertyBag]
//    public class SpaceshipPropsStore : SpaceshipProps, IAllSpaceshipProps
//    {
//        [SerializeField]
//        private SpaceshipProps _allProps;
//
//        [CreateProperty]
//        SpaceshipProps IAllSpaceshipProps.allProps
//        {
//            get => _allProps;
//            set => _allProps = value;
//        }
//    }
}
