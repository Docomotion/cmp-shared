using System;

namespace Docomotion.Shared.Utils
{
    public partial class FLib
    {
        public static void GC4Object(object obj2Free)
        {
            if (obj2Free != null)
            {
                int gen = GC.GetGeneration(obj2Free);

                GC.Collect(gen, GCCollectionMode.Forced);
            }
        }

    }
}
