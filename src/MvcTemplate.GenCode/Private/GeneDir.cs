using System;
using System.Collections.Generic;
using System.Text;

namespace MvcTemplate.GenCode.Private
{
    internal class GeneDir : IGene
    {
        private string target;

        public GeneDir(string _target)
        {
            target = _target;
        }

        public void Run()
        {

            if (!System.IO.Directory.Exists(target))
            {
                System.IO.Directory.CreateDirectory(target);
            }           
            Console.WriteLine(target);
        }
    }
}
