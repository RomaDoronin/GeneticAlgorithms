using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Gen
    {
        private List<short> _alleleList;

        public Gen()
        {
            _alleleList = new List<short>();
        }

        public Gen(Gen gen)
        {
            _alleleList = new List<short>();

            foreach (var allele in gen.GetAlleleList())
            {
                _alleleList.Add(allele);
            }
        }

        public Gen(short val)
        {
            Gen gen = new Gen();
            gen.SetAlleleList(new List<short>() { val });
        }

        public void SetAlleleList(List<short> alleleList) => _alleleList = alleleList;

        public List<short> GetAlleleList()
        {
            List<short> alleleList = new List<short>();

            foreach (var allele in _alleleList)
            {
                alleleList.Add(allele);
            }

            return alleleList;
        }

        public int GetGenSize() => _alleleList.Count;

        public override string ToString()
        {
            String outStr = "";

            foreach (var allele in _alleleList)
            {
                outStr += allele.ToString();
            }

            return outStr;
        }
    }
}
