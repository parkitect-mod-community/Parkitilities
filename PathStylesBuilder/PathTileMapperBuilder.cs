using UnityEngine;

namespace Parkitilities.PathStylesBuilder
{
    public class PathTileMapperBuilder : BaseBuilder<PathTileMapper>,
        IBuildableNonNonSerializable<PathTileMapper, PathTileMapper>, IBuildableNonNonSerializable<PathTileMapper>
    {

        public PathTileMapperBuilder ForwardRightConnection(GameObject value)
        {
            AddStep("F_R", mapper => { mapper.tileCornerGO = value; });
            return this;
        }

        public PathTileMapperBuilder ForwardBackConnection(GameObject value)
        {
            AddStep("F_B", mapper => { mapper.tileStraightGO = value; });
            return this;
        }

        public PathTileMapperBuilder CrossConnection(GameObject value)
        {
            AddStep("4_way", mapper => { mapper.tile4WayGO = value; });
            return this;
        }

        public PathTileMapperBuilder LeftForwardRightConnection(GameObject value)
        {
            AddStep("3_way", mapper => { mapper.tile3WayGO = value; });
            return this;
        }

        public PathTileMapperBuilder BackConnection(GameObject value)
        {
            AddStep("end", mapper => { mapper.tileEndGO = value; });
            return this;
        }

        public PathTileMapperBuilder NoConnection(GameObject value)
        {
            AddStep("NC", mapper => { mapper.tileSingleGO = value; });
            return this;
        }

        public PathTileMapperBuilder BackUp(GameObject value)
        {
            AddStep("ramp", mapper => { mapper.tileRamp = value; });
            return this;
        }

        public PathTileMapperBuilder BackUpHalf(GameObject value)
        {
            AddStep("ramp_half", mapper => { mapper.tileRampHalf = value; });
            return this;
        }

        public PathTileMapperBuilder ForwardEmptyShopConnection(GameObject value)
        {
            AddStep("shop_connection", mapper => { mapper.tileShopConnectorGO = value; });
            return this;
        }

        public PathTileMapper Build()
        {
            PathTileMapper mapper = ScriptableObject.CreateInstance<PathTileMapper>();
            Apply(mapper);
            return mapper;
        }

        public PathTileMapper Build(PathTileMapper input)
        {
            PathTileMapper mapper = Object.Instantiate(input);
            Apply(mapper);
            return mapper;
        }
    }
}
