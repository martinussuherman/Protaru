using System.Collections.Generic;

namespace MonevAtr.Models
{
    public class KodeComparer<T> : IComparer<T>, IEqualityComparer<T> where T : IKode
    {
        public int Compare(T x, T y)
        {
            return x.Kode.CompareTo(y.Kode);
        }

        public bool Equals(T x, T y)
        {
            return x.Kode.Equals(y.Kode);
        }

        public int GetHashCode(T obj)
        {
            return obj.Kode.GetHashCode();
        }
    }
}