using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NasaApi.Model
{
    public class MarsRoverResponse
    {
        public Collection<MarsRoverPhotos> photos { get; set; }
    }

    public class MarsRoverPhotos
    {
        public string img_src { get; set; }
    }


}
