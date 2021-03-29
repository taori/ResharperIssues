using System;
using System.Collections.Generic;
using System.Text;

namespace RIDER_60503
{
	public static class ScaleCubeVectorExtender
    {
        public static IEnumerable<CustomVector3> GetValuesForX(CustomVector3 range)
        {
            int z = 0;
            int y = 0;
            var extended = range.X + 1;
            do
            {
                y = 0;
                do
                {
                    // breakpoint at yield. step over should go to GetHashCode of CustomVector3
                    yield return new CustomVector3(extended, y, z);
                } while (y++ < range.Y);
            } while (z++ < range.Z);
        }

        public static IEnumerable<CustomVector3> GetValuesForY(CustomVector3 range)
        {
            int z = 0;
            int x = 0;
            var extended = range.Y + 1;
            do
            {
                x = 0;
                do
                {
                    yield return new CustomVector3(x, extended, z);
                } while (x++ < range.X);
            } while (z++ < range.Z);
        }

        public static IEnumerable<CustomVector3> GetValuesForZ(CustomVector3 range)
        {
            int y = 0;
            int x = 0;
            var extended = range.Z + 1;
            do
            {
                x = 0;
                do
                {
                    yield return new CustomVector3(x, y, extended);
                } while (x++ < range.X);
            } while (y++ < range.Y);
        }
    }

}
