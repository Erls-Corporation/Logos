using System;
using System.Collections.Generic;
using System.Linq;

namespace Logos.ReadModel
{
    public class RepositoryListDto
    {
        readonly Guid _id;
        readonly string _name;
        int _version;
        readonly List<SourcefileDto> _sourcefiles;


        public RepositoryListDto(Guid id, string name, int version)
        {
            _id = id;
            _name = name;
            _version = version;

            _sourcefiles = new List<SourcefileDto>();
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int Version
        {
            get
            {
                return _version;
            }
        }

        public List<SourcefileDto> Sourcefiles
        {
            get
            {
                return new List<SourcefileDto>(_sourcefiles);
            }
        }

        public void AddSourcefile(int version, SourcefileDto sourcefile)
        {
            _version = version;

            _sourcefiles.Add(sourcefile);
        }

        public void AddTag(int version, string sourcefilename, string tag)
        {
            _version = version;

            _sourcefiles.Where(sourcefile => sourcefile.Name == sourcefilename).FirstOrDefault().AddTag(tag);
        }
    }
}