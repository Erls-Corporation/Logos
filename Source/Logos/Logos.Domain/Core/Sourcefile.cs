using System;
using System.Collections.Generic;
namespace Logos.Domain.Core
{
    public sealed class Sourcefile
    {
        readonly string _filename;
        List<string> _tags;

        public Sourcefile(string filename)
        {
            _filename = filename;
            _tags = new List<string>();
        }

        public string Filename
        {
            get
            {
                return _filename;
            }
        }

        public void Tag(string newTag)
        {
            _tags.Add(newTag);
        }
    }
}