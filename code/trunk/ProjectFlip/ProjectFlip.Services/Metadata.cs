using System;
using System.Linq;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class Metadata : IMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        public Metadata(MetadataType type, string description)
        {
            Type = type;
            Description = description;
        }

        public MetadataType Type { get; private set; }
        public string Description { get; private set; }

        public bool Match(IProjectNote projectNote)
        {
            switch (Type)
            {
                case MetadataType.Sector:
                    return projectNote.Sector == Description;
                    break;
                case MetadataType.Technologies:
                    return projectNote.Technologies.Any(d => d.Description == Description);
                    break;
                case MetadataType.Services:
                    return projectNote.Services.Any(d => d.Description == Description);
                    break;
                case MetadataType.Tools:
                    return projectNote.Tools.Any(d => d.Description == Description);
                    break;
                case MetadataType.Customer:
                    return projectNote.Customer == Description;
                    break;
                case MetadataType.Focus:
                    return projectNote.Focus.Any(d => d.Description == Description);
                    break;
                case MetadataType.Applications:
                    return projectNote.Applications.Any(d => d.Description == Description);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
