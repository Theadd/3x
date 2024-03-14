using System;
using CharacterControllers;
using Unity.Properties;
using UnityEngine;

namespace Core.VirtualEntities
{
    // TODO: [assembly: GeneratePropertyBagsForAssembly]
    [Serializable, GeneratePropertyBag]
    public partial class PlayerEntityProvider : IAllSpaceshipProps
    {
        [SerializeField]
        private SpaceshipProps _allProps;
           
        // TODO: Create empty property bag

        [CreateProperty]
        SpaceshipProps IAllSpaceshipProps.allProps
        {
            get => _allProps ??= new SpaceshipProps();
            set => _allProps = value;
        }
    }
}
