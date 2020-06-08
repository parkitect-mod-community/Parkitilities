using UnityEngine;

namespace Parkitilities
{
    public interface IRecolorable<TResult>
    {
        TResult CustomColor(Color c1);
        TResult CustomColor(Color c1, Color c2);
        TResult CustomColor(Color c1, Color c2, Color c3);
        TResult CustomColor(Color c1, Color c2, Color c3, Color c4);
        TResult CustomColor(Color[] colors);
        TResult DisableCustomColors();
    }
}
