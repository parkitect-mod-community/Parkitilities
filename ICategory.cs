using System;

namespace Parkitilities
{
    public interface ICategory<TSelf>
    {
        TSelf Category(String group, String subGroup = "");
    }
}
