using UnityEngine;

namespace Parkitilities
{
    public interface IRecolorable<Y>
    {
        Y CustomColor(Color c1);
        Y CustomColor(Color c1, Color c2);
        Y CustomColor(Color c1, Color c2, Color c3);
        Y CustomColor(Color c1, Color c2, Color c3, Color c4);
        Y CustomColor(Color[] colors);
    }
}
